using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    public class Lading
    {
        public string Code { get; set; }
        public string FromCountry { get; set; }
        public string ToCountry { get; set; }
        public string ReleasedDate { get; set; }
        public string ReleasedTime { get; set; }
        public string From_PO { get; set; }
        public string To_PO { get; set; }
        public string From_Name { get; set; }
        public string From_Phone { get; set; }
        public string From_Address { get; set; }
        public string To_Name { get; set; }
        public string To_Address { get; set; }
        public string To_Phone { get; set; }
        public string To_Province { get; set; }
        public string To_Province_Name { get; set; }
        public string From_Province { get; set; }
        public string From_Province_Name { get; set; }
        public int Weight { get; set; }
        public string Class { get; set; }
        public string Service { get; set; }
        public int Amount { get; set; }
        public int Transport_Fee { get; set; }
        public int Total_Fee { get; set; }
        public string Customer_Code { get; set; }
        public string Note { get; set; }
        public string Reference_Code { get; set; }
    }
    public class ReturnValue
    {
        public String Code { get; set; }
        public String Message { get; set; }
        public Lading Data { get; set; }
    }
}

