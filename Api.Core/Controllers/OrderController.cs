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
    public class OrderController : ApiController
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
                BusinessProfile business_profile = new BusinessProfile();
                // dynamic business_profile = _mongoRepository.Get_Profile(keyHeader);
                business_profile = businessprofileRepository.GetBusinessProfileByKey(keyHeader);
                if (business_profile != null)
                {
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
                    if (oCShipment.WEIGHT == 0)
                    {
                        _response.Code = "99";
                        _response.Message = "WEIGHT null";
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
                    if (oCShipment.RECEIVER_PROVINCE_ID == 0)
                    {
                        _response.Code = "99";
                        _response.Message = "RECEIVER_PROVINCE_ID null";
                        return _response;
                    }
                    if (oCShipment.RECEIVER_DISTRICT_ID == 0)
                    {
                        _response.Code = "99";
                        _response.Message = "RECEIVER_DISTRICT_ID null";
                        return _response;
                    }
                    #endregion

                    #region SENDER
                    //if (String.IsNullOrEmpty(oCShipment.SENDER_CODE))
                    //{
                    //    oCShipment.SENDER_CODE = business_profile.system_ref_code;
                    //}                    
                    if (String.IsNullOrEmpty(oCShipment.SENDER_NAME))
                    {
                        // oCShipment.SENDER_NAME = business_profile.general_full_name.ToString();
                        oCShipment.SENDER_NAME = business_profile.GENERAL_FULL_NAME.ToString();
                    }
                    if (String.IsNullOrEmpty(oCShipment.SENDER_ADDRESS))
                    {
                        // oCShipment.SENDER_ADDRESS = business_profile.contact_address_address.ToString();
                        oCShipment.SENDER_ADDRESS = business_profile.CONTACT_ADDRESS.ToString();
                    }
                    if (String.IsNullOrEmpty(oCShipment.SENDER_PHONE))
                    {
                        // oCShipment.SENDER_PHONE = business_profile.contact_phone_mobile.ToString();
                        oCShipment.SENDER_PHONE = business_profile.CONTACT_PHONE_WORK.ToString();
                    }
                    if (String.IsNullOrEmpty(oCShipment.TO_COUNTRY))
                    {
                        if (oCShipment.SENDER_PROVINCE_ID == 0)
                        {
                            //  oCShipment.SENDER_PROVINCE_ID = int.Parse(business_profile.contact_address_province);
                            oCShipment.SENDER_PROVINCE_ID = int.Parse(business_profile.CONTACT_PROVINCE);
                        }
                        if (oCShipment.SENDER_DISTRICT_ID == 0)
                        {
                            //   oCShipment.SENDER_DISTRICT_ID = int.Parse(business_profile.contact_address_district);
                            oCShipment.SENDER_DISTRICT_ID = int.Parse(business_profile.CONTACT_DISTRICT);
                        }
                    }
                    #endregion

                    //  oCShipment.CUSTOMER_CODE = business_profile._id.ToString();
                    oCShipment.CUSTOMER_CODE = business_profile.CUSTOMER_CODE.ToString();
                    // oCShipment.PO_CREATE = _mongoRepository.Get_UserProfile(business_profile._id.ToString()).ProvinceCode;
                    oCShipment.PO_CREATE = business_profile.CONTACT_PROVINCE;
                    return _repositoryShipment.SHIPMENT_CREATE_PARTNER(oCShipment);
                }
                else
                {
                    _response.Code = "99";
                    _response.Message = "The key is invalid";
                    _response.Value = string.Empty;
                    return _response;
                }
            }
            catch(Exception ex)
            {
                _response.Code = "99";
                _response.Message = "System handling error." + ex.Message;
                _response.Value = string.Empty;

                return _response;
            }
        }

        [AcceptVerbs("POST")]
        [Route("Order/Cancel")]
        public ReponseEntity CancelOrder(OC oc)
        {
            if (!_authen.CheckAuthentication())
            {
                _response.Code = "98";
                _response.Message = "user name or password is invalid";
                return _response;
            }
            if (oc == null)
            {
                _response.Code = "100";
                _response.Message = "Id is invalid";
                return _response;
            }
            return _repositoryShipment.SHIPMENT_CANCEL(oc.Order_Code);
        }

        [AcceptVerbs("POST")]
        [Route("Order/Cancel/Id")]
        public ReponseEntity CancelOrder(OI oi)
        {
            if (!_authen.CheckAuthentication())
            {
                _response.Code = "98";
                _response.Message = "user name or password is invalid";
                return _response;
            }
            if (oi == null || oi.id == 0)
            {
                _response.Code = "100";
                _response.Message = "Id is invalid";
                return _response;
            }
            return _repositoryShipment.SHIPMENT_DELETE(oi.id);
        }


    }
}
