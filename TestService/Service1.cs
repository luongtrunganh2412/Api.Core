using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Configuration;
using System.Threading;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Script.Serialization;
using Api.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Api.Core.Common;
using TestService.Models;
using System.Net;
namespace TestService
{
    public partial class Service1 : ServiceBase
    {
        static Thread pushDelivery;
        public Service1()
        {
            PushDeliveryToPartner();
            // InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            LogAPI.LogToFile(LogFileType.THREADING, "On Start service..");
            pushDelivery = new Thread(new ThreadStart(PushDeliveryToPartner));
            pushDelivery.Start();
        }
        public void PushDeliveryToPartner()
        {
            int time_sleep = int.Parse(ConfigurationSettings.AppSettings["DATASYNC_TIME_SLEEP"].ToString());
            time_sleep = time_sleep * 60000;//60s
            Api.Core.Data.EmsRepository _ems = new Api.Core.Data.EmsRepository();
            Api.Core.Data.ShipmentRepository _shipmentRepository = new Api.Core.Data.ShipmentRepository();
            //List<Api.Core.Models.Delivery> _listDelivery = new List<Api.Core.Models.Delivery>();
            //Api.Core.Models.Lading_Info _ladinginfo = new Api.Core.Models.Lading_Info();
            List<Api.Core.Models.Shipment> _lstShipment = new List<Api.Core.Models.Shipment>();
            Api.Core.Data.DomesticRepository _domestic = new Api.Core.Data.DomesticRepository();

            
            string customer_code = ConfigurationSettings.AppSettings["Customer_Code"].ToString();
            string sc_user = ConfigurationSettings.AppSettings["SC_USER"].ToString();
            string sc_password = ConfigurationSettings.AppSettings["SC_PASSWORD"].ToString();
            string uri = ConfigurationManager.AppSettings["Partner_Uri"];
            while (true)
            {
                _lstShipment = _shipmentRepository.GET_SHIPMENT(customer_code);
                if (_lstShipment != null)
                {
                    foreach (var items in _lstShipment)
                    {
                        try
                        {           
                            var _listDelivery = _domestic.TrackingProcess(items.MAE1);
                            Api.Core.Models.SC_Journey _scjourney = new Api.Core.Models.SC_Journey();
                            _scjourney.username = sc_user;
                            _scjourney.password = sc_password;                            
                            if (_listDelivery != null)
                            {
                                var _ladinginfo = _domestic.E1_infomation(items.MAE1.Trim());
                                _listDelivery = _listDelivery.OrderBy(o => o.DateInt).ThenBy(o => o.TimeInt).ToList();
                                foreach (var item in _listDelivery)
                                {
                                    _scjourney.SC_CODE = items.TRACKING_NUMBER;
                                    _scjourney.STATUS = Convert_Status(item.Status, _scjourney.COLLECT);
                                    if (_scjourney.STATUS == "I" || _scjourney.STATUS == "IE")
                                        _ems.SET_STATUSINT(items.TRACKING_NUMBER);
                                    _scjourney.NOTE = item.Note + ".";
                                    _scjourney.CUSTOMER_CODE = items.CUSTOMER_CODE;
                                    _scjourney.WEIGHT = _ladinginfo.Weight;
                                    _scjourney.CITY = item.Location;
                                    _scjourney.COLLECT = _ladinginfo.Amount;
                                    PostDeliveryFormData(_scjourney, uri);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogAPI.LogToFile(LogFileType.EXCEPTION, "PushDeliveryToPartner: " + ex.Message);
                        }

                    }
                }
                _lstShipment = null;

                LogAPI.LogToFile(LogFileType.THREADING, "Threading Sleep " + time_sleep);
                System.Threading.Thread.Sleep(time_sleep);
                LogAPI.LogToFile(LogFileType.THREADING, "Threading Wakeup ");
            }
        }
        #region PostDelivery Form Data
        public void PostDeliveryFormData(Api.Core.Models.SC_Journey _scjourney, string uri)
        {
            Api.Core.Data.LogApiRepository _logapiRepository = new Api.Core.Data.LogApiRepository();
            Api.Core.Models.ApiLog _apilog = new Api.Core.Models.ApiLog();
            try
            {
                string postData = string.Format("username={0}&password={1}&function={2}&params[SC_CODE]={3}&params[STATUS]={4}&params[CITY]={5}&params[NOTE]={6}&params[Weight]={7}&params[COLLECT]={8}", _scjourney.username, _scjourney.password, "LichTrinh", _scjourney.SC_CODE, _scjourney.STATUS, _scjourney.CITY, _scjourney.NOTE, _scjourney.WEIGHT, _scjourney.COLLECT);

                HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(uri);
                getRequest.Method = WebRequestMethods.Http.Post;
                getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2"; // SGS Galaxy
                getRequest.AllowWriteStreamBuffering = true;
                getRequest.ProtocolVersion = HttpVersion.Version11;
                getRequest.AllowAutoRedirect = true;
                getRequest.ContentType = "application/x-www-form-urlencoded";

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                getRequest.ContentLength = byteArray.Length;
                Stream newStream = getRequest.GetRequestStream(); //open connection
                newStream.Write(byteArray, 0, byteArray.Length); // Send the data.
                newStream.Close();

                HttpWebResponse getResponse = (HttpWebResponse)getRequest.GetResponse();
                JObject _jO = new JObject();
                string _json_response = "";
                using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
                {
                    _json_response = sr.ReadToEnd();
                }
                dynamic _dynamicObj = JObject.Parse(_json_response.ToString());

                _apilog.API_KEY = "";
                _apilog.CLASS = "POST";
                _apilog.COUNT = 1;
                _apilog.CUSTOMER_CODE = _scjourney.CUSTOMER_CODE;
                _apilog.ERROR_CODE = _dynamicObj.error;
                _apilog.ERROR_MESSAGE = _dynamicObj.error_message;
                _apilog.SERVICE_NAME = "PushDeliveryToPartner";
                _apilog.FUNCTION_NAME = "PostDelivery";
                _apilog.TRACKING_CODE = _scjourney.SC_CODE;
                _apilog.STATUS = _scjourney.STATUS;
                _logapiRepository.Create_LogApi(_apilog);
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "PostDeliveryFormData: " + ex.Message);
            }
        }
        #endregion

        #region PostDelivery
        public void PostDelivery(Api.Core.Models.SC_Journey _scjourney)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Partner_Uri"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var _journey = new JourneySC
                {
                    username = _scjourney.username,
                    password = _scjourney.password,
                    Params = new Parameters
                    {
                        CITY = _scjourney.CITY,
                        COLLECT = _scjourney.COLLECT,
                        NOTE = _scjourney.NOTE,
                        SC_CODE = _scjourney.SC_CODE,
                        STATUS = _scjourney.STATUS,
                        WEIGHT = _scjourney.WEIGHT,
                    }
                };
                var json = new JavaScriptSerializer().Serialize(_journey);
                var response = client.PostAsJsonAsync("api/products", json).Result;
                // var _response = response.Content.ReadAsAsync<dynamic>().Result;
                if (response.IsSuccessStatusCode)
                {
                    // Uri gizmoUrl = response.Headers.Location;
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "PushSCDelivery: " + ex.Message);
            }
        }
        #endregion
        public string Convert_Status(string _staText, int _amount)
        {
            string _staCode = "";
            switch (_staText)
            {
                case "Đã phát hoàn thành công":
                    _staCode = "IE";
                    break;
                case "Phát hoàn thành công":
                    _staCode = "IE";
                    break;
                case "Phát thành công(PP)":
                    if (_amount > 0)
                        _staCode = "IC";
                    else
                        _staCode = "I";
                    break;
                case "Phát thành công":
                    if (_amount > 0)
                        _staCode = "IC";
                    else
                        _staCode = "I";
                    break;
                case "Đã thu tiền":
                    _staCode = "I";
                    break;
                case "Chuyển hoàn":
                    _staCode = "E";
                    break;
                case "Trả tiền cho người gửi":
                    _staCode = "I";
                    break;
                case "Phát không thành công":
                    _staCode = "H";
                    break;
                case "In phiếu nhờ thu (phát hàng)":
                    _staCode = "PD";
                    break;
                case "Phát hành yêu cầu nhờ thu":
                    _staCode = "S";
                    break;
                default:
                    _staCode = "N/A";
                    break;
            }
            return _staCode;
        }

