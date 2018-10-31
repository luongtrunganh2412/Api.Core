using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.App.Models;
using Api.App.Data;
using System.Web.Http.Cors;
namespace Api.App.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    
    public class OrderMobileController : ApiController
    {
        OrderRepository order_repository = new OrderRepository();
        AuthenticationHeader authentication_header = new AuthenticationHeader();
        TrackingRepository repository_tracking = new TrackingRepository();
        [AcceptVerbs("POST")]
        public ReturnOrder GetOrder(OrderRequest order_request)
        {
            ReturnOrder _response = new ReturnOrder();
            if (!authentication_header.CheckAuthentication())
            {
                _response.Code = "98";
                _response.Message = "user name or password is invalid";
                return _response;
            }
            if (order_request == null)
            {
                _response.Code = "100";
                _response.Message = "Request is null";
                return _response;
            }                    
            return order_repository.GET_ORDER(order_request);
        }

        #region OrderMobile by One
        [AcceptVerbs("POST")]
        [Route("api/OrderMobile/v1")]
        public ReturnOrderByCode Order_Get_One(OrderCode oc)
        {
            ReturnOrderByCode _response = new ReturnOrderByCode();
            if (!authentication_header.CheckAuthentication())
            {
                _response.Code = "98";
                _response.Message = "user name or password is invalid";
                return _response;
            }
            if (oc == null)
            {
                _response.Code = "100";
                _response.Message = "Code is null";
                return _response;
            }
            try
            {           
               
                    _response = repository_tracking.GetOrderV1(oc);
               
            }
            catch (Exception ex)
            {
                _response.Code = "500";
                _response.Message = "Exception: " + ex.Message;
            }

            return _response;
        }
        #endregion

    }
}
