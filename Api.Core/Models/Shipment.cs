using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    public class Shipment
    {
        public string MAE1 { get; set; }

        public string STC { get; set; }

        public string TRACKING_NUMBER { get; set; }

        public string ORDER_NUMBER { get; set; }

        public string CUSTOMER_CODE { get; set; }
        public int STATUSINT { get; set; }
        public string ADDITION_SERVICE { get; set; }
        public string SPECIAL_SERVICE { get; set; }
    }
    public class CShipment
    {
        public string API_KEY { get; set; }

        public int ID { get; set; }

        public string CODE { get; set; }

        public string ORDER_CODE { get; set; }

        public string TRACKING_CODE { get; set; }

        public string CUSTOMER_CODE { get; set; }

        public int STORE_ID { get; set; }

        public string SENDER_CODE { get; set; }

        public string SENDER_NAME { get; set; }

        public string SENDER_PHONE { get; set; }

        public string SENDER_EMAIL { get; set; }

        public int SENDER_PROVINCE_ID { get; set; }

        public int SENDER_DISTRICT_ID { get; set; }

        public int SENDER_WARD_ID { get; set; }

        public int SENDER_HAMLET_ID { get; set; }

        public string SENDER_STREET { get; set; }

        public string SENDER_ADDRESS { get; set; }

        public string RECEIVER_NAME { get; set; }

        public string RECEIVER_EMAIL { get; set; }

        public string RECEIVER_PHONE { get; set; }

        public int RECEIVER_PROVINCE_ID { get; set; }

        public int RECEIVER_DISTRICT_ID { get; set; }

        public int RECEIVER_WARD_ID { get; set; }

        public int RECEIVER_HAMLET_ID { get; set; }

        public string RECEIVER_STREET { get; set; }

        public string RECEIVER_ADDRESS { get; set; }

        public int PRODUCT_QUANTITY { get; set; }

        public string PRODUCT_NAME { get; set; }

        public string PRODUCT_DESCRIPTION { get; set; }

        public string STATUS { get; set; }

        public int TOTAL_AMOUNT { get; set; }

        public int WEIGHT { get; set; }

        public int SERVICE_TYPE { get; set; }

        public string CHANNEL { get; set; }

        public string PO_CREATE { get; set; }

        public string ORDER_JOIN_DATE { get; set; }

        public string REF_CODE { get; set; }

        public string FILE_NAME { get; set; }

        public string PRODUCT_CODE { get; set; }

        public string SYSTEM_ID { get; set; }

        public int COD { get; set; }

        public int PRODUCT_VALUE { get; set; }

        public int COD_FEE { get; set; }

        public int MAIN_FEE { get; set; }

        public int SERVICE_FEE { get; set; }

        public int TOTAL_FEE { get; set; }

        public string CREATE_TIME { get; set; }

        public string TO_COUNTRY { get; set; }

        public string TO_ZIPCODE { get; set; }

        public string MASTER_CODE { get; set; }

        public string ADDITION_SERVICE { get; set; }
        public string SPECIAL_SERVICE { get; set; }
    }
    public class CShipment_v2
    {
        public string API_KEY { get; set; }

        public int ID { get; set; }

        public string CODE { get; set; }

        public string ORDER_CODE { get; set; }

        public string TRACKING_CODE { get; set; }

        public string CUSTOMER_CODE { get; set; }

        public int STORE_ID { get; set; }

        public string SENDER_CODE { get; set; }

        public string SENDER_NAME { get; set; }

        public string SENDER_PHONE { get; set; }

        public string SENDER_EMAIL { get; set; }

        public int SENDER_PROVINCE_ID { get; set; }

        public int SENDER_DISTRICT_ID { get; set; }

        public int SENDER_WARD_ID { get; set; }

        public int SENDER_HAMLET_ID { get; set; }

        public string SENDER_STREET { get; set; }

        public string SENDER_ADDRESS { get; set; }

        public string RECEIVER_NAME { get; set; }

        public string RECEIVER_EMAIL { get; set; }

        public string RECEIVER_PHONE { get; set; }

        public int RECEIVER_PROVINCE_ID { get; set; }

        public int RECEIVER_DISTRICT_ID { get; set; }

        public int RECEIVER_WARD_ID { get; set; }

        public int RECEIVER_HAMLET_ID { get; set; }

        public string RECEIVER_STREET { get; set; }

        public string RECEIVER_ADDRESS { get; set; }

        public int PRODUCT_QUANTITY { get; set; }

        public string PRODUCT_NAME { get; set; }

        public string PRODUCT_DESCRIPTION { get; set; }       

        public int TOTAL_AMOUNT { get; set; }

        public int WEIGHT { get; set; }

        public int SERVICE_TYPE { get; set; }

        public string CHANNEL { get; set; }

        public string PO_CREATE { get; set; }

        public string ORDER_JOIN_DATE { get; set; }

        public string REF_CODE { get; set; }

        public string FILE_NAME { get; set; }

        public string PRODUCT_CODE { get; set; }

        public string SYSTEM_ID { get; set; }

        public int COD { get; set; }

        public int PRODUCT_VALUE { get; set; }

        public int COD_FEE { get; set; }

        public int MAIN_FEE { get; set; }

        public int SERVICE_FEE { get; set; }

        public int TOTAL_FEE { get; set; }

        public string CREATE_TIME { get; set; }

        public string TO_COUNTRY { get; set; }

        public string TO_ZIPCODE { get; set; }

        public string MASTER_CODE { get; set; }

        public string ADDITION_SERVICE { get; set; }
        public string SPECIAL_SERVICE { get; set; }
    }
    public class OC
    {
        public string Order_Code { get; set; }
    }
    public class OI
    {
        public int id { get; set; }
    }
}