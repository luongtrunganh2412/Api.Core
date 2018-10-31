using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using WebService.Core.Common;
using WebService.Core.Model;
using Oracle.ManagedDataAccess.Client;
namespace WebService.Core.Data
{
    public class OrderRepository
    {
        #region GET SHIPMENT
        public ResponseOrder ORDER_GET(string order_code, string customer_code)
        {
            ResponseOrder returnOrder = new ResponseOrder();

            Response response = null;
            List<CShipment> listShipment = null;
            CShipment shipment = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.ORDER_GET";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_ORDER_CODE", OracleDbType.Varchar2)).Value = order_code;
                    cmd.Parameters.Add(new OracleParameter("P_CUSTOMER_CODE", OracleDbType.Varchar2)).Value = customer_code;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            listShipment = new List<CShipment>();
                            while (dr.Read())
                            {
                                shipment = new CShipment();
                                shipment.ORDER_CODE = dr["ORDER_CODE"].ToString();
                                shipment.TRACKING_CODE = dr["TRACKING_CODE"].ToString();
                                shipment.SENDER_CODE = dr["SENDER_CODE"].ToString();
                                shipment.SENDER_NAME = dr["SENDER_NAME"].ToString();
                                shipment.SENDER_ADDRESS = dr["SENDER_ADDRESS"].ToString();
                                shipment.SENDER_PHONE = dr["SENDER_PHONE"].ToString();
                                shipment.SENDER_EMAIL = dr["SENDER_EMAIL"].ToString();
                                shipment.SENDER_PROVINCE_ID = Convert.ToInt32(dr["SENDER_PROVINCE_ID"].ToString());
                                shipment.SENDER_DISTRICT_ID = Convert.ToInt32(dr["SENDER_DISTRICT_ID"].ToString());

                                shipment.RECEIVER_NAME = dr["RECEIVER_NAME"].ToString();
                                shipment.RECEIVER_EMAIL = dr["RECEIVER_EMAIL"].ToString();
                                shipment.RECEIVER_PHONE = dr["RECEIVER_PHONE"].ToString();
                                shipment.RECEIVER_ADDRESS = dr["RECEIVER_ADDRESS"].ToString();
                                shipment.RECEIVER_PROVINCE_ID = Convert.ToInt32(dr["RECEIVER_PROVINCE_ID"].ToString());
                                shipment.RECEIVER_DISTRICT_ID = Convert.ToInt32(dr["RECEIVER_DISTRICT_ID"].ToString());
                                shipment.PRODUCT_NAME = dr["PRODUCT_NAME"].ToString();
                                shipment.PRODUCT_DESCRIPTION = dr["PRODUCT_DESCRIPTION"].ToString();

                                shipment.PRODUCT_QUANTITY = Convert.ToInt32(dr["PRODUCT_QUANTITY"].ToString());
                                shipment.PRODUCT_VALUE = Convert.ToInt32(dr["PRODUCT_VALUE"].ToString());
                                shipment.STATUS = dr["STATUS"].ToString();
                                shipment.TOTAL_AMOUNT = Convert.ToInt32(dr["TOTAL_AMOUNT"].ToString());
                                shipment.WEIGHT = Convert.ToInt32(dr["WEIGHT"].ToString());
                                shipment.SERVICE_TYPE = Convert.ToInt32(dr["SERVICE_TYPE"].ToString());
                                shipment.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                                shipment.REF_CODE = dr["REF_CODE"].ToString();
                                shipment.PO_CREATE = dr["PO_CREATE"].ToString();
                                shipment.TO_COUNTRY = dr["TO_COUNTRY"].ToString();
                                shipment.CHANNEL = dr["CHANNEL"].ToString();
                                shipment.COD = Convert.ToInt32(dr["COD"].ToString());
                                shipment.ADDITION_SERVICE = dr["ADDITION_SERVICE"].ToString();
                                shipment.SPECIAL_SERVICE = dr["SPECIAL_SERVICE"].ToString();
                                listShipment.Add(shipment);
                            }
                            response = new Response();
                            response.Code = "200";
                            response.Message = "Lấy dữ liệu thành công";
                            LogAPI.LogToFile(LogFileType.MESSAGE, "OrderRepository.ORDER_GET (GET DATA FROM SHIPMENT DATACENTER): " + order_code + "---" + "CODE:" + response.Code + "---" + "Message:" + response.Message);
                        }
                        else
                        {
                            response = new Response();
                            response.Code = "204";
                            response.Message = "Không tồn tại dữ liệu phù hợp";
                            LogAPI.LogToFile(LogFileType.MESSAGE, "OrderRepository.ORDER_GET (GET DATA FROM SHIPMENT DATACENTER): " + order_code + "---" + "CODE:" + response.Code + "---" + "Message:" + response.Message);
                        }
                        returnOrder.Response = response;
                        returnOrder.ListShipment = listShipment;
                        
                    }
                    else
                    {
                        response = new Response();
                        response.Code = "-98";
                        response.Message = "Lỗi xử lý hệ thống ";
                        returnOrder.Response = response;
                        returnOrder.ListShipment = listShipment;
                        LogAPI.LogToFile(LogFileType.MESSAGE, "OrderRepository.ORDER_GET (GET DATA FROM SHIPMENT DATACENTER): " + order_code + "---" + "CODE:" + response.Code + "---" + "Message:" + response.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "OrderRepository.ORDER_GET (GET DATA FROM SHIPMENT DATACENTER): " + "CODE:" + response.Code + "---" + "Message:" + response.Message);
                response = new Response();
                response.Code = "-99";
                response.Message = "Lỗi xử lý dữ liệu. " + ex.Message;
                returnOrder.Response = response;
                returnOrder.ListShipment = listShipment;
            }
            return returnOrder;
        }
        #endregion

        #region CHECK CONNECTION
        public bool CheckConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ORA_CONNECTION_STRING_DC"].ConnectionString;
            using (var conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    LogAPI.LogToFile(LogFileType.EXCEPTION, DateTime.Now.ToString() + ":" + connectionString + ".Connection fail." + ex.Message);
                    return false;
                }
            }
        }
        #endregion
    }
}