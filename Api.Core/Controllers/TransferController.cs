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
namespace Api.Core.Controllers
{
    public class TransferController : ApiController
    {
        [AcceptVerbs("GET")]

        public List<Transfer_Info> Transferring(string code)
        {
            DomesticRepository _reposi = new DomesticRepository();
            Response _response = new Response();
            return _reposi.Transfer_infomation(code.Trim().ToUpper());
        }
    }
}