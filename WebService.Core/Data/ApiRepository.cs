using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web;
using System.Net;
using System.Configuration;
using Api.Core.Common;
using WebService.Core.Model;
namespace WebService.Core.Data
{
    public class ApiRepository
    {
        #region Check connection
        public bool Check_Connection()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URI"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/Connection").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsAsync<bool>().Result;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "ApiRepository.Check_Connection: " + ex.Message);
                return false;
            }
        }

        #endregion

        #region Shipment search
        public ResponseOrder Shipment_Search(string order_code)
        {
            ResponseOrder returnOrder = new ResponseOrder();            
            Rootobject root = new Rootobject();
            Request request = new Request();
            Response response = null;
            request.Code = order_code;       

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URI"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["Username"] + ":" + ConfigurationManager.AppSettings["Password"]);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    var _response = client.PostAsJsonAsync("api/ShipmentSearch", request).Result;
                    root = _response.Content.ReadAsAsync<Rootobject>().Result;
                    if(root.Code =="00")
                    {
                        // convert error code
                        response = new Response();
                        response.Code = "200";
                        response.Message = root.Message;
                        returnOrder.Response = response;
                        // get value of object from api
                        returnOrder.ListData = root.ListValue;                     
                        
                    }
                    
                                    

                   
                }

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "ApiRepository.Shipment_Search: " + ex.Message);

                response = new Response();
                response.Code = "99";
                response.Message = "Lỗi xử lý dữ liệu." + ex.Message;
                returnOrder.Response = response;
                             
            }
            return returnOrder;
        }
        #endregion
    }
}