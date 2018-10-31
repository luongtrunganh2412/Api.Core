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
namespace Api.Core.Controllers
{
    public class ShipmentTMPController : ApiController
    {
        ReponseEntity _response = new ReponseEntity();
        // GET api/<controller>
        [AcceptVerbs("POST")]
        public ReponseEntity CreatShipment(CShipment oCShipment)
        {
            ShipmentRepository _repositoryShipment = new ShipmentRepository();
            MongoRepository _mongoRepository = new MongoRepository();
            try
            {
                // Phần này chưa lên DB, xem lại
                dynamic business_profile = _mongoRepository.Get_Profile_ByCustomerCode(oCShipment.CUSTOMER_CODE);
                #region SENDER
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
                if (oCShipment.SENDER_PROVINCE_ID == 0)
                {
                    oCShipment.SENDER_PROVINCE_ID = int.Parse(business_profile.contact_address_province);
                }
                if (oCShipment.SENDER_DISTRICT_ID == 0)
                {
                    oCShipment.SENDER_DISTRICT_ID = int.Parse(business_profile.contact_address_district);
                }
                #endregion

                #region RECEIVER
                if (String.IsNullOrEmpty(oCShipment.RECEIVER_NAME))
                {
                    return _response;
                }
                if (String.IsNullOrEmpty(oCShipment.RECEIVER_ADDRESS))
                {
                    return _response;
                }
                if (String.IsNullOrEmpty(oCShipment.RECEIVER_PHONE))
                {
                    return _response;
                }
                if (String.IsNullOrEmpty(oCShipment.PRODUCT_NAME))
                {
                    return _response;
                }
                if (oCShipment.WEIGHT == 0)
                {
                    return _response;
                }
                if (oCShipment.RECEIVER_PROVINCE_ID == 0)
                {
                    return _response;
                }
                if (oCShipment.RECEIVER_DISTRICT_ID == 0)
                {
                    return _response;
                }
                #endregion

                return _repositoryShipment.SHIPMENT_TMP_CREATE(oCShipment);
            }
            catch (Exception)
            {
                _response.Code = "99";
                _response.Message = "Lỗi xử lý dữ liệu";
                _response.Value = string.Empty;

                return _response;
            }
        }
        public ReturnShipment GetShipmentOne(int id)
        {
            Response _response = new Response();
            ShipmentRepository _repositoryShipment = new ShipmentRepository();
            return _repositoryShipment.SHIPMENT_TMP_GET_ONE(id);
        }

        [AcceptVerbs("GET")]
        public ReturnShipment GetShipment(string from_date, string to_date, string customer_code, int page_size, int page_index, string filename)
        {
            Response _response = new Response();
            ShipmentRepository _repositoryShipment = new ShipmentRepository();
            return _repositoryShipment.SHIPMENT_TMP_GET(from_date, to_date, customer_code, page_size, page_index, filename, "");
        }
        [AcceptVerbs("GET")]
        public ReturnShipment GetShipmentUpdate(string from_date, string to_date, string customer_code, int page_size, int page_index, string filename, string update)
        {
            Response _response = new Response();
            ShipmentRepository _repositoryShipment = new ShipmentRepository();
            return _repositoryShipment.SHIPMENT_TMP_GET(from_date, to_date, customer_code, page_size, page_index, filename, update);
        }
        [AcceptVerbs("POST")]
        [Route("ShipmentTMP/Delete")]
        public ReponseEntity ShipmentDelete(int id, User _user)
        {
            if (_user.USER_NAME == "cpn2005" && _user.PASS_WORD == "1!2@3#")
            {
                Response _response = new Response();
                ShipmentRepository _repositoryShipment = new ShipmentRepository();
                return _repositoryShipment.SHIPMENT_TMP_DELETE(id);
            }
            else
            {
                _response.Code = "22";
                _response.Message = "Sai user hoặc mật khẩu";
                _response.Value = string.Empty;
                return _response;
            }
        }


        [AcceptVerbs("POST")]
        [Route("ShipmentTMP/Update")]
        public ReponseEntity ShipmentTMP_Update(CShipment oCShipment)
        {           
                Response _response = new Response();
                ShipmentRepository _repositoryShipment = new ShipmentRepository();
                return _repositoryShipment.SHIPMENT_TMP_UPDATE(oCShipment);
        }
    }
}