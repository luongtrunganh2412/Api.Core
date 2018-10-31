using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebService.Core.Data;
using WebService.Core.Model;
using Newtonsoft.Json;
using System.Xml;
namespace WebService.Core
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Get_Item_Partner : System.Web.Services.WebService
    {
        [WebMethod]
        public DataSet GET_DETAIL_ORDERNUMBER(string OrderCode, string CustomerCode)
        {
            ResponseOrder responseOrder = new ResponseOrder();
            ApiRepository apiRep = new ApiRepository();
            OrderRepository orderRepository = new OrderRepository();

            OrderPNSRepository orderPNSRepository = new OrderPNSRepository();
            DataSet myDataSet = new DataSet();

            try
            {
                // Check connection
                if (!orderRepository.CheckConnection())
                {
                    Response response = new Response();
                    response.Code = "503";
                    response.Message = "Không thể kết nối đến CSDL.";

                    responseOrder.Response = response;
                    responseOrder.ListData = null;
                }
                else
                {
                    // Shipment from DC
                    responseOrder = new ResponseOrder();
                    responseOrder = orderRepository.ORDER_GET(OrderCode, CustomerCode);
                    if (responseOrder.ListData == null)
                    {
                        // Api Search Shipment from PNS
                        responseOrder = new ResponseOrder();
                        //CALL API 
                        //returnOrder = apiRep.Shipment_Search(OrderCode);
                        //CALL DATA SHIPMENT FROM PNS
                        responseOrder = orderPNSRepository.Shipment_Search(OrderCode);
                    }

                }
            }
            catch (Exception ex)
            {
                Response response = new Response();
                response.Code = "99";
                response.Message = "Lỗi trong quá trình xử lý. " + ex.Message;

                responseOrder.Response = response;
                responseOrder.ListData = null;
            }
            if (responseOrder != null)
            {
                // Convert 
                string jsonOb = JsonConvert.SerializeObject(responseOrder);
                //string jsonOb = JsonConvert.SerializeObject(SwitchTable(returnOrder));
                var xd = new XmlDocument();
                xd = JsonConvert.DeserializeXmlNode(jsonOb, "root");
                myDataSet.ReadXml(new XmlNodeReader(xd));
            }
            return myDataSet;
        }

        #region SwitchTable
        protected ResponseShipment SwitchTable(ResponseOrder return_order)
        {
            ResponseShipment response_shipment = new ResponseShipment();

            response_shipment.response = return_order.Response;
            response_shipment.response = return_order.Response;

            List<Shipment> lstTable = null;
            Shipment tb = null;

            try
            {
                lstTable = new List<Shipment>();
                foreach (var item in return_order.ListData)
                {
                    tb = new Shipment();
                    tb.ordernumber = item.OrderCode;
                    tb.trackingnumber = item.OrderCode;
                    tb.servicecode = "2";
                    tb.date_s = "0";
                    tb.hour_s = "0";
                    tb.shippername = item.SenderName;
                    tb.shipperphone = item.SenderPhone;
                    tb.pickupaddress = item.SenderAddress;
                    tb.consigneeaddress = item.ReceiveAddress;
                    tb.consigneename = item.ReceiverName + "-" + item.ReceiverPhone;
                    tb.ConsigneePhone = item.ReceiverPhone;
                    tb.pickupzipcode = item.SenderProvinceId.ToString();
                    tb.consigneezipcode = item.ReceiverProvinceId.ToString();
                    tb.description = item.ProductDescription + "-" + item.TotalAmount.ToString() + "(" + item.OrderCode + ")";
                    tb.moneycollectamount = item.TotalAmount;
                    tb.volume = 0;
                    tb.weight = item.Weight.ToString();
                    tb.usecochecking = 0;
                    tb.usecodservice = 0;
                    tb.useinsuranceservice = 0;
                    tb.status = item.StatusCode;
                    tb.mae1 = item.Mae1;
                    tb.phan_loai = 0;
                    tb.bcnhan = 0;
                    tb.bctra = 0;
                    tb.customercode = item.SenderCode;
                    tb.to_country = item.ToCountryCode;
                    tb.to_zipcode = item.ToZipCode;
                    tb.addition_service = item.AdditionService;
                    tb.special_service = item.SpecialService;
                    lstTable.Add(tb);
                }
                response_shipment.data = lstTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return response_shipment;
        }
        #endregion
    }
}
