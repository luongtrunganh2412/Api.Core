using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Mc.Models
{
    public class Business_Profile
    {
    }

    public class Business_Profile_Users
    {
        public string FULL_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string EMAIL { get; set; }
        public string ACTIVE { get; set; }
        public string ROLE { get; set; }
        public string PROVINCE_CODE { get; set; }
        public string DISTRICT_CODE { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string USER_PASSWORD { get; set; }
    }
    public class Response
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

    }
}