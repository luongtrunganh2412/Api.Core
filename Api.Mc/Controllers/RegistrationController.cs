using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Mc.Models;
using Api.Mc.Data;
namespace Api.Mc.Controllers
{
    public class RegistrationController : ApiController
    {
        Response _response = new Response();
        BusinessProfileRepository businessprofile_repository = new BusinessProfileRepository();
        [AcceptVerbs("POST")]
        public Response Business_Profile_User_Create(Business_Profile_Users bpu)
        {
            Response _response = new Response();            
            return businessprofile_repository.CreateBusinessProfile(bpu);
        }

    }
}
