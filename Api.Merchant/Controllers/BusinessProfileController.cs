using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Merchant.Models;
using Api.Merchant.Data;

namespace Api.Merchant.Controllers
{
    
    public class BusinessProfileController : ApiController
    {
        BusinessProfileRepository businessprofile_repository = new BusinessProfileRepository();
        [AcceptVerbs("POST")]
        [Route("BusinessProfileOa")]
        public Response Business_Profile_Oa_Create(Business_Profile_Oa bpo)
        {
            Response _response = new Response();
            return businessprofile_repository.CreateBusinessProfileOa(bpo);
        }
    }
}
