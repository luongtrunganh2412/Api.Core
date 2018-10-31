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
    public class TrackingRepository
    {
        #region ORDER_GET
        public ReturnTracking GET_TRACKING(string order_code)
        {
            ReturnTracking return_tracking = new ReturnTracking();
            List<Tracking> list_tracking = new List<Tracking>();
            Tracking tracking = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "ORDER_PKG.TRACKING_GET";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_ORDER_CODE", OracleDbType.Varchar2)).Value = order_code;                    
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);

                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) == -10)
                    {
                        return_tracking.Code = "10";
                        return_tracking.Message = "Order code is null";                        
                        return_tracking.ListTracking = null;
                        return return_tracking;
                    }
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            list_tracking = new List<Tracking>();
                            while (dr.Read())
                            {
                                tracking = new Tracking();
                                tracking.ITEMCODE = dr["ITEMCODE"].ToString();
                                tracking.WEIGHT = Convert.ToInt32(dr["WEIGHT"].ToString());
                                tracking.TOPOSTCODE = Convert.ToInt32(dr["TOPOSTCODE"].ToString());
                                tracking.ITEMCODEPARTNER = dr["ITEMCODEPARTNER"].ToString();
                                tracking.ADDRESSNAME = dr["ADDRESSNAME"].ToString();
                                tracking.STATE = dr["STATE"].ToString();
                                tracking.STATUSNOTE = dr["STATUSNOTE"].ToString();
                                list_tracking.Add(tracking);
                            }
                            return_tracking.Code = "00";
                            return_tracking.Message = "Lấy dữ liệu thành công.";
                            return_tracking.ListTracking = list_tracking;
                        }
                        else
                        {
                            return_tracking.Code = "01";
                            return_tracking.Message = "Không tồn tại bản ghi nào.";
                            return_tracking.ListTracking = null;
                        }

                    }
                    else
                    {
                        return_tracking.Code = "98";
                        return_tracking.Message = "Lỗi xử lý hệ thống..";
                        return_tracking.ListTracking = null;
                    }

                }

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "OrderRepository.GET_ORDER: " + ex.Message);
                return_tracking.Code = "99";
                return_tracking.Message = ex.Message;              
                return_tracking.ListTracking = null;
            }
            return return_tracking;
        }

        #endregion

        #region ORDER_GET_BY_CODE
        public ReturnOrderByCode GetOrderByCode(OrderCode oc)
        {
            ReturnOrderByCode return_order = new ReturnOrderByCode();
            Order order = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "ORDER_PKG.ORDER_GET_BY_CODE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_ORDER_CODE", OracleDbType.Varchar2)).Value = oc.order_code;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);

                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                order = new Order();
                                order.MAE1 = dr["MAE1"].ToString();
                                order.NGAYDONG = Convert.ToInt32(dr["NGAYDONG"].ToString());
                                order.NGAYNHAN = Convert.ToInt32(dr["NGAYNHAN"].ToString());
                                order.GIONHAN = Convert.ToInt32(dr["GIONHAN"].ToString());
                                order.NGUOINHAN = dr["NGUOINHAN"].ToString();
                                order.DIACHINHAN = dr["DIACHINHAN"].ToString();

                                order.MABC = Convert.ToInt32(dr["MABC"].ToString());
                                order.KHOILUONG = Convert.ToInt32(dr["KHOILUONG"].ToString());

                                order.STATE = dr["STATE"].ToString();
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
                                order.SO_TIEN_THU_HO = Convert.ToInt32(dr["SO_TIEN_THU_HO"].ToString());

                                order.TRACEDATE = dr["TRACEDATE"].ToString();
                                order.GHI_CHU = dr["GHI_CHU"].ToString();
                                order.DICH_VU = dr["DICH_VU"].ToString();
                                order.STATUSNOTE = dr["STATUSNOTE"].ToString();
                                order.STATUSPOST = Convert.ToInt32(dr["STATUSPOST"].ToString());

                            }
                            return_order.Code = "00";
                            return_order.Message = "Lấy dữ liệu thành công.";
                            return_order.Order = order;
                        }
                        else
                        {
                            return_order.Code = "01";
                            return_order.Message = "Không tồn tại bản ghi nào.";
                            return_order.Order = null;
                        }

                    }
                    else
                    {
                        return_order.Code = "98";
                        return_order.Message = "Lỗi xử lý hệ thống.";
                        return_order.Order = null;
                    }

                }

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "OrderRepository.GetOrderByCode: " + ex.Message);
                return_order.Code = "99";
                return_order.Message = ex.Message;

            }
            return return_order;
        }

        #endregion

        #region ORDER_GET_BY_CODE
        public ReturnOrderByCode GetOrderV1(OrderCode oc)
        {
            ReturnOrderByCode return_order = new ReturnOrderByCode();
            Order order = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "ORDER_PKG.ORDER_GET_V1";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_ORDER_CODE", OracleDbType.Varchar2)).Value = oc.order_code;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);

                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                order = new Order();
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
                                order.SO_TIEN_THU_HO = Convert.ToInt32(dr["SO_TIEN_THU_HO"].ToString());

                                order.TRACEDATE = dr["TRACEDATE"].ToString();
                                order.GHI_CHU = dr["GHI_CHU"].ToString();
                                order.DICH_VU = dr["DICH_VU"].ToString();
                                order.STATUSNOTE = dr["STATUSNOTE"].ToString();
                                order.STATUSPOST = Convert.ToInt32(dr["STATUSPOST"].ToString());

                            }
                            return_order.Code = "00";
                            return_order.Message = "Lấy dữ liệu thành công.";
                            return_order.Order = order;
                        }
                        else
                        {
                            return_order.Code = "01";
                            return_order.Message = "Không tồn tại bản ghi nào.";
                            return_order.Order = null;
                        }
                        
                    }
                    else
                    {
                        return_order.Code = "98";
                        return_order.Message = "Lỗi xử lý hệ thống.";
                        return_order.Order = null;
                    }
                }

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "OrderRepository.GetOrderByCode: " + ex.Message);
                return_order.Code = "99";
                return_order.Message = ex.Message;

            }
            return return_order;
        }

        #endregion

        #region TRACKING_V3
        public ReturnTracking_v3 TRACKING_V3(string order_code)
        {
            Exchange exc = new Exchange();
            ReturnTracking_v3 return_tracking = new ReturnTracking_v3();
            List<Tracking_v3> list_tracking = new List<Tracking_v3>();
            Tracking_v3 tracking = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "ORDER_PKG.TRACKING_V3";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_ORDER_CODE", OracleDbType.Varchar2)).Value = order_code;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);

                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) == -10)
                    {
                        return_tracking.Code = "10";
                        return_tracking.Message = "Order code is null";
                        return_tracking.ListTracking = null;
                        return return_tracking;
                    }
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            list_tracking = new List<Tracking_v3>();
                            while (dr.Read())
                            {
                                tracking = new Tracking_v3();
                                tracking.CODE = dr["CODE"].ToString();
                                tracking.DATE_RECEIVE = exc.IntToDate(Convert.ToInt32(dr["DATE_RECEIVE"].ToString()));
                                tracking.TIME_RECEIVE = exc.IntToTime(Convert.ToInt32(dr["TIME_RECEIVE"].ToString()));
                                tracking.RECEIVER = dr["RECEIVER"].ToString();
                                tracking.REASON = dr["REASON"].ToString();
                                tracking.SOLUTION = dr["SOLUTION"].ToString();
                                tracking.POSTCODE = dr["POSTCODE"].ToString();
                                list_tracking.Add(tracking);
                            }
                            return_tracking.Code = "00";
                            return_tracking.Message = "Lấy dữ liệu thành công.";
                            return_tracking.ListTracking = list_tracking;
                        }
                        else
                        {
                            return_tracking.Code = "01";
                            return_tracking.Message = "Không tồn tại bản ghi nào.";
                            return_tracking.ListTracking = null;
                        }

                    }
                    else
                    {
                        return_tracking.Code = "98";
                        return_tracking.Message = "Lỗi xử lý hệ thống.";
                        return_tracking.ListTracking = null;
                    }


                }

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "OrderRepository.TRACKING_V3: " + ex.Message);
                return_tracking.Code = "99";
                return_tracking.Message = ex.Message;
                return_tracking.ListTracking = null;
            }
            return return_tracking;
        }

        #endregion


        #region TRACKING_V4
        public ReturnTracking_v3 TRACKING_V4(string order_code)
        {
            Exchange exc = new Exchange();
            ReturnTracking_v3 return_tracking = new ReturnTracking_v3();
            List<Tracking_v3> list_tracking = new List<Tracking_v3>();
            Tracking_v3 tracking = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "ORDER_PKG.TRACKING_V4";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_ORDER_CODE", OracleDbType.Varchar2)).Value = order_code;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);

                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) == -10)
                    {
                        return_tracking.Code = "10";
                        return_tracking.Message = "Not found";
                        return_tracking.ListTracking = null;
                        return return_tracking;
                    }
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            list_tracking = new List<Tracking_v3>();
                            while (dr.Read())
                            {
                                tracking = new Tracking_v3();
                                tracking.CODE = dr["CODE"].ToString();
                                tracking.DATE_RECEIVE = exc.IntToDate(Convert.ToInt32(dr["DATE_RECEIVE"].ToString()));
                                tracking.TIME_RECEIVE = exc.IntToTime(Convert.ToInt32(dr["TIME_RECEIVE"].ToString()));
                                tracking.RECEIVER = dr["RECEIVER"].ToString();
                                tracking.REASON = dr["REASON"].ToString();
                                tracking.SOLUTION = dr["SOLUTION"].ToString();
                                tracking.POSTCODE = dr["POSTCODE"].ToString();
                                tracking.STATUSCODE = dr["STATUS_CODE"].ToString();
                                list_tracking.Add(tracking);
                            }
                            return_tracking.Code = "00";
                            return_tracking.Message = "Lấy dữ liệu thành công.";
                            return_tracking.ListTracking = list_tracking;
                        }
                        else
                        {
                            return_tracking.Code = "01";
                            return_tracking.Message = "Không tồn tại bản ghi nào.";
                            return_tracking.ListTracking = null;
                        }
                        
                    }
                    else
                    {
                        return_tracking.Code = "98";
                        return_tracking.Message = "Lỗi xử lý hệ thống.";
                        return_tracking.ListTracking = null;
                    }
                }

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "OrderRepository.TRACKING_V3: " + ex.Message);
                return_tracking.Code = "99";
                return_tracking.Message = ex.Message;
                return_tracking.ListTracking = null;
            }
            return return_tracking;
        }

        #endregion
    }
}