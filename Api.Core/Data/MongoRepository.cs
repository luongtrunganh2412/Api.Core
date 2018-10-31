using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Dynamic;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Text;
using Api.Core.Common;
namespace Api.Core.Data
{
    public class MongoRepository
    {
        public dynamic Get_Profile(string _apikey)
        {           
            try
            {
                if (!string.IsNullOrEmpty(_apikey))
                {
                    dynamic _channel = MongoHelper.Get("business_channel", Query.EQ("api_key", _apikey.Trim()));
                    dynamic _business_profile = MongoHelper.Get("business_profile", Query.EQ("_id", _channel.business_code));
                    return _business_profile;
                }
            }

            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                return null;
            }
            return null;
        }

        public dynamic Profile(string code)
        {
            try
            {           
                    dynamic _business_profile = MongoHelper.Get("business_profile", Query.EQ("_id", code));
                    return _business_profile;
               
            }

            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                return null;
            }
            
        }
        public dynamic Get_Profile_ByCustomerCode(string CustomerCode)
        {
            try
            {
                if (!string.IsNullOrEmpty(CustomerCode))
                {
                    //dynamic _channel = MongoHelper.Get("business_channel", Query.EQ("api_key", _apikey.Trim()));
                    dynamic _business_profile = MongoHelper.Get("business_profile", Query.EQ("_id", CustomerCode));
                    return _business_profile;
                }
            }

            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                return null;
            }
            return null;
        }
        public dynamic Get_UserProfile(string account_id)
        {
            try
            {
                if (!string.IsNullOrEmpty(account_id))
                {
                    dynamic merchant_user = MongoHelper.Get("merchant_user", Query.EQ("AccountID", account_id.Trim()));
                    return merchant_user;
                }
            }

            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                return null;
            }
            return null;
        }
    }
}