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
    public class WardController : ApiController
    {

        WardRepository _repositoryWard = new WardRepository();
        [HttpGet]
        public IEnumerable<Ward> GetAllWards()
        {
            return _repositoryWard.GetAllWards();
        }
        public IEnumerable<Ward> GetListWardByDistrictId(int id)
        {
            return _repositoryWard.GetListWardByDistrictId(id);
        }
    }
}