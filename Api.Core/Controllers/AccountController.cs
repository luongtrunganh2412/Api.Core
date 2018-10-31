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
    public class AccountController : ApiController
    {
        AccountRepository accountRepository = new AccountRepository();

        /// <summary>
        /// Tìm kiếm thông tin tài khoản
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public List<Account> GetAccount(string iD, string customerId)
        {
            if (string.IsNullOrEmpty(iD))
            {
                iD = "0";
            }

            if (string.IsNullOrEmpty(customerId))
            {
                customerId = "0";
            }

            string sMes = string.Empty;
            return accountRepository.ACCOUNT_GET(iD, customerId, ref sMes);
        }

        /// <summary>
        /// Khóa/Mở khóa tài khoản
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        public Result LockAccount(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }

            return accountRepository.LockAccount(Id);
        }

        /// <summary>
        /// Tạo tài khoản
        /// </summary>
        /// <param name="oAccount"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        public ReponseEntity CreatAccount(Account oAccount)
        {
            return accountRepository.CreatAccount(oAccount);
        }
    }
}
// Test thử
