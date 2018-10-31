using Api.Merchant.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using Common;

namespace Api.Merchant.Data
{
    public class BusinessProfileRepository
    {
        #region Create business profile user
        public Response CreateBusinessProfileUsers(Business_Profile_Users bpu)
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
                    cmd.CommandText = Helper.SchemaName + "BUSINESS_PROFILE_PKG.BUSINESS_PROFILE_USERS_CREATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("P_FULL_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = bpu.FULL_NAME;
                    cmd.Parameters.Add("P_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = bpu.ADDRESS;
                    cmd.Parameters.Add("P_PHONE_NUMBER", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.PHONE_NUMBER;
                    cmd.Parameters.Add("P_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.EMAIL;
                    cmd.Parameters.Add("P_PROVINCE_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.PROVINCE_CODE; // 6 characters
                    cmd.Parameters.Add("P_DISTRICT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.DISTRICT_CODE; // 6 characters
                    cmd.Parameters.Add("P_CUSTOMER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.CUSTOMER_ID;
                    cmd.Parameters.Add("P_USER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpu.USER_NAME;
                    cmd.Parameters.Add("P_USER_PASSWORD", OracleDbType.Varchar2, ParameterDirection.Input).Value = Security.MD5Hash(bpu.USER_PASSWORD + bpu.PHONE_NUMBER + "686668");
                    cmd.ExecuteNonQuery();
                    id = Convert.ToInt32(cmd.Parameters["P_RETURN"].Value.ToString());
                    if (id > 0)
                    {
                        response.Code = "00";
                        response.Message = "Đăng ký thành công";
                        response.Id = id;
                    }
                    else
                    {
                        if (id == -1)
                        {
                            response.Code = "01";
                            response.Message = "Số điện thoại hoặc email đã được sử dụng.";
                        }
                        else
                        {
                            response.Code = "-99";
                            response.Message = "Lỗi xử lý hệ thống, vui lòng liên hệ quản trị.";
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
        #endregion

        #region Login
        public ReturnLogin Login(Login login)
        {
            ReturnLogin return_login = new ReturnLogin();
            Business_Profile_Users bpu = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "BUSINESS_PROFILE_PKG.LOGIN";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_PHONE_NUMBER", OracleDbType.Varchar2, ParameterDirection.Input).Value = login.Phone_Number;
                    cmd.Parameters.Add("P_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = login.Email;
                    cmd.Parameters.Add("P_USER_PASSWORD", OracleDbType.Varchar2, ParameterDirection.Input).Value = login.Password;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {

                                bpu = new Business_Profile_Users();
                                bpu.ID = dr["ID"].ToString();
                                bpu.FULL_NAME = dr["FULL_NAME"].ToString();
                                bpu.PHONE_NUMBER = dr["PHONE_NUMBER"].ToString();
                                bpu.EMAIL = dr["EMAIL"].ToString();
                                bpu.ADDRESS = dr["ADDRESS"].ToString();
                                bpu.PROVINCE_CODE = dr["PROVINCE_CODE"].ToString();
                                bpu.DISTRICT_CODE = dr["DISTRICT_CODE"].ToString();
                                bpu.CUSTOMER_ID = dr["CUSTOMER_ID"].ToString();
                                bpu.USER_NAME = dr["USER_NAME"].ToString();
                                bpu.USER_PASSWORD = dr["USER_PASSWORD"].ToString();
                                //bpu.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                                return_login.Code = "00";
                                return_login.Message = "Đăng nhập thành công.";
                                return_login.user = bpu;
                            }
                        }
                        else
                        {
                            return_login.Code = "10";
                            return_login.Message = "Sai thông tin đăng nhập hoặc mật khẩu";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                return_login.Code = "99";
                return_login.Message = "Lỗi xử lý hệ thống";
            }
            return return_login;
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

        #region Get User
        public ReturnBusinessProfileUsers GetUser(User user)
        {
            ReturnBusinessProfileUsers return_bpu = new ReturnBusinessProfileUsers();
            Business_Profile_Users bpu = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "BUSINESS_PROFILE_PKG.GET_USER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = user.Id;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        while (dr.Read())
                        {
                            if (dr.HasRows)
                            {
                                bpu = new Business_Profile_Users();
                                bpu.ID = dr["ID"].ToString();
                                bpu.FULL_NAME = dr["FULL_NAME"].ToString();
                                bpu.PHONE_NUMBER = dr["PHONE_NUMBER"].ToString();
                                bpu.EMAIL = dr["EMAIL"].ToString();
                                bpu.ADDRESS = dr["ADDRESS"].ToString();
                                bpu.PROVINCE_CODE = dr["PROVINCE_CODE"].ToString();
                                bpu.DISTRICT_CODE = dr["DISTRICT_CODE"].ToString();
                                bpu.CUSTOMER_ID = dr["CUSTOMER_ID"].ToString();
                                bpu.USER_NAME = dr["USER_NAME"].ToString();
                                
                                //bpu.USER_PASSWORD = dr["USER_PASSWORD"].ToString();
                                return_bpu.Code = "00";
                                return_bpu.Message = "Đăng nhập thành công.";
                                return_bpu.bpu = bpu;
                            }
                            else
                            {
                                return_bpu.Code = "10";
                                return_bpu.Message = "Sai thông tin đăng nhập hoặc mật khẩu";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                return_bpu.Code = "99";
                return_bpu.Message = "Lỗi xử lý hệ thống";
            }
            return return_bpu;
        }
        #endregion

        #region Create business profile oa
        public Response CreateBusinessProfileOa(Business_Profile_Oa bpo)
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
                    cmd.CommandText = Helper.SchemaName + "BUSINESS_PROFILE_PKG.BUSINESS_PROFILE_OA_CREATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("P_CONTACT_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = bpo.CONTACT_NAME;
                    cmd.Parameters.Add("P_CONTACT_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = bpo.CONTACT_ADDRESS;
                    cmd.Parameters.Add("P_CONTACT_PHONE_WORK", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpo.CONTACT_PHONE_WORK;
                    cmd.Parameters.Add("P_GENERAL_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpo.GENERAL_EMAIL;
                    cmd.Parameters.Add("P_CONTACT_PROVINCE", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpo.CONTACT_PROVINCE; // 6 characters
                    cmd.Parameters.Add("P_CONTACT_DISTRICT", OracleDbType.Varchar2, ParameterDirection.Input).Value = bpo.CONTACT_DISTRICT; // 6 characters
                    cmd.Parameters.Add("P_UNIT_CODE", OracleDbType.Int32, ParameterDirection.Input).Value = bpo.UNIT_CODE;
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
                            response.Message = "Số điện thoại hoặc email đã được sử dụng.";
                        }
                        else
                        {
                            response.Code = "-99";
                            response.Message = "Lỗi xử lý hệ thống, vui lòng liên hệ quản trị.";
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
        #endregion

    }
}