using Api.Core.Common;
using Api.Core.Models;
using Api.Core.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using System.Configuration;
namespace Api.Core.Controllers
{
    public class ConnectionController : ApiController
    {
        ReponseEntity _response = new ReponseEntity();
        AuthenticationHeader _authen = new AuthenticationHeader();
        ShipmentRepository _repositoryShipment = new ShipmentRepository();
        MongoRepository _mongoRepository = new MongoRepository();
        BusinessProfileRepository businessprofileRepository = new BusinessProfileRepository();
        [AcceptVerbs("GET")]
        public bool CheckConnection()
        {
            try
            {
                var connectionString = ConfigurationManager.AppSettings["MongoServer"];
                var client = new MongoClient(connectionString);
                var server = client.GetServer();
                server.Ping();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //[AcceptVerbs("GET")]
        //public ReponseEntity DoJob(string x)
        //{
        //    ReponseEntity oReponseEntity = null;
        //    try
        //    {
        //        BusinessProfile bp = null;
        //        List<dynamic> lst;
        //        lst = new List<dynamic>();
        //        lst.AddRange(MongoHelper.List("business_channel", null));
        //        foreach (var p in lst)
        //        {
        //            dynamic business_profile = _mongoRepository.Profile(p.business_code);
        //            bp = new BusinessProfile();                    
        //            bp.CUSTOMER_CODE = business_profile._id;
        //            bp.GENERAL_ACCOUNT_TYPE = business_profile.general_account_type;
        //            bp.GENERAL_FULL_NAME = business_profile.general_full_name;
        //            bp.GENERAL_SHORT_NAME = business_profile.general_short_name;
        //            bp.CONTACT_NAME = business_profile.contact_name;
        //            bp.GENERAL_EMAIL = business_profile.general_email;
        //            bp.CONTACT_PHONE_MOBILE = business_profile.contact_phone_mobile;
        //            bp.BUSINESS_TAX = business_profile.business_tax;
        //            bp.CONTRACT = business_profile.contract;
        //            bp.CONTACT_ADDRESS = business_profile.contact_address_address;
        //            bp.CONTACT_PROVINCE = business_profile.contact_address_province;
        //            bp.CONTACT_DISTRICT = business_profile.contact_address_district;
        //            bp.STREET = business_profile.contact_address_address;
        //            bp.UNIT_CODE = business_profile.contact_address_province;
        //            bp.SYSTEM_REF_CODE = business_profile.system_ref_code;
        //            bp.API_KEY = p.api_key;
        //            oReponseEntity = new ReponseEntity();
        //            oReponseEntity = businessprofileRepository.CreatBusinessProfile(bp);
        //        }
        //        return oReponseEntity;
        //    }
        //    catch (Exception ex)
        //    {
        //        return oReponseEntity;
        //    }
        //}
    }
}
