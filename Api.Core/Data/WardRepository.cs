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
    /// WardRepository
    /// </summary>
    public class WardRepository
    {
        /// <summary>
        /// GetAllWards
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Ward> GetAllWards()
        {
            List<Ward> listWard = null;
            Ward ward = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM BCCP_COMMUNE ORDER BY COMMUNENAME");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listWard = new List<Ward>();
                        while (dr.Read())
                        {
                            ward = new Ward();
                          
                            ward.CommuneCode = dr["COMMUNECODE"].ToString();
                            ward.CommuneName = dr["COMMUNENAME"].ToString();
                            ward.DistrictCode = dr["DISTRICTCODE"].ToString();                           
                         
                            listWard.Add(ward);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "WardRepository.GetAllWards: " + ex.Message);
                listWard = null;
            }

            return listWard;
        }
        public IEnumerable<Ward> GetWardById(int Id)
        {
            List<Ward> listWard = null;
            Ward ward = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM Bccp_Commune D WHERE  D.COMMUNECODE = {0}", new object[] { Id });
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listWard = new List<Ward>();
                        while (dr.Read())
                        {
                            ward = new Ward();

                            ward.CommuneCode = dr["COMMUNECODE"].ToString();
                            ward.CommuneName = dr["COMMUNENAME"].ToString();
                            ward.DistrictCode = dr["DISTRICTCODE"].ToString();
                            listWard.Add(ward);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "WardRepository.GetWardById: " + ex.Message);
                listWard = null;
            }

            return listWard;
        }
        /// <summary>
        /// GetListWardByDistrictId
        /// </summary>
        /// <param name="districtId">districtId</param>
        /// <returns></returns>
        public IEnumerable<Ward> GetListWardByDistrictId(int districtId)
        {
            List<Ward> listWard = null;
            Ward ward = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM Bccp_Commune D WHERE  D.DISTRICTCODE = {0} ORDER BY D.CommuneNAME", new object[] { districtId });
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listWard = new List<Ward>();
                        while (dr.Read())
                        {
                            ward = new Ward();

                            ward.CommuneCode = dr["COMMUNECODE"].ToString();
                            ward.CommuneName = dr["COMMUNENAME"].ToString();
                            ward.DistrictCode = dr["DISTRICTCODE"].ToString();   
                            listWard.Add(ward);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "WardRepository.GetListWardByDistrictId: " + ex.Message);
                listWard = null;
            }

            return listWard;
        }
    }
}