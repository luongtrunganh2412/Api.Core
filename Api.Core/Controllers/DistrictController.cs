using Api.Core.Data;
using Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace Api.Core.Controllers
{
    public class DistrictController : ApiController
    {     
        DistrictRepository _repositoryDistrict = new DistrictRepository();
        [HttpGet]
        public IEnumerable<District> GetAllDistricts()
        {
            return _repositoryDistrict.GetAllDistricts();
        }   
        [HttpGet]
        public IEnumerable<District> GetDistrictByProvinceId(int id)
        {
            return _repositoryDistrict.GetListDistrictByProvinceId(id);
        }
    }
}