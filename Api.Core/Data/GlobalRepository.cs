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
    /// GlobalRepository
    /// </summary>
    public class GlobalRepository
    {
        public IEnumerable<ShippingService> GetAllShippingServices()
        {
            List<ShippingService> listShippingService = null;
            ShippingService oShippingService = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.ME24OracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM SHIPPING_SERVICE S WHERE S.AMND_STATE = 'A' ORDER BY S.CODE");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listShippingService = new List<ShippingService>();
                        while (dr.Read())
                        {
                            oShippingService = new ShippingService();                            
                            if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                                oShippingService.Id = Convert.ToInt32(dr["ID"]);                            
                            oShippingService.Code = dr["CODE"].ToString();
                            oShippingService.Name = dr["NAME"].ToString();                           
                            oShippingService.IsLock = dr["IS_LOCK"].ToString();
                            listShippingService.Add(oShippingService);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GlobalRepository.GetAllShippingServices: " + ex.Message);
                listShippingService = null;
            }

            return listShippingService;
        }

        public IEnumerable<AddedService> GetAllAddedServices()
        {
            List<AddedService> listAddedService = null;
            AddedService oAddedService = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.ME24OracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM ADDED_SERVICE S WHERE S.AMND_STATE = 'A' ORDER BY S.CODE");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listAddedService = new List<AddedService>();
                        while (dr.Read())
                        {
                            oAddedService = new AddedService();
                            if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                                oAddedService.Id = Convert.ToInt32(dr["ID"]);
                            oAddedService.Code = dr["CODE"].ToString();
                            oAddedService.Name = dr["NAME"].ToString();
                            oAddedService.IsLock = dr["IS_LOCK"].ToString();
                            listAddedService.Add(oAddedService);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GlobalRepository.GetAllAddedServices: " + ex.Message);
                listAddedService = null;
            }

            return listAddedService;
        }

    }
}