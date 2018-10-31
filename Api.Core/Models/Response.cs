using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    public class Response
    {
        public string code { get; set; }
        public string message { get; set; }
        public int int_value { get; set; }
        public string str_value { get; set; }
    }

    public class ResponseDelivery
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Delivery> ListDelivery { get; set; }
    }  

    public class ResponseFee
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public Data data { get; set; }
        public double CalculatedFee { get; set; }
        public int WeightDimension { get; set; }
    }

    public class Data
    {
        public double TransportFee { get; set; }
        public double CoDFee { get; set; }
    }

}