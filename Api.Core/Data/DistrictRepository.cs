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
    /// DistrictRepository
    /// </summary>
    public class DistrictRepository
    {
        /// <summary>
        /// Lấy danh sách Quận/Huyện
        /// </summary>
        /// <returns></returns>
        public IEnumerable<District> GetAllDistricts()
        {
            List<District> listDistrict = null;
            District district = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT D.*, P.TEN_TINH PROVINCENAME FROM BCCP_DISTRICT D, BCCP_PROVINCE P WHERE D.PROVINCECODE = P.MA_TINH ORDER BY D.DISTRICTNAME");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listDistrict = new List<District>();
                        while (dr.Read())
                        {
                            district = new District();                        
                            district.DistrictCode = dr["DISTRICTCODE"].ToString();
                            district.DistrictName = dr["DISTRICTNAME"].ToString();                            
                            district.Description = dr["DESCRIPTION"].ToString();
                            district.ProvinceCode = dr["PROVINCECODE"].ToString();
                            district.ProvinceName = dr["PROVINCENAME"].ToString().Trim();
                            listDistrict.Add(district);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "DistrictRepository.GetAllDistricts: " + ex.Message);
                listDistrict = null;
            }

            return listDistrict;
        }
        public IEnumerable<District> GetDistrictById(int id)
        {
            List<District> listDistrict = null;
            District district = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM BCCP_DISTRICT WHERE DISTRICTCODE=" + id );
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listDistrict = new List<District>();
                        while (dr.Read())
                        {
                            district = new District();

                            district.DistrictCode = dr["DISTRICTCODE"].ToString();
                            district.DistrictName = dr["DISTRICTNAME"].ToString();
                            district.ProvinceCode = dr["PROVINCECODE"].ToString();
                            district.Description = dr["DESCRIPTION"].ToString();
                            listDistrict.Add(district);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "DistrictRepository.GetAllDistricts: " + ex.Message);
                listDistrict = null;
            }

            return listDistrict;
        }
        /// <summary>
        /// Lấy danh sách Huyện của một Tỉnh theo Id
        /// </summary>
        /// <returns></returns>
        public IEnumerable<District> GetListDistrictByProvinceId(int provinceId)
        {
            List<District> listDistrict = null;
            District district = null;
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM BCCP_DISTRICT D WHERE D.PROVINCECODE = {0} ORDER BY D.DISTRICTNAME", new object[] { provinceId });
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listDistrict = new List<District>();
                        while (dr.Read())
                        {
                            district = new District();

                            district.DistrictCode = dr["DISTRICTCODE"].ToString();
                            district.DistrictName = dr["DISTRICTNAME"].ToString();
                            district.ProvinceCode = dr["PROVINCECODE"].ToString();
                            district.Description = dr["DESCRIPTION"].ToString(); 
                         
                            listDistrict.Add(district);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "DistrictRepository.GetListDistrictByProvinceId: " + ex.Message);
                listDistrict = null;
            }

            return listDistrict;
        }
    }
}