        #region convertToUnSign2
        public string convertToUnSign2(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D'); sb = sb.Replace('à', 'a'); sb = sb.Replace('á', 'a'); sb = sb.Replace('ả', 'a');
            sb = sb.Replace('đ', 'd'); sb = sb.Replace('ã', 'a'); sb = sb.Replace('â', 'a'); sb = sb.Replace('ầ', 'a');
            sb = sb.Replace('ấ', 'a'); sb = sb.Replace('ậ', 'a'); sb = sb.Replace('ẫ', 'a'); sb = sb.Replace('ă', 'a');
            sb = sb.Replace('ằ', 'a'); sb = sb.Replace('ặ', 'a'); sb = sb.Replace('ẳ', 'a'); sb = sb.Replace('ẵ', 'a');


            sb = sb.Replace('ò', 'o'); sb = sb.Replace('ồ', 'o'); sb = sb.Replace('ờ', 'o');
            sb = sb.Replace('ó', 'o'); sb = sb.Replace('ố', 'o'); sb = sb.Replace('ớ', 'o');
            sb = sb.Replace('ọ', 'o'); sb = sb.Replace('ộ', 'o'); sb = sb.Replace('ơ', 'o');
            sb = sb.Replace('ỏ', 'o'); sb = sb.Replace('ổ', 'o'); sb = sb.Replace('ở', 'o');
            sb = sb.Replace('õ', 'o'); sb = sb.Replace('ỗ', 'o'); sb = sb.Replace('ỡ', 'o');
            sb = sb.Replace('ô', 'o'); sb = sb.Replace('ơ', 'o');

            sb = sb.Replace('è', 'e'); sb = sb.Replace('ẽ', 'e'); sb = sb.Replace('ế', 'e'); sb = sb.Replace('ể', 'e');
            sb = sb.Replace('é', 'e'); sb = sb.Replace('ê', 'e'); sb = sb.Replace('ễ', 'e');
            sb = sb.Replace('ẻ', 'e'); sb = sb.Replace('ề', 'e'); sb = sb.Replace('ệ', 'e');

            sb = sb.Replace('ì', 'i'); sb = sb.Replace('í', 'i'); sb = sb.Replace('ỉ', 'i'); sb = sb.Replace('ĩ', 'i');
            sb = sb.Replace('ị', 'i');

            sb = sb.Replace('ù', 'u'); sb = sb.Replace('ụ', 'u'); sb = sb.Replace('ũ', 'u'); sb = sb.Replace('ứ', 'u');
            sb = sb.Replace('ú', 'u'); sb = sb.Replace('ủ', 'u'); sb = sb.Replace('ư', 'u'); sb = sb.Replace('ự', 'u');
            sb = sb.Replace('ử', 'u'); sb = sb.Replace('ữ', 'u');

            sb = sb.Replace('ỳ', 'y'); sb = sb.Replace('ỵ', 'y'); sb = sb.Replace('ỹ', 'y');
            sb = sb.Replace('ý', 'y'); sb = sb.Replace('ỷ', 'y');

            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }
        #endregion
        protected override void OnStop()
        {
            LogAPI.LogToFile(LogFileType.THREADING, "On Stop service..");
        }
    }
}
