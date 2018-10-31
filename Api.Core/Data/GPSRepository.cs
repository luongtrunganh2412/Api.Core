using Api.Core.Common;
using Api.Core.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Services;
using Newtonsoft.Json;
namespace Api.Core.Data
{
    public class GPSRepository
    {
       
        #region POST JOURNEY GPS

        #region POST JOURNEY GPS
        public ReponseEntity GPS_CREATE_JOURNEY(GPS gps)
        {

            ReponseEntity oReponseEntity = new ReponseEntity();           
            try
            {
                
                    using (OracleCommand cmd = new OracleCommand())
                    {
                     
                        cmd.Connection = Helper.OraDCOracleConnection;
                        cmd.CommandText = Helper.SchemaName + "gps_pkg.POST_GPS_JOURNEY";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_ID", OracleDbType.Int32, ParameterDirection.ReturnValue);
                        cmd.Parameters.Add("v_id", OracleDbType.Int32, ParameterDirection.Input).Value = gps.id;
                        cmd.Parameters.Add("v_plate", OracleDbType.Varchar2, ParameterDirection.Input).Value = gps.plate;
                        cmd.Parameters.Add("v_groupid", OracleDbType.Int32, ParameterDirection.Input).Value = gps.groupid;
                        cmd.Parameters.Add("v_journey", OracleDbType.Varchar2, ParameterDirection.Input).Value = gps.journey;
                        cmd.Parameters.Add("v_gpstime", OracleDbType.Date, ParameterDirection.Input).Value = gps.gpstime;
                       
                        cmd.ExecuteNonQuery();
                        var id = Convert.ToInt32(cmd.Parameters["P_ID"].Value.ToString());
                        

                        if (id == 0)
                        {
                            oReponseEntity.Code = "00";
                            oReponseEntity.Message = "Cập nhật dữ liệu thành công";
                            oReponseEntity.Value = gps.id.ToString();
                           
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
                oReponseEntity.Code = "99";
                oReponseEntity.Message = "Lỗi xử lý dữ liệu";
                oReponseEntity.Value = string.Empty;
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
            }
            return oReponseEntity;
        }
        #endregion
        #endregion     
    }
}