using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.App.Models
{
    public class BusinessProfile
    {
    }

    public class BusinessProfileOa
    {
        public int ID { get; set; }

        public string  CUSTOMER_CODE { get; set; }

        public string CONTACT_NAME { get; set; }

        public string CONTACT_PHONE_WORK { get; set; }

        public string GENERAL_EMAIL { get; set; }

        public int UNIT_CODE { get; set; }

        public string TOTAL_CUSTOMER_CODE { get; set; }

        public string EMPLOYEE_SALE_CODE { get; set; }

        public string CSKH_PHONE { get; set; }
    }

    public class LoginApp
    {
        public string PHONE_NUMBER { get; set; }
        public string USER_PASSWORD { get; set; }
    }

    public class ReturnLoginApp
    {
        public string   Code { get; set; }
        public string Message { get; set; }
        public BusinessProfileOa business_profile_oa { get; set; }
    }

    public class Change_Password
    {
        public string Password_Old { get; set; }

        public string Password_New { get; set; }

        public int Id { get; set; }

        public string Phone_Number { get; set; }
    }
    public class Change_Password_App
    {
        public string Password_Old { get; set; }

        public string Password_New { get; set; }    

        public string Phone_Number { get; set; }
    }
    public class Response
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

    }
}