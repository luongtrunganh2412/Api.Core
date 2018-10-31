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
    /// Lớp truy xuất dữ liệu Tỉnh/Thành phố
    /// </summary>
    public class ProvinceRepository
    {
        /// <summary>
        /// Lấy danh sách Tỉnh/Thành phố
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Province> GetAllProvinces()
        {
            List<Province> listProvince = null;
            Province oProvince = null;
            Exchange exchange = new Exchange();
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM BCCP_PROVINCE ORDER BY TEN_TINH");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listProvince = new List<Province>();
                        while (dr.Read())
                        {
                            oProvince = new Province();
                            oProvince.ProvinceCode = int.Parse(dr["PROVINCECODE"].ToString());
                            oProvince.ProvinceName = dr["TEN_TINH"].ToString().Trim();
                            oProvince.Description =dr["DESCRIPTION"].ToString().Trim();
                            oProvince.ProvinceCode2 = int.Parse(dr["MA_TINH"].ToString());
                            listProvince.Add(oProvince);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "ProvinceRepository.GetAllProvinces: " + ex.Message);
                listProvince = null;
            }

            return listProvince;
        }


        public IEnumerable<Province> GetProvinceById(int id)
        {
            List<Province> listProvince = null;
            Province oProvince = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM BCCP_PROVINCE WHERE MA_TINh =" + id );
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listProvince = new List<Province>();
                        while (dr.Read())
                        {
                            oProvince = new Province();
                            oProvince.ProvinceCode = int.Parse(dr["PROVINCECODE"].ToString());
                            oProvince.ProvinceName = dr["TEN_TINH"].ToString();
                            oProvince.Description = dr["DESCRIPTION"].ToString();
                            oProvince.ProvinceCode2 = int.Parse(dr["MA_TINH"].ToString());
                            listProvince.Add(oProvince);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "ProvinceRepository.GetAllProvinces: " + ex.Message);
                listProvince = null;
            }

            return listProvince;
        }
    }
}