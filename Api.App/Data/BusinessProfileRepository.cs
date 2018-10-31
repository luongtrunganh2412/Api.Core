using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using Common;
using Api.App.Models;
using Api.App.Data;
namespace Api.App.Data
{
    public class BusinessProfileRepository
    {
        #region LoginApp
        public ReturnLoginApp Login(LoginApp loginapp)
        {
            ReturnLoginApp return_loginapp = new ReturnLoginApp();
            BusinessProfileOa bpo = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "BUSINESS_PROFILE_PKG.LOGIN_APP";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_PHONE_NUMBER", OracleDbType.Varchar2, ParameterDirection.Input).Value = loginapp.PHONE_NUMBER;
                    cmd.Parameters.Add("P_USER_PASSWORD", OracleDbType.Varchar2, ParameterDirection.Input).Value = loginapp.USER_PASSWORD;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                bpo = new BusinessProfileOa();
                                bpo.ID = Convert.ToInt32(dr["ID"].ToString());
                                bpo.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                                bpo.CONTACT_NAME = dr["CONTACT_NAME"].ToString();
                                bpo.CONTACT_PHONE_WORK = dr["CONTACT_PHONE_WORK"].ToString();
                                bpo.CSKH_PHONE = dr["CSKH_PHONE"].ToString();
                                bpo.EMPLOYEE_SALE_CODE = dr["EMPLOYEE_SALE_CODE"].ToString();
                                bpo.GENERAL_EMAIL = dr["GENERAL_EMAIL"].ToString();
                                bpo.TOTAL_CUSTOMER_CODE = dr["TOTAL_CUSTOMER_CODE"].ToString();
                                bpo.UNIT_CODE = Convert.ToInt32(dr["UNIT_CODE"].ToString());

                                return_loginapp.Code = "00";
                                return_loginapp.Message = "Đăng nhập thành công.";
                                return_loginapp.business_profile_oa = bpo;
                            }
                        }

                    }
                    else
                    {
                        return_loginapp.Code = float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) + "";
                        return_loginapp.Message = "Không tồn tại thông tin người dùng";
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                return_loginapp.Code = "99";
                return_loginapp.Message = "Lỗi xử lý hệ thống";
            }
            return return_loginapp;
        }
        #endregion              

        #region Change Password
        public Response Change_Password(Change_Password change_password)
        {
            Response response = new Response();
            Security se = new Security();
            int id = 0;
            OracleCommand cmd;
            try
            {
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "BUSINESS_PROFILE_PKG.CHANGE_PASSWORD";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("P_ID", OracleDbType.Int32, ParameterDirection.Input).Value = change_password.Id;
                    cmd.Parameters.Add("P_PHONE_NUMBER", OracleDbType.Varchar2, ParameterDirection.Input).Value = change_password.Phone_Number;
                    cmd.Parameters.Add("P_OLD_PASSWORD", OracleDbType.Varchar2, ParameterDirection.Input).Value = Security.MD5Hash(change_password.Password_Old + change_password.Phone_Number + "686668");
                    cmd.Parameters.Add("P_NEW_PASSWORD", OracleDbType.Varchar2, ParameterDirection.Input).Value = Security.MD5Hash(change_password.Password_New + change_password.Phone_Number + "686668");
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
                        if (id == -2)
                        {
                            response.Code = "01";
                            response.Message = "Không tồn tại tài khoản này.";
                        }
                        else
                        {
                            response.Code = "-99";
                            response.Message = "Lỗi xử lý dữ liệu";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                response.Code = "-98";
                response.Message = ex.Message;
            }
            return response;
        }
        #endregion

        #region Change Password
        public Response Change_Password_App(Change_Password_App cpa)
        {
            Response response = new Response();
            Security se = new Security();
            int id = 0;
            OracleCommand cmd;
            try
            {
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "BUSINESS_PROFILE_PKG.CHANGE_PASSWORD_APP";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    
                    cmd.Parameters.Add("P_PHONE_NUMBER", OracleDbType.Varchar2, ParameterDirection.Input).Value = cpa.Phone_Number;
                    cmd.Parameters.Add("P_OLD_PASSWORD", OracleDbType.Varchar2, ParameterDirection.Input).Value = cpa.Password_Old;  //Security.MD5Hash(change_password.Password_Old + change_password.Phone_Number + "686668");
                    cmd.Parameters.Add("P_NEW_PASSWORD", OracleDbType.Varchar2, ParameterDirection.Input).Value = cpa.Password_New; //Security.MD5Hash(change_password.Password_New + change_password.Phone_Number + "686668");
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
                        if (id == -2)
                        {
                            response.Code = "01";
                            response.Message = "Mật khẩu không chính xác.";
                        }
                        else
                        {
                            response.Code = "-99";
                            response.Message = "Lỗi xử lý dữ liệu";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                response.Code = "-98";
                response.Message = ex.Message;
            }
            return response;
        }
        #endregion
    }
}