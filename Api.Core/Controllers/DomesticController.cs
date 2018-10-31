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
    public class DomesticController : ApiController
    {
        [AcceptVerbs("POST")]
        public Response GetDomestic_VN(Domestic _domestic)
        {
            //Domestic _domestic = new Domestic();
            DomesticRepository _reposi = new DomesticRepository();
            Response _response = new Response();
            try
            {
                switch (_domestic.function)
                {
                    //--{"Name":"Domestic_TN","Nation":"UK","Frompostcode":"100000","Topostcode":"260000","Weight":"5000","Species":"1"}
                    case "Domestic_TN":
                        _response.str_value = _reposi.Cuoc_TN(_domestic.FromProvince, _domestic.ToProvince, _domestic.Weight, _domestic.Species);
                        _response.code = "00";
                        _response.message = "Thành công";
                        break;
                    case "Domestic_TT":
                        _response.str_value = _reposi.Cuoc_TN_TT(_domestic.FromProvince, _domestic.ToProvince, _domestic.Weight);
                        _response.code = "00";
                        _response.message = "Thành công";
                        break;
                    case "Domestic_QT":
                        _response.str_value = _reposi.Cuoc_QT(_domestic.Country, _domestic.Weight, _domestic.Species);
                        _response.code = "00";
                        _response.message = "Thành công";
                        break;
                    default:
                        _response.code = "03";
                        _response.message = "Không xử lý"; break;

                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "DomesticGet" + ex.Message);
                _response = null;
            }
            return _response;
        }
    }
}
