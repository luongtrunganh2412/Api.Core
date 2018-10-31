using Api.Core.Data;
using Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace MyApi.Core.Controllers
{
    /// <summary>
    /// Province Controller
    /// </summary>
    public class ProvinceController : ApiController
    {
        ProvinceRepository _provinceRepository = new ProvinceRepository();

        /// <summary>
        /// Lấy danh sách Tỉnh/Thành phố
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Province> GetAllProvinces()
        {
            return _provinceRepository.GetAllProvinces();
        }

        [HttpGet]
        public IEnumerable<Province> GetProvincesById(int id)
        {
            return _provinceRepository.GetProvinceById(id);
        }
    }
}
