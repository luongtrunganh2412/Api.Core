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
    public class ShipmentController : ApiController
    {
        ReponseEntity _response = new ReponseEntity();       
        [AcceptVerbs("POST")]
        public ReponseEntity CreatShipment(CShipment oCShipment)
        {
            Response _response = new Response();
            ShipmentRepository _repositoryShipment = new ShipmentRepository();
            return _repositoryShipment.SHIPMENT_CREATE(oCShipment);
        }

        [AcceptVerbs("POST")]
        [Route("Shipment/CreateList")]
        public ReponseEntity CreatListShipment(List<CShipment> listCShipment)
        {
            Response _response = new Response();
            ShipmentRepository _repositoryShipment = new ShipmentRepository();
            return _repositoryShipment.LIST_SHIPMENT_CREATE(listCShipment);
        }

        [AcceptVerbs("POST")]
        public ReponseEntity CreateShipmentPartner(string key, CShipment oCShipment)
        {
            ShipmentRepository _repositoryShipment = new ShipmentRepository();
            MongoRepository _mongoRepository = new MongoRepository();
            try
            {
                dynamic business_profile = _mongoRepository.Get_Profile(key);
                if (business_profile != null)
                {
                    #region PRODUCT
                    if (String.IsNullOrEmpty(oCShipment.PRODUCT_NAME))
                    {
                        _response.Code = "11";
                        _response.Message = "PRODUCT_NAME null or empty";
                        return _response;
                    }                
                    if (oCShipment.PRODUCT_QUANTITY == 0)
                    {
                        _response.Code = "11";
                        _response.Message = "PRODUCT_QUANTITY can not be zero";
                        return _response;
                    }
                    if (oCShipment.WEIGHT == 0)
                    {
                        _response.Code = "11";
                        _response.Message = "WEIGHT null";
                        return _response;
                    }                  
                    #endregion

                    #region RECEIVER
                    if (String.IsNullOrEmpty(oCShipment.RECEIVER_NAME))
                    {
                        _response.Code = "11";
                        _response.Message = "RECEIVER_NAME null or empty";
                        return _response;
                    }
                    if (String.IsNullOrEmpty(oCShipment.RECEIVER_ADDRESS))
                    {
                        _response.Code = "11";
                        _response.Message = "RECEIVER_ADDRESS null or empty";
                        return _response;
                    }
                    if (String.IsNullOrEmpty(oCShipment.RECEIVER_PHONE))
                    {
                        _response.Code = "11";
                        _response.Message = "RECEIVER_PHONE null or empty";
                        return _response;
                    }
                    if (oCShipment.RECEIVER_PROVINCE_ID == 0)
                    {
                        _response.Code = "11";
                        _response.Message = "RECEIVER_PROVINCE_ID null";
                        return _response;
                    }
                    if (oCShipment.RECEIVER_DISTRICT_ID == 0)
                    {
                        _response.Code = "11";
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
                        oCShipment.SENDER_NAME = business_profile.general_full_name.ToString();
                    }
                    if (String.IsNullOrEmpty(oCShipment.SENDER_ADDRESS))
                    {
                        oCShipment.SENDER_ADDRESS = business_profile.contact_address_address.ToString();
                    }
                    if (String.IsNullOrEmpty(oCShipment.SENDER_PHONE))
                    {
                        oCShipment.SENDER_PHONE = business_profile.contact_phone_mobile.ToString();
                    }
                    if (String.IsNullOrEmpty(oCShipment.TO_COUNTRY))
                    {
                        if (oCShipment.SENDER_PROVINCE_ID == 0)
                        {
                            oCShipment.SENDER_PROVINCE_ID = int.Parse(business_profile.contact_address_province);
                        }
                        if (oCShipment.SENDER_DISTRICT_ID == 0)
                        {
                            oCShipment.SENDER_DISTRICT_ID = int.Parse(business_profile.contact_address_district);
                        }
                    }                    
                    #endregion
                    if (String.IsNullOrEmpty(oCShipment.MASTER_CODE))
                    {
                        oCShipment.MASTER_CODE = "-";
                    }
                    oCShipment.CUSTOMER_CODE = business_profile._id.ToString();
                    oCShipment.PO_CREATE = _mongoRepository.Get_UserProfile(business_profile._id.ToString()).ProvinceCode;                 
                    return _repositoryShipment.SHIPMENT_CREATE_PARTNER(oCShipment);
                }
                else
                {
                    _response.Code = "22";                    
                    _response.Message = "Không tồn tại key";
                    _response.Value = string.Empty;
                    return _response;
                }
            }
            catch
            {
                _response.Code = "99";
                _response.Message = "Lỗi xử lý dữ liệu";
                _response.Value = string.Empty;

                return _response;
            }
        }
        [AcceptVerbs("GET")]
        public ReturnShipment GetShipment(string code, string order_code, string status, string from_date, string to_date, string province_id, string customer_code, int page_size, int page_index)
        {
            Response _response = new Response();
            ShipmentRepository _repositoryShipment = new ShipmentRepository();
            return _repositoryShipment.SHIPMENT_GET(code, order_code, status, from_date, to_date, province_id, customer_code, page_size, page_index);
        }

        [AcceptVerbs("GET")]
        public ReturnShipment_v2 GetShipmentOne(string code)
        {
            Response _response = new Response();
            ShipmentRepository _repositoryShipment = new ShipmentRepository();
            return _repositoryShipment.SHIPMENT_GET_ONE(code);
        }
        [AcceptVerbs("GET")]
        public ReturnShipment_v2 GetShipmentOneById(int Id)
        {
            Response _response = new Response();
            ShipmentRepository _repositoryShipment = new ShipmentRepository();
            return _repositoryShipment.SHIPMENT_GET_ONE_BY_ID(Id);
        }

        [AcceptVerbs("POST")]
        [Route("Shipment/Delete")]
        public ReponseEntity ShipmentDelete(int OrderNumber, User _user)
        {
            if (_user.USER_NAME == "cpn2005" && _user.PASS_WORD == "1!2@3#")
            {
                Response _response = new Response();
                ShipmentRepository _repositoryShipment = new ShipmentRepository();
                return _repositoryShipment.SHIPMENT_DELETE(OrderNumber);
            }
            else
            {
                _response.Code = "22";
                _response.Message = "Sai user hoặc mật khẩu";
                _response.Value = string.Empty;
                return _response;
            }
        }
        [Route("Shipment/FromTMP")]
        [AcceptVerbs("POST")]
        public ReponseEntity CreateShipmentFromFile(CShipment oCShipment)
        {
            Response _response = new Response();
            ShipmentRepository _repositoryShipment = new ShipmentRepository();
            return _repositoryShipment.SHIPMENT_CREATE_FROM_FILE(oCShipment);
        }      
    }
}