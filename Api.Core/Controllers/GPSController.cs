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
    public class GPSController : ApiController
    {
        ReponseEntity _response = new ReponseEntity();
        [AcceptVerbs("POST")]
        public ReponseEntity CreateGPSJourney(string key, GPS gps)
        {
            GPSRepository _repositorygps = new GPSRepository();
           
            try
            {               
                if (key == "1333dfab-4585-40f1-8508-c19b0ecd52a4")
                {
                                                      
                    return _repositorygps.GPS_CREATE_JOURNEY(gps);
                }
                else
                {
                    _response.Code = "22";
                    _response.Message = "Không tồn tại key";
                    _response.Value = string.Empty;
                    return _response;
                }
            }
            catch
            {
                _response.Code = "99";
                _response.Message = "Lỗi xử lý dữ liệu";
                _response.Value = string.Empty;

                return _response;
            }
        }
    }
}
