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

namespace Api.Core.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrderWHController : ApiController
    {
        ReponseEntity _response = new ReponseEntity();
        AuthenticationHeader _authen = new AuthenticationHeader();
        ShipmentRepository _repositoryShipment = new ShipmentRepository();
        MongoRepository _mongoRepository = new MongoRepository();
        BusinessProfileRepository businessprofileRepository = new BusinessProfileRepository();
        [AcceptVerbs("POST")]
        public ReponseEntity CreateShipmentPartner(CShipment oCShipment)
        {
            if (!_authen.CheckAuthentication())
            {
                _response.Code = "98";
                _response.Message = "user name or password is invalid";
                return _response;
            }
            if (oCShipment == null)
            {
                _response.Code = "100";
                _response.Message = "Order is null";
                return _response;
            }
            HttpContext httpContext = HttpContext.Current;
            string keyHeader = httpContext.Request.Headers["key"];
            try
            {
                //  BusinessProfile business_profile = new BusinessProfile();
                // dynamic business_profile = _mongoRepos`itory.Get_Profile(keyHeader);
                //  business_profile = businessprofileRepository.GetBusinessProfileByKey(keyHeader);
                #region PRODUCT
                if (String.IsNullOrEmpty(oCShipment.PRODUCT_NAME))
                {
                    _response.Code = "99";
                    _response.Message = "PRODUCT_NAME null or empty";
                    return _response;
                }
                if (String.IsNullOrEmpty(oCShipment.ORDER_CODE))
                {
                    _response.Code = "99";
                    _response.Message = "ORDER_CODE null or empty";
                    return _response;
                }
                if (oCShipment.PRODUCT_QUANTITY == 0)
                {
                    _response.Code = "99";
                    _response.Message = "PRODUCT_QUANTITY can not be zero";
                    return _response;
                }
                #endregion

                #region RECEIVER
                if (String.IsNullOrEmpty(oCShipment.RECEIVER_NAME))
                {
                    _response.Code = "99";
                    _response.Message = "RECEIVER_NAME null or empty";
                    return _response;
                }
                if (String.IsNullOrEmpty(oCShipment.RECEIVER_ADDRESS))
                {
                    _response.Code = "99";
                    _response.Message = "RECEIVER_ADDRESS null or empty";
                    return _response;
                }
                if (String.IsNullOrEmpty(oCShipment.RECEIVER_PHONE))
                {
                    _response.Code = "99";
                    _response.Message = "RECEIVER_PHONE null or empty";
                    return _response;
                }
                #endregion

                return _repositoryShipment.SHIPMENT_CREATE_PARTNER(oCShipment);

            }
            catch
            {
                _response.Code = "99";
                _response.Message = "System handling error";
                _response.Value = string.Empty;

                return _response;
            }
        }
    }
}
