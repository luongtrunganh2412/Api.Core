using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.App.Models;
using Api.App.Data;
using System.Web;
using System.Configuration;
namespace Api.App.Controllers
{
    public class BusinessProfileController : ApiController
    {
        BusinessProfileRepository businessprofile_repository = new BusinessProfileRepository();
        AuthenticationHeader authentication_header = new AuthenticationHeader();
        [AcceptVerbs("POST")]
        [Route("api/BusinessProfile/Login")]
        public ReturnLoginApp LoginApp(LoginApp loginapp)
        {
            HttpContext httpContext = HttpContext.Current;
            string keyHeader = httpContext.Request.Headers["key"];
            ReturnLoginApp return_loginapp = new ReturnLoginApp();
            string secret_key = ConfigurationManager.AppSettings["SecretKey"];
            if (!authentication_header.CheckAuthentication())
            {
                return_loginapp.Code = "98";
                return_loginapp.Message = "user name or password is invalid";
                return return_loginapp;
            }
            if (keyHeader != secret_key)
            {
                return_loginapp.Code = "98";
                return_loginapp.Message = "Secret key is invalid";
            }
            return businessprofile_repository.Login(loginapp);
        }

        [AcceptVerbs("POST")]
        [Route("api/BusinessProfile/ChangePassword")]
        public Response ChangePasswordApp(Change_Password_App cpa)
        {
            Response response = new Response();
            HttpContext httpContext = HttpContext.Current;
            string keyHeader = httpContext.Request.Headers["key"];
            ReturnLoginApp return_loginapp = new ReturnLoginApp();
            string secret_key = ConfigurationManager.AppSettings["SecretKey"];
            if (!authentication_header.CheckAuthentication())
            {
                response.Code = "98";
                response.Message = "user name or password is invalid";
                return response;
            }
            if (keyHeader != secret_key)
            {
                return_loginapp.Code = "98";
                return_loginapp.Message = "Secret key is invalid";
                return response;
            }
            return businessprofile_repository.Change_Password_App(cpa);
        }
    }
}
