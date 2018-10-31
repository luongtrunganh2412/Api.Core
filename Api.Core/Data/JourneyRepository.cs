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
    public class JourneyRepository
    {
        #region JOURNEY_LOG_CREATE 
        public ReponseEntity JOURNEY_LOG_CREATE(string _str, string name)
        {
            ReponseEntity oReponseEntity = new ReponseEntity();
            try
            {
                OracleCommand cmd = new OracleCommand(Helper.SchemaName + "JOURNEY_PKG.JOURNEY_LOG_CREATE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ID", OracleDbType.Int32, ParameterDirection.ReturnValue);

                cmd.Parameters.Add("P_USER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = name.ToUpper().Trim();
                cmd.Parameters.Add("P_CONTENT", OracleDbType.Varchar2, ParameterDirection.Input).Value = _str;
                OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                var id = Convert.ToInt32(cmd.Parameters["P_ID"].Value.ToString());
                if (id > 0)
                {
                    oReponseEntity.Code = "200";
                    oReponseEntity.Message = "Success";
                    oReponseEntity.Id = id;
                }
                else
                {
                    oReponseEntity.Code = "-99";
                    oReponseEntity.Message = "Data processing error";
                    oReponseEntity.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                oReponseEntity.Code = "99";
                oReponseEntity.Message = "System processing error. " + ex.Message;
                oReponseEntity.Value = string.Empty;

            }
            return oReponseEntity;
        }
        #endregion
    }
}