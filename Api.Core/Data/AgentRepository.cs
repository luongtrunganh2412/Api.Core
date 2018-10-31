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
    /// Lớp truy xuất dữ liệu Đại lý
    /// </summary>
    public class AgentRepository
    {
        /// <summary>
        /// Lấy danh sách Đại lý
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Agent> GetAllAgents()
        {
            List<Agent> listAgent = null;
            Agent oAgent = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.ME24OracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM AGENT A WHERE A.AMND_STATE = 'A' ORDER BY A.NAME");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listAgent = new List<Agent>();
                        while (dr.Read())
                        {
                            oAgent = new Agent();
                            if (!string.IsNullOrEmpty(dr["AMND_USER"].ToString()))
                                oAgent.AmndUser = Convert.ToInt32(dr["AMND_USER"]);
                            if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                                oAgent.Id = Convert.ToInt32(dr["ID"]);
                            if (!string.IsNullOrEmpty(dr["PARENT_ID"].ToString()))
                                oAgent.ParentId = Convert.ToInt32(dr["PARENT_ID"]);
                            oAgent.Code = dr["CODE"].ToString();                            
                            oAgent.Name = dr["NAME"].ToString();
                            if (!string.IsNullOrEmpty(dr["AGENT_LEVEL"].ToString()))
                                oAgent.AgentLevel = Convert.ToInt32(dr["AGENT_LEVEL"]);
                            if (!string.IsNullOrEmpty(dr["PROVINCE_ID"].ToString()))
                                oAgent.ProvinceId = Convert.ToInt32(dr["PROVINCE_ID"]);
                            if (!string.IsNullOrEmpty(dr["DISTRICT_ID"].ToString()))
                                oAgent.DistrictId = Convert.ToInt32(dr["DISTRICT_ID"]);
                            if (!string.IsNullOrEmpty(dr["WARD_ID"].ToString()))
                                oAgent.WardId = Convert.ToInt32(dr["WARD_ID"]);
                            if (!string.IsNullOrEmpty(dr["HAMLET_ID"].ToString()))
                                oAgent.HamletId = Convert.ToInt32(dr["HAMLET_ID"]);
                            oAgent.Street = dr["STREET"].ToString();
                            oAgent.ContactName = dr["CONTACT_NAME"].ToString();
                            oAgent.ContactPhone = dr["CONTACT_PHONE"].ToString();
                            oAgent.ContactEmail = dr["CONTACT_EMAIL"].ToString();
                            oAgent.IsLock = dr["IS_LOCK"].ToString();
                            listAgent.Add(oAgent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "AgentRepository.GetAllAgents: " + ex.Message);
                listAgent = null;
            }

            return listAgent;
        }

        /// <summary>
        /// Lấy thông tin Đại lý
        /// </summary>
        /// <param name="id">Id đại lý</param>
        /// <returns></returns>
        public Agent GetAgentById(int id)
        {            
            Agent oAgent = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.ME24OracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM AGENT A WHERE A.AMND_STATE = 'A' AND A.ID = {0} ORDER BY A.NAME", new object[] { id });
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {                        
                        while (dr.Read())
                        {
                            oAgent = new Agent();
                            if (dr["AMND_USER"] != null)
                                oAgent.AmndUser = Convert.ToInt32(dr["AMND_USER"]);
                            if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                                oAgent.Id = Convert.ToInt32(dr["ID"]);
                            if (!string.IsNullOrEmpty(dr["PARENT_ID"].ToString()))
                                oAgent.ParentId = Convert.ToInt32(dr["PARENT_ID"]);
                            oAgent.Code = dr["CODE"].ToString();
                            oAgent.Name = dr["NAME"].ToString();
                            if (!string.IsNullOrEmpty(dr["AGENT_LEVEL"].ToString()))
                                oAgent.AgentLevel = Convert.ToInt32(dr["AGENT_LEVEL"]);
                            if (!string.IsNullOrEmpty(dr["PROVINCE_ID"].ToString()))
                                oAgent.ProvinceId = Convert.ToInt32(dr["PROVINCE_ID"]);
                            if (!string.IsNullOrEmpty(dr["DISTRICT_ID"].ToString()))
                                oAgent.DistrictId = Convert.ToInt32(dr["DISTRICT_ID"]);
                            if (!string.IsNullOrEmpty(dr["WARD_ID"].ToString()))
                                oAgent.WardId = Convert.ToInt32(dr["WARD_ID"]);
                            if (!string.IsNullOrEmpty(dr["HAMLET_ID"].ToString()))
                                oAgent.HamletId = Convert.ToInt32(dr["HAMLET_ID"]);
                            oAgent.Street = dr["STREET"].ToString();
                            oAgent.ContactName = dr["CONTACT_NAME"].ToString();
                            oAgent.ContactPhone = dr["CONTACT_PHONE"].ToString();
                            oAgent.ContactEmail = dr["CONTACT_EMAIL"].ToString();
                            oAgent.IsLock = dr["IS_LOCK"].ToString();         
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "AgentRepository.GetAgentById: " + ex.Message);
                oAgent = null;
            }

            return oAgent;
        }

        /// <summary>
        /// Create Agent
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public ReponseEntity CreateAgent(Agent agent)
        {
            ReponseEntity oReponseEntity = null;
            int id = 0;
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    //P_AMND_USER     NUMBER,
                    //P_PARENT_ID     NUMBER,
                    //P_CODE          VAPRHAR2,
                    //P_NAME          VARCHAR2,
                    //P_AGENT_LEVEL   NUMBER,
                    //P_PROVINCE_ID   NUMBER,
                    //P_DISTRICT_ID   NUMBER,
                    //P_WARD_ID       NUMBER,
                    //P_HAMLET_ID     NUMBER,
                    //P_STREET        NVARCHAR2,
                    //P_CONTACT_NAME  NVARCHAR2,
                    //P_CONTACT_PHONE VARCHAR2,
                    //P_CONTACT_EMAIL NVARCHAR2,
                    //P_IS_LOCK       VARCHAR2,
                    //P_ID            OUT NUMBER

                    cm.Connection = Helper.ME24OracleConnection;
                    cm.CommandText = Helper.SchemaName + "AGENT_PKG.AGENT_ADD";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("P_AMND_USER", OracleDbType.Int32, ParameterDirection.Input).Value = agent.AmndUser;
                    cm.Parameters.Add("P_PARENT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = agent.ParentId;
                    cm.Parameters.Add("P_CODE", OracleDbType.Int32, ParameterDirection.Input).Value = agent.Code;
                    cm.Parameters.Add("P_NAME", OracleDbType.Int32, ParameterDirection.Input).Value = agent.Name;
                    cm.Parameters.Add("P_AGENT_LEVEL", OracleDbType.Int32, ParameterDirection.Input).Value = agent.AgentLevel;
                    cm.Parameters.Add("P_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = agent.ProvinceId;
                    cm.Parameters.Add("P_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = agent.DistrictId;
                    cm.Parameters.Add("P_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = agent.WardId;
                    cm.Parameters.Add("P_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = agent.HamletId;
                    cm.Parameters.Add("P_STREET", OracleDbType.Int32, ParameterDirection.Input).Value = agent.Street;
                    cm.Parameters.Add("P_CONTACT_NAME", OracleDbType.Int32, ParameterDirection.Input).Value = agent.ContactName;
                    cm.Parameters.Add("P_CONTACT_PHONE", OracleDbType.Int32, ParameterDirection.Input).Value = agent.ContactPhone;
                    cm.Parameters.Add("P_CONTACT_EMAIL", OracleDbType.Int32, ParameterDirection.Input).Value = agent.ContactEmail;
                    cm.Parameters.Add("P_IS_LOCK", OracleDbType.Int32, ParameterDirection.Input).Value = agent.IsLock;
                    cm.Parameters.Add("ID", OracleDbType.Int32, ParameterDirection.Output).Value = agent.Id;
                    cm.ExecuteNonQuery();
                    id = Convert.ToInt32(cm.Parameters["ID"].Value);
                    oReponseEntity = new ReponseEntity();
                    if(id > 0)
                    {
                        oReponseEntity.Code = "00";
                        oReponseEntity.Message = "Cập nhật dữ liệu thành công";
                        oReponseEntity.Value = id.ToString();
                    }
                    else
                    {
                        switch(id)
                        {
                            case -1:
                                oReponseEntity.Code = "-1";
                                oReponseEntity.Message = "Đã tồn tại mã [" + agent.Code + "] trong hệ thống.";
                                oReponseEntity.Value = id.ToString();
                                break;
                            default:
                                oReponseEntity.Code = "-99";
                                oReponseEntity.Message = "Lỗi cập nhật dữ liệu.";
                                oReponseEntity.Value = string.Empty;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "AgentRepository.CreateAgent: " + ex.Message);
                oReponseEntity = null;
            }

            return oReponseEntity;
        }
    }
}