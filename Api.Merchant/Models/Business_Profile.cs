using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Merchant.Models
{
    public class Business_Profile
    {
    }
    public class Business_Profile_Oa
    {
        public string CONTACT_NAME { get; set; }

        public string CONTACT_PHONE_WORK { get; set; }

        public string GENERAL_EMAIL { get; set; }

        public string CONTACT_ADDRESS { get; set; }

        public int UNIT_CODE { get; set; }

        public string CONTACT_PROVINCE { get; set; }

        public string CONTACT_DISTRICT { get; set; }

    }
        public class Business_Profile_Users
    {
        public string ID { get; set; }
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
        public string CUSTOMER_CODE { get; set; }
    }
    public class Response
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

    }
    public class Login
    {
        public string Email { get; set; }

        public string Phone_Number { get; set; }

        public string Password { get; set; }

    }

    public class ReturnBusinessProfileUsers
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public Business_Profile_Users bpu { get; set; }


    }

    public class ReturnBusinessProfileOa
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public Business_Profile_Oa bpo { get; set; }


    }

    public class ReturnLogin
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public Business_Profile_Users user { get; set; }


    }
    public class Change_Password
    {
        public string Password_Old { get; set; }

        public string Password_New { get; set; }

        public int Id { get; set; }

        public string Phone_Number { get; set; }

    }

    public class User
    {
        public int Id { get; set; }
    }

    ////Phần Test API trả về dữ liệu của itemcode
    //public class Itemcode
    //{
    //    public string itemcode { get; set; }
    //}
}