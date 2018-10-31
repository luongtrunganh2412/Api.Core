using Api.Core.Common;
using Api.Core.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Api.Core.Data
{
    /// <summary>
    /// Lớp truy xuất dữ liệu cho đối tượng Employee
    /// </summary>
    public class EmployeeRepository
    {
        public string MD5(string input_string)
        {
            return Security.MD5Hash(input_string);
        }
        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Employee Login(User user)
        {
            Employee oEmployee = null;
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.ME24OracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM EMPLOYEE E WHERE E.AMND_STATE = 'A' AND E.USER_NAME = '{0}' AND E.PASSWORD = '{1}'", new string[] {
                        user.USER_NAME == null ? "" : user.USER_NAME,
                        user.PASS_WORD == null ? "" : Security.MD5Hash(user.USER_NAME + user.PASS_WORD)
                    });
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oEmployee = new Employee();                            
                            if (dr["AGENT_ID"] != null)
                                oEmployee.AgentId = Convert.ToInt32(dr["AGENT_ID"]);
                            oEmployee.EmailAddress = dr["EMAIL_ADDRESS"].ToString();
                            if (dr["EMP_GROUP_ID"] != null)
                                oEmployee.EmpGroupId = Convert.ToInt32(dr["EMP_GROUP_ID"]);
                            oEmployee.FullName = dr["FULL_NAME"].ToString();
                            if (dr["ID"] != null)
                                oEmployee.Id = Convert.ToInt32(dr["ID"].ToString());
                            oEmployee.IsLock = dr["IS_LOCK"].ToString();
                            oEmployee.Password = dr["PASSWORD"].ToString();
                            oEmployee.PhoneNumber = dr["PHONE_NUMBER"].ToString();
                            oEmployee.UserName = dr["USER_NAME"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "EmployeeRepository.Login: " + ex.Message);
                oEmployee = null;
            }

            return oEmployee;
        }
    }
}