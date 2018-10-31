using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Core.Model
{
    public class Partner
    {
    }
    class Request
    {
        public string CustomerCode { get; set; }
        public string PhoneNumber { get; set; }
        public string UnitCode { get; set; }
        public string Code { get; set; }
        public string SignatureCode { get; set; }
    }
    public class Response
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class ResponseOrder
    {
        public Response Response { get; set; }
        public List<ListValue> ListData { get; set; }
        public List<CShipment> ListShipment { get; set; }
    }

    public class Rootobject
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object Value { get; set; }
        public List<ListValue> ListValue { get; set; }
        
    }
    public class ListValue
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string OrderCode { get; set; }
        public string TrackingCode { get; set; }
        public int StoreId { get; set; }
        public string SenderCode { get; set; }
        public string SenderName { get; set; }
        public string SenderPhone { get; set; }
        public string SenderEmail { get; set; }
        public int SenderProvinceId { get; set; }
        public int SenderDistrictId { get; set; }
        public int SenderWardId { get; set; }
        public int SenderHamletId { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverPhone { get; set; }
        public int ReceiverProvinceId { get; set; }
        public int ReceiveDistrictId { get; set; }
        public int ReceiveWardId { get; set; }
        public int ReceiveHamletId { get; set; }
        public string ReceiveStreet { get; set; }
        public string ReceiveAddress { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public int TotalAmount { get; set; }
        public int Weight { get; set; }
        public int ServiceType { get; set; }
        public object Channel { get; set; }
        public object Po_Create { get; set; }
        public string Order_Join { get; set; }
        public string Order_Status { get; set; }
        public string Order_Status_Desc { get; set; }
        public string FileName { get; set; }
        public string SubCustomerCode { get; set; }
        public int ProductValue { get; set; }
        public int Cod { get; set; }
        public string ToCountryCode { get; set; }
        public string ToZipCode { get; set; }
        public string RefCode { get; set; }
        public string Mae1 { get; set; }
        public string AdditionService { get; set; }
        public string SpecialService { get; set; }
    }
    public class ListData
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string OrderCode { get; set; }
        public string TrackingCode { get; set; }
        public int StoreId { get; set; }
        public string SenderCode { get; set; }
        public string SenderName { get; set; }
        public string SenderPhone { get; set; }
        public string SenderEmail { get; set; }
        public int SenderProvinceId { get; set; }
        public int SenderDistrictId { get; set; }
        public int SenderWardId { get; set; }
        public int SenderHamletId { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverPhone { get; set; }
        public int ReceiverProvinceId { get; set; }
        public int ReceiveDistrictId { get; set; }
        public int ReceiveWardId { get; set; }
        public int ReceiveHamletId { get; set; }
        public string ReceiveStreet { get; set; }
        public string ReceiveAddress { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public int TotalAmount { get; set; }
        public int Weight { get; set; }
        public int ServiceType { get; set; }
        public object Channel { get; set; }
        public object Po_Create { get; set; }
        public string Order_Join { get; set; }
        public string Order_Status { get; set; }
        public string Order_Status_Desc { get; set; }
        public string FileName { get; set; }
        public string SubCustomerCode { get; set; }
        public int ProductValue { get; set; }
        public int Cod { get; set; }
        public string ToCountryCode { get; set; }
        public string ToZipCode { get; set; }
        public string RefCode { get; set; }
        public string Mae1 { get; set; }
        public string AdditionService { get; set; }
        public string SpecialService { get; set; }
    }   
    public class ResponseShipment
    {
        public Response response { get; set; }
        public List<Shipment> data { get; set; }
    }
    public class Shipment
    {
        public string ordernumber { get; set; }
        public string trackingnumber { get; set; }
        public string servicecode { get; set; }
        public string date_s { get; set; }
        public string hour_s { get; set; }
        public string shippername { get; set; }
        public string shipperphone { get; set; }
        public string pickupaddress { get; set; }
        public string ConsigneePhone { get; set; }
        public string consigneename { get; set; }
        public string consigneeaddress { get; set; }
        public string pickupzipcode { get; set; }
        public string consigneezipcode { get; set; }
        public string description { get; set; }
        public int moneycollectamount { get; set; }
        public int volume { get; set; }
        public int usecodservice { get; set; }
        public int useinsuranceservice { get; set; }
        public int usecochecking { get; set; }
        public string status { get; set; }
        public int phan_loai { get; set; }
        public int bcnhan { get; set; }
        public int bctra { get; set; }
        public string customercode { get; set; }
        public string to_country { get; set; }
        public string to_zipcode { get; set; }
        public string  weight { get; set; }
        public string  mae1 { get; set; }
        public string addition_service { get; set; }
        public string special_service { get; set; }
    }


}