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
using System.IO;
using System.Configuration;
namespace Api.Core.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TrackingController : ApiController
    {
        ResponseDelivery response = new ResponseDelivery();
        AuthenticationHeader _authen = new AuthenticationHeader();
        DomesticRepository _reposi = new DomesticRepository();
        TrackingRepository repository_tracking = new TrackingRepository();

        [AcceptVerbs("GET")]
        public ResponseDelivery Tracking(string code)
        {
            if (!_authen.CheckAuthentication())
            {
                response.code = 98;
                response.message = "user name or password is invalid";
                return response;
            }
           
            try
            {                
                if (Common.TrackingHelper.TRACKING_LAST_REQUEST_DAY != DateTime.Today.DayOfYear)
                {
                    Common.TrackingHelper.TRACKING_LAST_REQUEST_DAY = DateTime.Today.DayOfYear;
                    Common.TrackingHelper.TRACKING_REQUEST_COUNT = 0;
                }          
                if (Common.TrackingHelper.TRACKING_REQUEST_COUNT <= Common.TrackingHelper.MAX_REQUEST_COUNT)
                {
                    Common.TrackingHelper.TRACKING_REQUEST_COUNT += 1;
                    response = _reposi.ListItemStatus(code.Trim().ToUpper());
                }
                else
                {
                    response.code = 100;
                    response.message = "More than "+Common.TrackingHelper.MAX_REQUEST_COUNT+" requests";
                }
            }
            catch(Exception ex)
            {
                response.code = 500;
                response.message = "Exception: " + ex.Message;
            }
            
            return response;
        }

        #region Tracking v2
        [AcceptVerbs("POST")]
        [Route("api/Tracking/v2")]
        public ReturnOrderByCode GetOrderByCode(OrderCode oc)
        {
            ReturnOrderByCode _response = new ReturnOrderByCode();
            if (!_authen.CheckAuthentication())
            {
                _response.Code = "98";
                _response.Message = "user name or password is invalid";
                return _response;
            }
            try
            {
                if (Common.TrackingHelper.TRACKING_LAST_REQUEST_DAY != DateTime.Today.DayOfYear)
                {
                    Common.TrackingHelper.TRACKING_LAST_REQUEST_DAY = DateTime.Today.DayOfYear;
                    Common.TrackingHelper.TRACKING_REQUEST_COUNT = 0;
                }
                if (Common.TrackingHelper.TRACKING_REQUEST_COUNT <= Common.TrackingHelper.MAX_REQUEST_COUNT)
                {
                    Common.TrackingHelper.TRACKING_REQUEST_COUNT += 1;
                    _response = repository_tracking.GetOrderByCode(oc);
                }
                else
                {
                    _response.Code = "100";
                    _response.Message = "More than " + Common.TrackingHelper.MAX_REQUEST_COUNT + " requests";
                }
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