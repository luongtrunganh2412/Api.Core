using Api.Core.Data;
using Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
namespace Api.Core.Controllers
{
    public class BusinessProfileController : ApiController
    {
        BusinessProfileRepository businessprofileRepository = new BusinessProfileRepository();
        AuthenticationHeader _authen = new AuthenticationHeader();
        ReponseEntity _response = new ReponseEntity();
        [AcceptVerbs("POST")]
        [Route("api/BusinessProfileRef")]
        public ReponseEntity CreateBusinessProfileRef(BusinessProfileRef oBusinessProfileRef)
        {
            return businessprofileRepository.CreatBusinessProfileRef(oBusinessProfileRef);
        }
        public Customer ApiKeyGet(string _apikey)
        {
           // return businessprofileRepository.CreatBusinessProfile(_apikey);
            return null;
        }

        [AcceptVerbs("GET")]
        public Customer ListCustomers()
        {
            // return businessprofileRepository.CreatBusinessProfile(_apikey);
            return null;
        }
        [AcceptVerbs("POST")]
        public ReponseEntity BusinessProfileChannel_Create(Business_Profile_Channel bpc)
        {
            
            Business_Profile business_profile = new Business_Profile();
            HttpContext httpContext = HttpContext.Current;
            if (!_authen.CheckAuthentication())
            {
                _response.Code = "98";
                _response.Message = "user name or password is invalid";
                return _response;
            }
            if (bpc == null)
            {
                _response.Code = "100";
                _response.Message = "Channel is null";
                return _response;
            }          
            string keyHeader = httpContext.Request.Headers["key"];                      
            business_profile = businessprofileRepository.Business_Profile_By_Key(keyHeader);
            if (business_profile == null)
            {
                _response.Code = "101";
                _response.Message = "Key is invalid";
                return _response;
            }
            else
            return businessprofileRepository.CreatBusinessProfile(bpc, business_profile.CUSTOMER_ID);
        }
    }
}