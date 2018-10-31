using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using Api.App.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
namespace Api.App.Data
{
    public class OrderRepository
    {
        #region ORDER_GET
        public ReturnOrder GET_ORDER(OrderRequest order_request)
        {
            ReturnOrder return_order = new ReturnOrder();
            List<OrderMobile> list_order = new List<OrderMobile>();
            OrderMobile order = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "ORDER_PKG.ORDER_GET";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_ORDER_CODE", OracleDbType.Varchar2)).Value = order_request.code;
                    cmd.Parameters.Add(new OracleParameter("P_CUSTOMER_CODE", OracleDbType.Varchar2)).Value = order_request.customer_code;
                    cmd.Parameters.Add(new OracleParameter("P_STATUS", OracleDbType.Varchar2)).Value = order_request.status;
                    cmd.Parameters.Add(new OracleParameter("P_FROM_DATE", OracleDbType.Int32)).Value = order_request.from_date;
                    cmd.Parameters.Add(new OracleParameter("P_TO_DATE", OracleDbType.Int32)).Value = order_request.to_date;
                    cmd.Parameters.Add(new OracleParameter("P_PAGE_INDEX", OracleDbType.Int32)).Value = order_request.page_index;
                    cmd.Parameters.Add(new OracleParameter("P_PAGE_SIZE", OracleDbType.Int32)).Value = order_request.page_size;
                    cmd.Parameters.Add("P_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_SUCCESS", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_AMOUNT", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_AMOUNT_SUCCESS", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_TOTAL_FREIGHT", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);

                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) == -10)
                    {
                        return_order.Code = "10";
                        return_order.Message = "Customer code is null";
                        return_order.Total = 0;
                        return_order.ListOrder = null;
                        return return_order;
                    }
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            list_order = new List<OrderMobile>();
                            while (dr.Read())
                            {
                                order = new OrderMobile();
                                order.MAE1 = dr["MAE1"].ToString();
                                order.NGAYDONG = Convert.ToInt32(dr["NGAYDONG"].ToString());
                                order.NGAYNHAN = Convert.ToInt32(dr["NGAYNHAN"].ToString());
                                order.GIONHAN = Convert.ToInt32(dr["GIONHAN"].ToString());
                                order.NGUOINHAN = dr["NGUOINHAN"].ToString();
                                order.DIACHINHAN = dr["DIACHINHAN"].ToString();

                                order.MABC = Convert.ToInt32(dr["MABC"].ToString());
                                order.KHOILUONG = Convert.ToInt32(dr["KHOILUONG"].ToString());

                                order.STATE = dr["STATE"].ToString();
                                order.STATE_NAME = dr["STATE_NAME"].ToString();
                                order.STATE_COD = dr["STATE_COD"].ToString();

                                order.STATEINT = Convert.ToInt32(dr["STATEINT"].ToString());
                                order.STATECODINT = Convert.ToInt32(dr["STATECODINT"].ToString());

                                order.MAKH = dr["MAKH"].ToString();
                                order.MA_BC_KHAI_THAC = Convert.ToInt32(dr["MA_BC_KHAI_THAC"].ToString());

                                order.DIEN_THOAI_NHAN = dr["DIEN_THOAI_NHAN"].ToString();

                                order.CUOC_E1 = Convert.ToInt32(dr["CUOC_E1"].ToString());

                                order.NGUOINHAN1 = dr["NGUOINHAN1"].ToString();
                                order.BCCP = string.IsNullOrEmpty(dr["BCCP"].ToString()) ? 0 : Convert.ToInt32(dr["BCCP"].ToString());
                                order.SO_THAM_CHIEU = dr["SO_THAM_CHIEU"].ToString();
                                order.SO_TIEN_THU_HO = string.IsNullOrEmpty(dr["SO_TIEN_THU_HO"].ToString()) ? 0 : Convert.ToInt32(dr["SO_TIEN_THU_HO"].ToString());

                                order.TRACEDATE = dr["TRACEDATE"].ToString();
                                order.GHI_CHU = dr["GHI_CHU"].ToString();
                                order.DICH_VU = dr["DICH_VU"].ToString();
                                order.STATUSNOTE = dr["STATUSNOTE"].ToString();
                                order.STATUSPOST = string.IsNullOrEmpty(dr["STATUSPOST"].ToString()) ? 0 : Convert.ToInt32(dr["STATUSPOST"].ToString());

                                list_order.Add(order);
                            }
                            return_order.Code = "00";
                            return_order.Message = "Lấy dữ liệu thành công.";
                            return_order.Total = Convert.ToInt32(cmd.Parameters["P_TOTAL"].Value.ToString());
                            return_order.Total_Success = Convert.ToInt32(cmd.Parameters["P_SUCCESS"].Value.ToString());
                            if (string.IsNullOrEmpty(cmd.Parameters["P_AMOUNT"].Value.ToString()))
                                return_order.Total_Amount = 0;
                            else
                                return_order.Total_Amount = Convert.ToDouble(cmd.Parameters["P_AMOUNT"].Value.ToString());
                            if (string.IsNullOrEmpty(cmd.Parameters["P_AMOUNT_SUCCESS"].Value.ToString()))
                                return_order.Total_Amount_Success = 0;
                            else
                                return_order.Total_Amount_Success = Convert.ToDouble(cmd.Parameters["P_AMOUNT_SUCCESS"].Value.ToString());
                            if (string.IsNullOrEmpty(cmd.Parameters["P_TOTAL_FREIGHT"].Value.ToString()))
                                return_order.Total_Freight = 0;
                            else
                                return_order.Total_Freight = Convert.ToDouble(cmd.Parameters["P_TOTAL_FREIGHT"].Value.ToString());
                            return_order.ListOrder = list_order;
                        }
                        else
                        {
                            return_order.Code = "01";
                            return_order.Message = "Không tồn tại dữ liệu.";
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "OrderRepository.GET_ORDER: " + ex.Message);
                return_order.Code = "99";
                return_order.Message = ex.Message;
                return_order.Total = 0;
                return_order.ListOrder = null;
            }
            return return_order;
        }

        #endregion        
    }
}