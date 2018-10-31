using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Merchant.Models;
using Api.Merchant.Data;
using Newtonsoft.Json;
using System.Data;

namespace Api.Merchant.Controllers
{
    public class RegistrationController : ApiController
    {
        Response _response = new Response();
        BusinessProfileRepository businessprofile_repository = new BusinessProfileRepository();

        [AcceptVerbs("POST")]
        [Route("Registration")]
        public Response Business_Profile_User_Create(Business_Profile_Users bpu)
        {
            Response _response = new Response();
            return businessprofile_repository.CreateBusinessProfileUsers(bpu);
        }

        [AcceptVerbs("POST")]
        [Route("Registration/Login")]
        public ReturnLogin Login(Login login)
        {           
            return businessprofile_repository.Login(login);
        }

        [AcceptVerbs("POST")]
        [Route("Registration/Change_Password")]
        public Response Change_Password(Change_Password change_password)
        {
            return businessprofile_repository.Change_Password(change_password);
        }

        [AcceptVerbs("POST")]
        [Route("Registration/User")]
        public ReturnBusinessProfileUsers GetUser(User user)
        {
            return businessprofile_repository.GetUser(user);
        }
        
    }
}
