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
    public class CustomerController : ApiController
    {
        CustomerRepository oCusRep = new CustomerRepository();

        /// <summary>
        /// GetFirstCustomer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public Customer GetFirstCustomer(string Id)
        {
            string sMes = string.Empty;
            return oCusRep.GET_FIRST_CUSTOMER_BY_ACCOUNT(Id, ref sMes);
        }

        /// <summary>
        /// GetCustomerByParent
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="CusCode"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public IEnumerable<Customer> GetCustomer(string Id, string CusCode)
        {
            if (Id == null)
            {
                Id = "0";
            }
            if (CusCode == null)
            {
                CusCode = "";
            }
            string sMes = string.Empty;
            return oCusRep.GetCustomer(Id, CusCode, ref sMes);
        }
    }
}
