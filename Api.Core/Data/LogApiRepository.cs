using Api.Core.Common;
using Api.Core.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
namespace Api.Core.Data
{
    public class LogApiRepository
    {
        public ReponseEntity Create_LogApi(ApiLog _apilog )
        {
            ReponseEntity oReponseEntity = new ReponseEntity();
            int id = 0;
            OracleCommand cmd;
            try
            {
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = "EMS_TRACKING.LOG_API_CREATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_CUSTOMER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = _apilog.CUSTOMER_CODE;
                    cmd.Parameters.Add("P_ERROR_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = _apilog.ERROR_CODE;
                    cmd.Parameters.Add("P_ERROR_MESSAGE", OracleDbType.NVarchar2, ParameterDirection.Input).Value = _apilog.ERROR_MESSAGE;
                    cmd.Parameters.Add("P_SERVICE_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = _apilog.SERVICE_NAME;
                    cmd.Parameters.Add("P_TRACKING_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = _apilog.TRACKING_CODE;
                    cmd.Parameters.Add("P_API_KEY", OracleDbType.Varchar2, ParameterDirection.Input).Value = _apilog.API_KEY;
                    cmd.Parameters.Add("P_COUNT", OracleDbType.Int32, ParameterDirection.Input).Value = _apilog.COUNT;
                    cmd.Parameters.Add("P_CLASS", OracleDbType.Varchar2, ParameterDirection.Input).Value = _apilog.CLASS;
                    cmd.Parameters.Add("P_FUNCTION_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = _apilog.FUNCTION_NAME;
                    cmd.Parameters.Add("P_STATUS", OracleDbType.Varchar2, ParameterDirection.Input).Value = _apilog.STATUS;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);

                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    id = int.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString());
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
                            oReponseEntity.Value = id.ToString();
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
    }
}