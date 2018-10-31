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
    public class StoreRepository
    {
        public List<Store> GET_STORE(string Id, string cusId, ref string sMes)
        {
            string sReturn = string.Empty;
            string sOutput = string.Empty;
            List<Store> lstStore = new List<Store>();
            Store oStore;
            try
            {
                OracleCommand dbCommand = new OracleCommand("mbe.STORE_PKG.GET_STORE");
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add("P_ID", OracleDbType.Int64, Id, ParameterDirection.Input);
                dbCommand.Parameters.Add("P_CUSTOMER_ID", OracleDbType.Varchar2, cusId, ParameterDirection.Input);
                dbCommand.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                dbCommand.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                OracleDataReader dr = Helper.ExecuteDataReader(dbCommand,Helper.ME24OracleConnection);
                if (float.Parse(dbCommand.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                {
                    while (dr.Read())
                    {
                        oStore = new Store();
                        oStore.Id = int.Parse("0" + dr["ID"].ToString());
                        oStore.Code = dr["STORE_CODE"].ToString();
                        oStore.CustomerId = int.Parse("0" + dr["CUSTOMER_ID"].ToString());
                        oStore.ProvinceId = int.Parse("0" + dr["PROVINCE_ID"].ToString());
                        oStore.DistrictId = int.Parse("0" + dr["DISTRICT_ID"].ToString());
                        oStore.WardId = int.Parse("0" + dr["WARD_ID"].ToString());
                        oStore.HamletId = int.Parse("0" + dr["HAMLET_ID"].ToString());
                        oStore.Street = dr["STREET"].ToString();
                        oStore.ContactName = dr["CONTACT_NAME"].ToString();
                        oStore.ContactMobile = dr["CONTACT_MOBILE"].ToString();
                        oStore.ContactEmail = dr["CONTACT_EMAIL"].ToString();
                        oStore.IsLock = dr["IS_LOCK"].ToString();
                        oStore.ProvinceName = dr["PROVINCE_NAME"].ToString();
                        oStore.DistrictName = dr["DISTRICT_NAME"].ToString();
                        oStore.WardName = dr["WARD_NAME"].ToString();
                        oStore.HamletName = dr["HAMLET_NAME"].ToString();
                        oStore.Name = dr["STORE_NAME"].ToString();

                        lstStore.Add(oStore);
                    }
                }
                else
                {
                    lstStore = null;
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "CustomerRepository.GET_STORE: " + ex.Message);
                lstStore = null;
            }

            if (lstStore == null || lstStore.Count == 0)
            {
                sMes = "-01|Lấy dữ liệu thất bại.";
            }
            else
            {
                sMes = "00|Lấy dữ liệu thành công.";
            }

            return lstStore;
        }

        public Result LockStore(string Id)
        {
            Result result = new Result();
            try
            {
                OracleCommand dbCommand = new OracleCommand("mbe.STORE_PKG.STORE_LOCK");
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
                else if(float.Parse(dbCommand.Parameters["P_RETURN_CODE"].Value.ToString()) == -1)
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
                LogAPI.LogToFile(LogFileType.EXCEPTION, "CustomerRepository.GET_STORE: " + ex.Message);
                result.bResult = false;
                result = new Result();
                result.Code = "-99";
                result.Message = "Lỗi hệ thống " + ex.Message;
            }

            return result;
        }
        /// <summary>
        /// CreatStore
        /// </summary>
        /// <param name="oStore"></param>
        /// <returns></returns>
     public ReponseEntity CreatStore(Store oStore)
        {
            ReponseEntity oReponseEntity = new ReponseEntity();

            int id = 0;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                            //    P_AMND_USER               NUMBER,
                            //    P_STORE_CODE              VARCHAR2,
                            //    P_STORE_NAME              NVARCHAR2,
                            //    P_CUSTOMER_ID             NUMBER,cmd
                            //    P_PROVINCE_ID             NUMBER,
                            //    P_DISTRICT_ID             NUMBER,
                            //    P_WARD_ID                 NUMBER,
                            //    P_HAMLET_ID               NUMBER,
                            //    P_STREET                  NVARCHAR2,
                            //    P_CONTACT_NAME            NVARCHAR2,
                            //    P_CONTACT_MOBILE          NVARCHAR2,
                            //    P_CONTACT_EMAIL           NVARCHAR2,
                            //    P_IS_LOCK                 VARCHAR2,
                            //    P_RETURN_CODE             OUT NUMBER
                    cmd.Connection = Helper.ME24OracleConnection;
                    cmd.CommandText = Helper.SchemaName + "STORE_PKG.STORE_CREATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_AMND_USER", OracleDbType.Int32, ParameterDirection.Input).Value = oStore.AmndUser;
                    cmd.Parameters.Add("P_STORE_CODE", OracleDbType.NVarchar2, ParameterDirection.Input).Value = oStore.Code;
                    cmd.Parameters.Add("P_STORE_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = oStore.Name;
                    cmd.Parameters.Add("P_CUSTOMER_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oStore.CustomerId;
                    cmd.Parameters.Add("P_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oStore.ProvinceId;
                    cmd.Parameters.Add("P_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oStore.DistrictId;
                    cmd.Parameters.Add("P_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oStore.WardId;
                    cmd.Parameters.Add("P_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oStore.HamletId;
                    cmd.Parameters.Add("P_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = oStore.Street;
                    cmd.Parameters.Add("P_CONTACT_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = oStore.ContactName;
                    cmd.Parameters.Add("P_CONTACT_MOBILE", OracleDbType.NVarchar2, ParameterDirection.Input).Value = oStore.ContactMobile;
                    cmd.Parameters.Add("P_CONTACT_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = oStore.ContactEmail;
                    cmd.Parameters.Add("P_IS_LOCK", OracleDbType.Varchar2, ParameterDirection.Input).Value = oStore.IsLock;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.ExecuteNonQuery();
                    id = Convert.ToInt32(cmd.Parameters["P_RETURN_CODE"].Value.ToString());
                    if(id>0)
                    {
                        oReponseEntity.Code = "00";
                        oReponseEntity.Message = "Cập nhật dữ liệu thành công";
                        oReponseEntity.Value = id.ToString();
                    }
                    else
                        if(id==-1)
                        {
                            oReponseEntity.Code = "-1";
                            oReponseEntity.Message = "Đã tồn tại tài khoản này";
                            oReponseEntity.Value = id.ToString();

                        }
                        else
                        {
                            oReponseEntity.Code = "-99";
                            oReponseEntity.Message = "Lỗi cập nhật hệ thống";
                            oReponseEntity.Value = string.Empty;
                        }
                }
            }
         catch(Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                oReponseEntity = null;
            }
            return oReponseEntity;

        }
        /// <summary>
        /// Update Store
        /// </summary>
        /// <param name="oStore"></param>
        /// <returns></returns>
        public ReponseEntity UpdateStore(Store oStore)
     {
         ReponseEntity oReponseEntity = new ReponseEntity();
         int id = 0;
         try
         {
             using(OracleCommand cmd = new OracleCommand())
             {
                    //     P_ID             NUMBER,
                    //     P_STORE_CODE     VARCHAR2,
                    //     P_STORE_NAME     NVARCHAR2,
                    //     P_CUSTOMER_ID    NUMBER,
                    //     P_PROVINCE_ID    NUMBER,
                    //     P_DISTRICT_ID    NUMBER,
                    //     P_WARD_ID        NUMBER,
                    //     P_HAMLET_ID      NUMBER,
                    //     P_STREET         NVARCHAR2,
                    //     P_CONTACT_NAME   VARCHAR2,
                    //     P_CONTACT_MOBILE VARCHAR2,
                    //     P_CONTACT_EMAIL  NVARCHAR2,
                    //     P_IS_LOCK        VARCHAR2)
                 cmd.Connection = Helper.ME24OracleConnection;
                 cmd.CommandText = Helper.SchemaName + "STORE_PKG.STORE_UPDATE";
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.Add("P_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oStore.Id;
                 cmd.Parameters.Add("P_STORE_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = oStore.Code;
                 cmd.Parameters.Add("P_STORE_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = oStore.Name;
                 cmd.Parameters.Add("P_CUSTOMER_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oStore.CustomerId;
                 cmd.Parameters.Add("P_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oStore.ProvinceId;
                 cmd.Parameters.Add("P_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oStore.DistrictId;
                 cmd.Parameters.Add("P_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oStore.WardId;
                 cmd.Parameters.Add("P_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = oStore.HamletId;
                 cmd.Parameters.Add("P_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = oStore.Street;
                 cmd.Parameters.Add("P_CONTACT_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = oStore.ContactName;
                 cmd.Parameters.Add("P_CONTACT_MOBILE", OracleDbType.NVarchar2, ParameterDirection.Input).Value = oStore.ContactMobile;
                 cmd.Parameters.Add("P_CONTACT_EMAIL", OracleDbType.NVarchar2, ParameterDirection.Input).Value = oStore.ContactEmail;
                 //cmd.Parameters.Add("P_IS_LOCK", OracleDbType.NVarchar2, ParameterDirection.Input).Value = oStore.IsLock;
                 cmd.Parameters.Add("P_RETURN_CODE",OracleDbType.Int32,ParameterDirection.Output);
                 cmd.ExecuteNonQuery();
                 id = Convert.ToInt32(cmd.Parameters["P_RETURN_CODE"].Value.ToString());
                 if(id > 0 )
                 {
                     oReponseEntity.Code = "00";
                     oReponseEntity.Message = "Cập nhật thành công";
                     oReponseEntity.Value = id.ToString();
                 }
                 else
                 {
                     oReponseEntity.Code = "-99";
                     oReponseEntity.Message = "Cập nhật thất bại, Vui lòng thử lại";
                     oReponseEntity.Value = string.Empty;
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


    }
}