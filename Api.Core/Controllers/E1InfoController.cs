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
    public class E1InfoController : ApiController
    {
        
        BusinessProfile business_profile = new BusinessProfile();
        BusinessProfileRepository businessprofileRepository = new BusinessProfileRepository();
        ReturnValue _response = new ReturnValue();
        EmsRepository _ems = new EmsRepository();

        //[AcceptVerbs("GET")]
        //public ReturnValue E1Infomation(string code)
        //{                      
        //    EmsRepository _ems = new EmsRepository();            
        //    return  _ems.getThongTinE1NEW(code);          
        //}


        [AcceptVerbs("GET")]
        [Route("E1InfomationV2")]
        public ReturnValue E1InfomationV2(string code)
        {
            HttpContext httpContext = HttpContext.Current;
            string keyHeader = httpContext.Request.Headers["key"];
            business_profile = businessprofileRepository.GetBusinessProfileByKey(keyHeader);
            if (business_profile != null)
            {
                _response = _ems.getThongTinE1NEW(code);
                return _response;
            }
            else
            {
                _response.Code = "99";
                _response.Message = "Api key không tồn tại";
                return _response;
            }
            

        }
    }
}