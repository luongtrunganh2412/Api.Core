﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Configuration;
namespace Api.App
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Common.TrackingHelper.TRACKING_LAST_REQUEST_DAY = DateTime.Today.DayOfYear;
            Common.TrackingHelper.TRACKING_REQUEST_COUNT = 0;
            Common.TrackingHelper.MAX_REQUEST_COUNT = int.Parse(ConfigurationManager.AppSettings["QUOTA_VALUE"]);
        }
    }
}
