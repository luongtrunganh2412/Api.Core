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
    /// <summary>
    /// GlobalController
    /// </summary>
    public class GlobalController : ApiController
    {
        GlobalRepository _globalRepository = new GlobalRepository();
        /// <summary>
        /// Get server time
        /// </summary>
        /// <returns></returns>
        public string GetServerTime()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        [HttpGet]
        public IEnumerable<ShippingService> GetAllShippingServices()
        {
            return _globalRepository.GetAllShippingServices();
        }

        [HttpGet]
        public IEnumerable<AddedService> GetAllAddedServices()
        {
            return _globalRepository.GetAllAddedServices();
        }
    }
}
