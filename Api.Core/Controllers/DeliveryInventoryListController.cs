using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Core.Data;
using System.Dynamic;
using Newtonsoft.Json;
namespace Api.Core.Controllers
{
    public class DeliveryInventoryListController : ApiController
    {
         // GET api/values/5
        public string GetDeliveryEms(DateTime fromdate, DateTime todate)
        {
            DataSet ds = new DataSet();
            dynamic response = new ExpandoObject();
            string json_Detail = JsonConvert.SerializeObject(response);
            try
            {
                DeliveryInventory di = new DeliveryInventory();
                ds = di.GetDeliveryEmsData(fromdate, todate);
            }
            catch (Exception ex)
            {
                response.error_code = "03";
                response.error_message = "Không xử lý";
                response.data = ds;
            }
            response.error_code = "00";
            response.error_message = "Thành công";
            response.data = ds;
            return JsonConvert.SerializeObject(response);
        }

        public string PostInsertDeliveryInventoryList(DataSet ds)//(Person obj )
        {
            dynamic response = new ExpandoObject();
            string json_Detail = JsonConvert.SerializeObject(response);
            try
            {
                DeliveryInventory di = new DeliveryInventory();
                di.InsertDeliveryInventoryList(ds);              
            }
            catch (Exception) { 
            
                response.error_code = "03";
                response.error_message = "Không xử lý";                           
            }
             response.error_code = "00";
             response.error_message = "Thành công";
             return JsonConvert.SerializeObject(response);
        }
    }
}
