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
using Newtonsoft.Json;

namespace Api.Core.Controllers
{
    public class JourneyController : ApiController
    {
        BusinessProfileRepository businessprofileRepository = new BusinessProfileRepository();
        AuthenticationHeader _authen = new AuthenticationHeader();
        ReponseEntity _response = new ReponseEntity();
        JourneyRepository _repositoryJourney = new JourneyRepository();
        [AcceptVerbs("POST")]
        [Route("api/Journey/Create")]
        public ReponseEntity CreateJourney(dynamic _str)
        {
            HttpContext httpContext = HttpContext.Current;
            string keyHeader = httpContext.Request.Headers["key"];
            return _repositoryJourney.JOURNEY_LOG_CREATE(JsonConvert.SerializeObject(_str), keyHeader);
        }
    }
}
