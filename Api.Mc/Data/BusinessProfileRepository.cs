using Api.Mc.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using Common;
namespace Api.Mc.Data
{
    public class BusinessProfileRepository
    {
        public Response CreateBusinessProfile(Business_Profile_Users bpu)
        {
            Response response = new Response();
            int id = 0;
            OracleCommand cmd;
            try
            {
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "BUSINESS_PROFILE_PKG.BUSINESS_PROFILE_USERS_CREATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);                    
                    cmd.Parameters.Add("P_FULL_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = bpu.FULL_NAME;
                    cmd.Parameters.Add("P_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = bpu.ADDRESS;
                    cmd.Parameters.Add("P_PHONE_NUMBER", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.ADDRESS;
                    cmd.Parameters.Add("P_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.EMAIL;
                    cmd.Parameters.Add("P_PROVINCE_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.PROVINCE_CODE;
                    cmd.Parameters.Add("P_DISTRICT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.DISTRICT_CODE;
                    cmd.Parameters.Add("P_CUSTOMER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.CUSTOMER_ID;
                    cmd.Parameters.Add("P_USER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.USER_NAME;
                    cmd.Parameters.Add("P_USER_PASSWORD", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.USER_PASSWORD;             
                    cmd.ExecuteNonQuery();
                    id = Convert.ToInt32(cmd.Parameters["P_RETURN"].Value.ToString());                  
                    if (id > 0)
                    {
                        response.Code = "00";
                        response.Message = "Cập nhật dữ liệu thành công";
                        response.Id = id;
                    }
                    else
                    {
                        if (id == -1)
                        {
                            response.Code = "01";
                            response.Message = "Đã tồn tại tài khoản này";                          
                        }
                        else
                        {
                            response.Code = "-99";
                            response.Message = "Lỗi xử lý dữ liệu";
                            response.Id = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                response.Code = "-98";
                response.Message = ex.Message;
                response.Id = 0;
            }
            return response;
        }    

     
    }
}