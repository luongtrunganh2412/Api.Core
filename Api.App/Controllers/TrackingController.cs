using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.App.Models;
using Api.App.Data;
using System.Web.Http.Cors;
using Api.App.Common;

namespace Api.App.Controllers
{
    public class TrackingController : ApiController
    {
        TrackingRepository repository_tracking = new TrackingRepository();
        AuthenticationHeader authentication_header = new AuthenticationHeader();
        CheckingHelper ckh = new CheckingHelper();

        #region Tracking v1 ITEMCODEPOSTTOPARTNER
        [AcceptVerbs("POST")]
        [Route("api/Tracking/v1")]
        public ReturnTracking GetShipment(OrderCode oc)
        {
            ReturnTracking return_tracking = new ReturnTracking();
            if (!authentication_header.CheckAuthentication())
            {
                return_tracking.Code = "98";
                return_tracking.Message = "user name or password is invalid";
                return return_tracking;
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
                    return_tracking = repository_tracking.GET_TRACKING(oc.order_code);
                }
                else
                {
                    return_tracking.Code = "100";
                    return_tracking.Message = "More than " + Common.TrackingHelper.MAX_REQUEST_COUNT + " requests";
                }
            }
            catch (Exception ex)
            {
                return_tracking.Code = "500";
                return_tracking.Message = "Exception: " + ex.Message;
            }

            return return_tracking;
        }
        #endregion

        #region Tracking v2 ITEMCODE_KHL
        [AcceptVerbs("POST")]
        [Route("api/Tracking/v2")]
        public ReturnOrderByCode Tracking_v2(OrderCode oc)
        {
            ReturnOrderByCode _response = new ReturnOrderByCode();
            if (!authentication_header.CheckAuthentication())
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

        #region Tracking v3 E1NH_2015
        [AcceptVerbs("POST")]
        [Route("api/Tracking/v3")]
        public ReturnTracking_v3 Tracking_v3(OrderCode oc)
        {
            ReturnTracking_v3 return_tracking = new ReturnTracking_v3();
            if (!authentication_header.CheckAuthentication())
            {
                return_tracking.Code = "98";
                return_tracking.Message = "user name or password is invalid";
                return return_tracking;
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
                    return_tracking = repository_tracking.TRACKING_V3(oc.order_code);
                }
                else
                {
                    return_tracking.Code = "100";
                    return_tracking.Message = "More than " + Common.TrackingHelper.MAX_REQUEST_COUNT + " requests";
                }
            }
            catch (Exception ex)
            {
                return_tracking.Code = "500";
                return_tracking.Message = "Exception: " + ex.Message;
            }

            return return_tracking;
        }
        #endregion

        #region Tracking v4 E1NH_2015
        [AcceptVerbs("POST")]
        [Route("api/Tracking/v4")]
        public ReturnTracking_v3 Tracking_v4(OrderCode oc)
        {
            ReturnTracking_v3 return_tracking = new ReturnTracking_v3();
            if (!authentication_header.CheckAuthentication())
            {
                return_tracking.Code = "98";
                return_tracking.Message = "user name or password is invalid";
                return return_tracking;
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
                    if (ckh.Check_EmsCode(oc.order_code))
                        return_tracking = repository_tracking.TRACKING_V3(oc.order_code);
                    else
                        return_tracking = repository_tracking.TRACKING_V4(oc.order_code);
                }
                else
                {
                    return_tracking.Code = "100";
                    return_tracking.Message = "More than " + Common.TrackingHelper.MAX_REQUEST_COUNT + " requests";
                }
            }
            catch (Exception ex)
            {
                return_tracking.Code = "500";
                return_tracking.Message = "Exception: " + ex.Message;
            }

            return return_tracking;
        }
        #endregion
    }
}
