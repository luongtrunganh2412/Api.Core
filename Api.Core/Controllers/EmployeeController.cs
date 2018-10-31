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
    /// Employee
    /// </summary>
    public class EmployeeController : ApiController
    {
        EmployeeRepository _employeeRepository = new EmployeeRepository();
        AccountRepository accountRepository = new AccountRepository();
        AuthenticationHeader _authen = new AuthenticationHeader();
        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <param name="user">Thông tin tài khoản</param>
        /// <returns></returns>
        [ActionName("Login")]
        public Employee Login(User user)
        {
            return _employeeRepository.Login(user);
        }

        /// <summary>
        /// Đăng nhập hệ thống WebApp
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]        
        public Account AccountLogin(string username,string password)
        {
            string sMes = string.Empty;
            return accountRepository.AC_LOGIN(username, password, ref sMes);
        }

        [AcceptVerbs("GET")]
        public string MD5(string input_string)
        {
            try
            {
                if (!_authen.CheckAuthentication())
                {
                    return "Authentication Error";
                }
                else
                    return _employeeRepository.MD5(input_string);
            }
            catch
            {
                return "Processing Error";
            }
        
        }
        // partner6688 / ndt401        
    }
}
