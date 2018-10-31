using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.App.Models
{
    public class Tracking
    {
        public string ITEMCODE { get; set; }
        public int WEIGHT { get; set; }
        public string ADDRESSNAME { get; set; }
        public string STATE { get; set; }
        public int TOPOSTCODE { get; set; }
        public int COLLECMONEY { get; set; }
        public string ITEMCODEPARTNER { get; set; }
        public string STATUSNOTE { get; set; }
    }

    public class Tracking_v3
    {
        public string CODE { get; set; }
        public string DATE_RECEIVE { get; set; }
        public string TIME_RECEIVE { get; set; }
        public string RECEIVER { get; set; }
        public string REASON { get; set; }
        public string SOLUTION { get; set; }
        public string POSTCODE { get; set; }
        public string STATUSCODE { get; set; }


    }

    public class ReturnTracking
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public List<Tracking> ListTracking;
    }
    public class ReturnTracking_v3
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public List<Tracking_v3> ListTracking;
    }
}