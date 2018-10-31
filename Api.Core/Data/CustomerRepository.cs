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
    public class CustomerRepository
    {
        /// <summary>
        /// GET_FIRST_CUSTOMER_BY_ACCOUNT
        /// </summary>
        /// <param name="cusId"></param>
        /// <param name="sMes"></param>
        /// <returns></returns>
        public Customer GET_FIRST_CUSTOMER_BY_ACCOUNT(string cusId, ref string sMes)
        {
            string sReturn = string.Empty;
            string sOutput = string.Empty;
            Customer oCus;
            try
            {
                OracleCommand dbCommand = new OracleCommand("ems.CUSTOMER_PKG.GET_FIRST_CUSTOMER_BY_ACCOUNT");
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add("P_CUS_ID", OracleDbType.Int64, cusId, ParameterDirection.Input);
                dbCommand.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                dbCommand.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);

                OracleDataReader dr = Helper.ExecuteDataReader(dbCommand, Helper.ME24OracleConnection);

                if (float.Parse(dbCommand.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                {
                    oCus = new Customer();
                    while (dr.Read())
                    {
                        oCus.Id = int.Parse("0" + dr["ID"].ToString());
                       
                        oCus.Name = dr["NAME"].ToString();
                        oCus.PhoneNumber = dr["PHONE_NUMBER"].ToString();
                      
                        oCus.ProvinceId = int.Parse("0" + dr["PROVINCE_ID"].ToString());
                        oCus.DistrictId = int.Parse("0" + dr["DISTRICT_ID"].ToString());
                        oCus.WardId = int.Parse("0" + dr["WARD_ID"].ToString());
                        oCus.HamletId = int.Parse("0" + dr["HAMLET_ID"].ToString());
                        oCus.Street = dr["STREET"].ToString();
                      
                    }
                }
                else
                {
                    oCus = null;
                }

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "CustomerRepository.GET_FIRST_CUSTOMER_BY_ACCOUNT: " + ex.Message);
                oCus = null;
            }

            if (oCus == null)
            {
                sMes = "-01|Lấy dữ liệu thất bại.";
            }
            else
            {
                sMes = "00|Lấy dữ liệu thành công.";
            }

            return oCus;
        }

        /// <summary>
        /// GET_CUSTOMER
        /// </summary>
        /// <param name="cusId"></param>
        /// <param name="cusParentId"></param>
        /// <param name="cusCode"></param>
        /// <param name="sMes"></param>
        /// <returns></returns>
        public List<Customer> GetCustomer(string cusId, string cusCode, ref string sMes)
        {
            string sReturn = string.Empty;
            string sOutput = string.Empty;
            List<Customer> lstCus = new List<Customer>();
            Customer oCus;
            try
            {
                OracleCommand dbCommand = new OracleCommand("mbe.CUSTOMER_PKG.GET_CUSTOMER");
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add("P_ID", OracleDbType.Int64, cusId, ParameterDirection.Input);
                //dbCommand.Parameters.Add("P_PARENT_ID", OracleDbType.Int64, cusParentId, ParameterDirection.Input);
                dbCommand.Parameters.Add("P_CUSTOMER_CODE", OracleDbType.Varchar2, cusCode, ParameterDirection.Input);
                dbCommand.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                dbCommand.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);

                OracleDataReader dr = Helper.ExecuteDataReader(dbCommand, Helper.ME24OracleConnection);

                if (float.Parse(dbCommand.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                {

                    while (dr.Read())
                    {
                                    //A.ID,
                                    //USER_NAME,
                                    //FULL_NAME,
                                    //GENDER_ID AS GENDER,
                                    //BIRTH_DATE,
                                    //PID_NUMBER,
                                    //PHONE_NUMBER,
                                    //A.PROVINCE_ID,
                                    //A.DISTRICT_ID,
                                    //A.WARD_ID,
                                    //A.HAMLET_ID,
                                    //STREET,
                                    //CUSTOMER_ID,
                                    //ACCOUNT_LEVEL,
                                    //IS_LOCK,
                                    //P.NAME        AS PROVINCE_NAME,
                                    //D.NAME        AS DISTRICT_NAME,
                                    //W.NAME        AS WARD_NAME,
                                    //H.NAME        AS HAMLET_NAME
        //                 public int Id { get; set; }

        ///// <summary>
        ///// Loại
        ///// </summary>
        //public int CustomerType { get; set; }

        ///// <summary>
        ///// Mã khách hàng
        ///// </summary>
        //public string Code { get; set; }

        ///// <summary>
        ///// Tên khách hàng
        ///// </summary>
        //public string Name { get; set; }

        ///// <summary>
        ///// Số điện thoại
        ///// </summary>
        //public string PhoneNumber { get; set; }

        ///// <summary>
        ///// Địa chỉ Email
        ///// </summary>
        //public string EmailAddress { get; set; }

        ///// <summary>
        ///// Id Tỉnh/Thành phố
        ///// </summary>
        //public int ProvinceId { get; set; }

        ///// <summary>
        ///// Tên Tỉnh/Thành phố
        ///// </summary>
        //public string ProvinceName { get; set; }

        ///// <summary>
        ///// Id Quận/Huyện
        ///// </summary>
        //public int DistrictId { get; set; }

        ///// <summary>
        ///// Tên Quận/Huyện
        ///// </summary>
        //public string DistrictName { get; set; }

        ///// <summary>
        ///// Id Xã/Phường
        ///// </summary>
        //public int WardId { get; set; }

        ///// <summary>
        ///// Tên Xã/Phường
        ///// </summary>
        //public string WardName { get; set; }

        ///// <summary>
        ///// Id Thôn/Xóm
        ///// </summary>
        //public int HamletId { get; set; }

        ///// <summary>
        ///// Tên Thôn/Xóm
        ///// </summary>
        //public string HamletName { get; set; }
        
        ///// <summary>
        ///// Địa chỉ chi tiết
        ///// </summary>
        //public string Street { get; set; }

        ///// <summary>
        ///// Khóa sử dụng
        ///// </summary>
        //public string IsLock { get; set; }


                        oCus = new Customer();
                        oCus.Id = int.Parse("0" + dr["ID"].ToString());
                       
                        oCus.Name = dr["NAME"].ToString();
                        oCus.PhoneNumber = dr["PHONE_NUMBER"].ToString();
                        oCus.ProvinceId = int.Parse("0" + dr["PROVINCE_ID"].ToString());
                        oCus.DistrictId = int.Parse("0" + dr["DISTRICT_ID"].ToString());
                        oCus.WardId = int.Parse("0" + dr["WARD_ID"].ToString());
                        oCus.HamletId = int.Parse("0" + dr["HAMLET_ID"].ToString());
                        oCus.Street = dr["STREET"].ToString();                     
                        oCus.ProvinceName = dr["PROVINCE_NAME"].ToString();
                        oCus.DistrictName = dr["DISTRICT_NAME"].ToString();
                        oCus.WardName = dr["WARD_NAME"].ToString();
                        oCus.HamletName = dr["HAMLET_NAME"].ToString();
                        oCus.IsLock = dr["IS_LOCK"].ToString();
                        lstCus.Add(oCus);
                    }
                }
                else
                {
                    lstCus = null;
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "CustomerRepository.GET_CUSTOMER_BY_PARENT: " + ex.Message);
                lstCus = null;
            }

            if (lstCus == null || lstCus.Count == 0)
            {
                sMes = "-01|Lấy dữ liệu thất bại.";
            }
            else
            {
                sMes = "00|Lấy dữ liệu thành công.";
            }

            return lstCus;
        }
    }
}