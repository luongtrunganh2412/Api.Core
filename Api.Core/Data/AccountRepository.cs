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
    public class AccountRepository
    {
        public Account AC_LOGIN(string userName, string passWord, ref string sMes)
        {
            string sReturn = string.Empty;
            string sOutput = string.Empty;
            Account oProfile;
            try
            {
                OracleCommand dbCommand = new OracleCommand("mbe.ACCOUNT_PKG.AC_LOGIN");
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add("P_USERNAME", OracleDbType.NVarchar2, userName.Trim(), ParameterDirection.Input);
                dbCommand.Parameters.Add("P_PASSWORD", OracleDbType.NVarchar2, passWord.Trim(), ParameterDirection.Input);
                dbCommand.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                dbCommand.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);

                OracleDataReader dr = Helper.ExecuteDataReader(dbCommand, Helper.ME24OracleConnection);

                if (float.Parse(dbCommand.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                {
                    oProfile = new Account();
                    while (dr.Read())
                    {
                        oProfile.Id = int.Parse("0" + dr["ID"].ToString());
                        oProfile.UserName = dr["USER_NAME"].ToString();
                        oProfile.FullName = dr["FULL_NAME"].ToString();
                        oProfile.Gender = dr["GENDER"].ToString();
                        oProfile.BirthDay = dr["BIRTH_DATE"].ToString();
                        oProfile.PIdNumber = dr["PID_NUMBER"].ToString();
                        oProfile.PhoneNumber = dr["PHONE_NUMBER"].ToString();
                        oProfile.ProvinceId = int.Parse("0" + dr["PROVINCE_ID"].ToString());
                        oProfile.DistrictId = int.Parse("0" + dr["DISTRICT_ID"].ToString());
                        oProfile.WardId = int.Parse("0" + dr["WARD_ID"].ToString());
                        oProfile.HamletId = int.Parse("0" + dr["HAMLET_ID"].ToString());
                        oProfile.Street = dr["STREET"].ToString();
                        oProfile.CustemerId = int.Parse("0" + dr["CUSTOMER_ID"].ToString());
                        oProfile.AccountLevel = dr["ACCOUNT_LEVEL"].ToString();
                    }
                }
                else
                {
                    oProfile = null;
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "AccountRepository.AC_LOGIN: " + ex.Message);
                oProfile = null;
            }

            if (oProfile == null)
            {
                sMes = "-01|Đăng nhập thất bại.";
            }
            else
            {
                sMes = "00|Đăng nhập thành công.";
            }

            return oProfile;
        }

        public List<Account> ACCOUNT_GET(string iD, string customerId, ref string sMes)
        {
            string sReturn = string.Empty;
            string sOutput = string.Empty;
            List<Account> listAccount;
            try
            {
                OracleCommand dbCommand = new OracleCommand("mbe.ACCOUNT_PKG.ACCOUNT_GET");
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add("P_ID", OracleDbType.Int64, iD.Trim(), ParameterDirection.Input);
                dbCommand.Parameters.Add("P_CUSTOMER_ID", OracleDbType.Int64, customerId.Trim(), ParameterDirection.Input);
                dbCommand.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                dbCommand.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);

                OracleDataReader dr = Helper.ExecuteDataReader(dbCommand, Helper.ME24OracleConnection);

                if (float.Parse(dbCommand.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                {
                    listAccount = new List<Account>();
                    while (dr.Read())
                    {
                        Account oProfile = new Account();
                        oProfile.Id = int.Parse("0" + dr["ID"].ToString());
                        oProfile.UserName = dr["USER_NAME"].ToString();
                        oProfile.FullName = dr["FULL_NAME"].ToString();
                        oProfile.Gender = dr["GENDER"].ToString();
                        oProfile.BirthDay = dr["BIRTH_DATE"].ToString();
                        oProfile.PIdNumber = dr["PID_NUMBER"].ToString();
                        oProfile.PhoneNumber = dr["PHONE_NUMBER"].ToString();
                        oProfile.ProvinceId = int.Parse("0" + dr["PROVINCE_ID"].ToString());
                        oProfile.DistrictId = int.Parse("0" + dr["DISTRICT_ID"].ToString());
                        oProfile.WardId = int.Parse("0" + dr["WARD_ID"].ToString());
                        oProfile.HamletId = int.Parse("0" + dr["HAMLET_ID"].ToString());
                        oProfile.Street = dr["STREET"].ToString();
                        oProfile.CustemerId = int.Parse("0" + dr["CUSTOMER_ID"].ToString());
                        oProfile.AccountLevel = dr["ACCOUNT_LEVEL"].ToString();
                        oProfile.IsLock = dr["IS_LOCK"].ToString();
                        oProfile.ProvinceName = dr["PROVINCE_NAME"].ToString();
                        oProfile.DistrictName = dr["DISTRICT_NAME"].ToString();
                        oProfile.WardName = dr["WARD_NAME"].ToString();
                        oProfile.HamletName = dr["HAMLET_NAME"].ToString();

                        listAccount.Add(oProfile);
                    }
                }
                else
                {
                    listAccount = null;
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "AccountRepository.AC_LOGIN: " + ex.Message);
                listAccount = null;
            }

            if (listAccount == null || listAccount.Count == 0)
            {
                sMes = "-01|Lấy dữ liệu thất bại.";
            }
            else
            {
                sMes = "00|Lấy dữ liệu thành công.";
            }
            
            return listAccount;
        }
        /// <summary>
        /// CreatAccount
        /// </summary>
        /// <param name="oAccount"></param>
        /// <returns></returns>
        public ReponseEntity CreatAccount(Account oAccount)
        {
            ReponseEntity oReponseEntity = new ReponseEntity();
          
            int id = 0;
            OracleCommand cmd;
                try
                {
                    using ( cmd = new OracleCommand())
                     {                          
                         cmd.Connection = Helper.ME24OracleConnection;
                         cmd.CommandText = Helper.SchemaName + "ACCOUNT_PKG.CREATE_ACCOUNT_PROFILE";
                         cmd.CommandType = CommandType.StoredProcedure;
                         cmd.Parameters.Add("P_USERNAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = oAccount.UserName;
                         cmd.Parameters.Add("P_PASSWORD", OracleDbType.Varchar2, ParameterDirection.Input).Value = oAccount.PassWord;
                         cmd.Parameters.Add("P_FULL_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = oAccount.FullName;
                         cmd.Parameters.Add("P_GENDER_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oAccount.Gender;
                         cmd.Parameters.Add("P_BIRTH_DATE", OracleDbType.Date, ParameterDirection.Input).Value = oAccount.BirthDay;
                         cmd.Parameters.Add("P_PID_NUMBER", OracleDbType.Varchar2, ParameterDirection.Input).Value = oAccount.PIdNumber;
                         cmd.Parameters.Add("P_PHONE_NUMBER", OracleDbType.Varchar2, ParameterDirection.Input).Value = oAccount.PhoneNumber;
                         cmd.Parameters.Add("P_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oAccount.ProvinceId;
                         cmd.Parameters.Add("P_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oAccount.DistrictId;
                         cmd.Parameters.Add("P_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oAccount.WardId;
                         cmd.Parameters.Add("P_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oAccount.HamletId;
                         cmd.Parameters.Add("P_STREET", OracleDbType.Varchar2, ParameterDirection.Input).Value = oAccount.Street;
                         cmd.Parameters.Add("P_PROVIDER_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oAccount.CustemerId;
                         cmd.Parameters.Add("P_ACCOUNT_LEVEL", OracleDbType.Int32, ParameterDirection.Input).Value = oAccount.AccountLevel;
                         cmd.Parameters.Add("P_ACCOUNT_GROUP_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oAccount.AccoutGroupId;
                         cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32,0, ParameterDirection.Output);

                         cmd.ExecuteNonQuery();
                         id = Convert.ToInt32(cmd.Parameters["P_RETURN_CODE"].Value);
                         if(id>0)
                         {
                             oReponseEntity.Code = "00";
                             oReponseEntity.Message = "Cập nhật dữ liệu thành công";
                             oReponseEntity.Value = id.ToString();

                         }
                         else
                         {
                             if(id==-1)
                             {
                                 oReponseEntity.Code = "-1";
                                 oReponseEntity.Message = "Đã tồn tại tài khoản này";
                                 oReponseEntity.Value = id.ToString();
                             }
                             else
                             {
                                 oReponseEntity.Code="-99";
                                 oReponseEntity.Message = "Lỗi cập nhật dữ liệu";
                                 oReponseEntity.Value = string.Empty;
                             }
                         }
                     }
            

                }
                catch (Exception ex)
                {
                    LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                    oReponseEntity = null;
                }
                return oReponseEntity;
        }
        public Result LockAccount(string Id)
        {
            Result result = new Result();
            try
            {
                OracleCommand dbCommand = new OracleCommand("mbe.ACCOUNT_PKG.ACCOUNT_LOCK");
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add("P_ID", OracleDbType.Int64, Id, ParameterDirection.Input);
                dbCommand.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);

                Helper.ExecuteNonQuery(dbCommand);


                if (float.Parse(dbCommand.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                {
                    result.bResult = true;
                    result.Code = dbCommand.Parameters["P_RETURN_CODE"].Value.ToString();
                    result.Message = "Cập nhật trạng thái thành công.";
                }
                else if (float.Parse(dbCommand.Parameters["P_RETURN_CODE"].Value.ToString()) == -1)
                {
                    result.bResult = false;
                    result.Code = dbCommand.Parameters["P_RETURN_CODE"].Value.ToString();
                    result.Message = "Không tồn tại dữ liệu cần cập nhật.";
                }
                else
                {
                    result.bResult = false;
                    result.Code = dbCommand.Parameters["P_RETURN_CODE"].Value.ToString();
                    result.Message = "Cập nhật trạng thái thất bại.";
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "AccountRepository.LockAccount: " + ex.Message);
                result.bResult = false;
                result = new Result();
                result.Code = "-99";
                result.Message = "Lỗi hệ thống " + ex.Message;
            }

            return result;
        }
    }
}