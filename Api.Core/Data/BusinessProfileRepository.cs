using Api.Core.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Configuration;
using Api.Core.Common;
namespace Api.Core.Data
{
    public class BusinessProfileRepository
    {
        public ReponseEntity CreatBusinessProfile(Business_Profile_Channel bp, int customer_id)
        {
            ReponseEntity oReponseEntity = new ReponseEntity();
            int id = 0;
            OracleCommand cmd;
            try
            {
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "BUSINESS_PROFILE_PKG.BUSINESS_PROFILE_CREATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);                   
                    cmd.Parameters.Add("P_CONTACT_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = bp.CONTACT_NAME;
                    cmd.Parameters.Add("P_CONTACT_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = bp.CONTACT_ADDRESS;
                    cmd.Parameters.Add("P_CONTACT_PHONE_WORK", OracleDbType.Varchar2, ParameterDirection.Input).Value = bp.CONTACT_PHONE_WORK;
                    cmd.Parameters.Add("P_CUSTOMER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = bp.CUSTOMER_CODE;
                    cmd.Parameters.Add("P_CONTACT_PROVINCE", OracleDbType.Int32, ParameterDirection.Input).Value = bp.CONTACT_PROVINCE;
                    cmd.Parameters.Add("P_CONTACT_DISTRICT", OracleDbType.Int32, ParameterDirection.Input).Value = bp.CONTACT_DISTRICT;
                    cmd.Parameters.Add("P_GENERAL_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = bp.GENERAL_EMAIL;
                    cmd.Parameters.Add("P_CUSTOMER_ID", OracleDbType.Int32, ParameterDirection.Input).Value = customer_id;
                    cmd.ExecuteNonQuery();
                    id = Convert.ToInt32(cmd.Parameters["P_RETURN"].Value.ToString());                  
                    if (id > 0)
                    {
                        oReponseEntity.Code = "00";
                        oReponseEntity.Message = "Tạo mới dữ liệu thành công";
                        oReponseEntity.Value = API_KEY_GET(id);
                        oReponseEntity.Id = id;                   
                    }
                    else
                    {                        
                            oReponseEntity.Code = "-99";
                            oReponseEntity.Message = "Lỗi cập nhật dữ liệu";
                            oReponseEntity.Value = string.Empty;                        
                    }
                }


            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                oReponseEntity.Code = "-88";
                oReponseEntity.Message = "Lỗi xử lý hệ thống." + ex.Message;
            }
            return oReponseEntity;
        }
        public ReponseEntity CreatBusinessProfileRef(BusinessProfileRef oBusinessProfileRef)
        {
            ReponseEntity oReponseEntity = new ReponseEntity();
            int id = 0;
            OracleCommand cmd;
            try
            {
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "BUSINESS_PROFILE_PKG.BUSINESS_PROFILE_REF_CREATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("P_CUSTOMER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = oBusinessProfileRef.CUSTOMER_CODE;
                    cmd.Parameters.Add("P_REF_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = oBusinessProfileRef.REF_CODE;
                    cmd.Parameters.Add("P_REF_DESCRIPTION", OracleDbType.NVarchar2, ParameterDirection.Input).Value = oBusinessProfileRef.REF_DESCRIPTION;
                    cmd.Parameters.Add("P_DATE_CREATE", OracleDbType.Int32, ParameterDirection.Input).Value = oBusinessProfileRef.DATE_CREATE;
                    cmd.Parameters.Add("P_UNIT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = oBusinessProfileRef.UNIT_CODE;               

                    cmd.ExecuteNonQuery();
                    id = Convert.ToInt32(cmd.Parameters["P_RETURN"].Value.ToString());
                    if (id > 0)
                    {
                        oReponseEntity.Code = "00";
                        oReponseEntity.Message = "Cập nhật dữ liệu thành công";
                        oReponseEntity.Value = id.ToString();

                    }
                    else
                    {
                        if (id == -1)
                        {
                            oReponseEntity.Code = "-1";
                            oReponseEntity.Message = "Đã tồn tại tài khoản này";
                            oReponseEntity.Value = string.Empty;
                        }
                        else
                        {
                            oReponseEntity.Code = "-99";
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


        private string API_KEY_GET(int id)
        {
            string api_key ="";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT BP.API_KEY FROM BUSINESS_PROFILE BP WHERE BP.ID =" + id );
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        if(dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                api_key =  dr["API_KEY"].ToString();
                            }
                        }
                      
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "BusinessProfileRepository.GetBusinessProfileByKey: " + ex.Message);
                
            }
            return api_key;
        }
        private string API_KEY_GET_BY_EMAIL_PHONE(int id)
        {
            string api_key = "";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT BP.API_KEY FROM BUSINESS_PROFILE BP WHERE BP.ID =" + id);
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                api_key = dr["API_KEY"].ToString();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "BusinessProfileRepository.GetBusinessProfileByKey: " + ex.Message);

            }
            return api_key;
        }

        public BusinessProfile GetBusinessProfileByKey(string api_key)
        {
           // List<BusinessProfile> listBusinessProfile = null;
            BusinessProfile bp = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM BUSINESS_PROFILE WHERE API_KEY ='" + api_key.Trim() + "'");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                    //    listBusinessProfile = new List<BusinessProfile>();
                        while (dr.Read())
                        {
                            bp = new BusinessProfile();

                            bp.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                            bp.CONTACT_NAME = dr["CONTACT_NAME"].ToString();
                            bp.CONTACT_ADDRESS = dr["CONTACT_ADDRESS"].ToString();
                            bp.CONTACT_PROVINCE = dr["CONTACT_PROVINCE"].ToString();
                            bp.CONTACT_DISTRICT = dr["CONTACT_DISTRICT"].ToString();
                            bp.CONTACT_PHONE_WORK = dr["CONTACT_PHONE_WORK"].ToString();
                            bp.GENERAL_EMAIL = dr["GENERAL_EMAIL"].ToString();
                            bp.GENERAL_FULL_NAME = dr["GENERAL_FULL_NAME"].ToString();
                            bp.UNIT_CODE = dr["UNIT_CODE"].ToString();
                            bp.SYSTEM_REF_CODE = dr["SYSTEM_REF_CODE"].ToString();
                            bp.API_KEY = dr["API_KEY"].ToString();
                          //  listBusinessProfile.Add(bp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "BusinessProfileRepository.GetBusinessProfileByKey: " + ex.Message);
                bp = null;
            }

            return bp;
        }
        public Business_Profile Business_Profile_By_Key(string api_key)
        {
            
            Business_Profile bp = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM BUSINESS_PROFILE WHERE API_KEY ='" + api_key.Trim() + "'");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        //    listBusinessProfile = new List<BusinessProfile>();
                        while (dr.Read())
                        {
                            bp = new Business_Profile();                           
                            bp.CONTACT_NAME = dr["CONTACT_NAME"].ToString();
                            bp.CONTACT_ADDRESS = dr["CONTACT_ADDRESS"].ToString();
                            bp.CONTACT_PROVINCE = string.IsNullOrEmpty(dr["CONTACT_PROVINCE"].ToString()) ? 0 : Convert.ToInt32(dr["CONTACT_PROVINCE"].ToString()); 
                            bp.CONTACT_DISTRICT = string.IsNullOrEmpty(dr["CONTACT_DISTRICT"].ToString()) ? 0 : Convert.ToInt32(dr["CONTACT_DISTRICT"].ToString());
                            bp.CONTACT_PHONE_WORK = dr["CONTACT_PHONE_WORK"].ToString();
                            bp.GENERAL_EMAIL = dr["GENERAL_EMAIL"].ToString();
                            bp.GENERAL_FULL_NAME = dr["GENERAL_FULL_NAME"].ToString();
                            bp.CUSTOMER_ID = string.IsNullOrEmpty(dr["CUSTOMER_ID"].ToString()) ? 0 : Convert.ToInt32(dr["CUSTOMER_ID"].ToString());                      
                                                       
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "BusinessProfileRepository.Business_Profile_By_Key: " + ex.Message);
                bp = null;
            }

            return bp;
        }
        //public dynamic ApiKeyGet(string _apikey)
        //{          
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(_apikey))
        //        {
        //            dynamic _channel = Configuration.Data.Get("business_channel", Query.EQ("_id", _apikey.Trim()));                    
        //            dynamic _business_profile = Api.Core.Common.Configuration.Data.Get("business_profile", Query.EQ("_id", _channel.business_code));
        //            return _business_profile;
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
        //        return null;
        //    }
        //    return null;
        //}
    }
}