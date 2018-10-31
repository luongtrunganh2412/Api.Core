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

namespace Api.Core.Controllers
{
      [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class E1Controller : ApiController
    {
          DomesticRepository _domesticRepository = new DomesticRepository();
          [AcceptVerbs("GET")]
          public string CheckCode(string code)
          {
              if (code == null)
                  return "Code could not be null.";
              if (_domesticRepository.checkcode(code))
                  return "Congratulations!";
              else
                  return "Invalid, check again please!";
          }
    }
}
