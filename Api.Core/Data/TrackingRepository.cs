using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Core.Models;
using Api.Core.Data;
using Oracle.ManagedDataAccess.Client;
using Api.Core.Common;
using System.Data;

namespace Api.Core.Data
{
    public class TrackingRepository
    {

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
                            order.BCCP = Convert.ToInt32(dr["BCCP"].ToString());
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
        
        public List<Delivery> TrackingProcess(string code)
        {
            code = code.Trim().ToUpper();
            DataTable COD_TABLE0 = new DataTable();
            DataTable COD_TABLE1 = new DataTable();
            DataTable VNP_TRACKING_TABLE = new DataTable();
            DataTable EMS_TRACKING_TABLE = new DataTable();
            int m_type = 0;
            EmsRepository _ems = new EmsRepository();
            List<Delivery> _listDelivery = null;
            Delivery _delivery = null;
            try
            {
                cod_service.Service _codapi = new cod_service.Service();
                tracking_service.TrackAndTrace tracking = new tracking_service.TrackAndTrace();
                tracking_service.UserCredentical uc = new tracking_service.UserCredentical();
                uc.user = "vnpost";
                uc.pass = "vn!@#post";
                tracking.EnableDecompression = true;
                tracking.UserCredenticalValue = uc;
                if (checkcode(code) == false)   // <> vận đơn EMS
                {
                    code = Partner_code(code);  // Lấy ra vận đơn EMS từ mã tham chiếu khác
                }
                #region Mã EMS
                #region Hiện thị thông tin báo phát
                #region Dữ liệu PAYPOST
                try
                {
                    COD_TABLE0 = _codapi.GET_MONEY_STATE_BY_ITEM(code).Tables[0];
                    if (COD_TABLE0.Rows[0]["ERROR_CODE"].ToString() == "11")
                        COD_TABLE1 = null;
                    else
                        COD_TABLE1 = _codapi.GET_MONEY_STATE_BY_ITEM(code).Tables[1];
                }
                catch
                {
                    COD_TABLE1 = null;
                }
                #endregion
                if (COD_TABLE1 != null)
                {
                    _listDelivery = new List<Delivery>();
                    for (int j = 0; j < COD_TABLE1.Rows.Count; j++)
                    {
                        _delivery = new Delivery();
                        _delivery.DateInt = Convert_DateInt((COD_TABLE1.Rows[j]["DATE"]));
                        _delivery.TimeInt = Convert_TimeInt(COD_TABLE1.Rows[j]["TIME_DETAIL"]);
                        _delivery.Date = (string)COD_TABLE1.Rows[j]["DATE"];
                        _delivery.Time = (string)(COD_TABLE1.Rows[j]["TIME_DETAIL"]);
                        _delivery.Status = (string)COD_TABLE1.Rows[j]["STATUS_TEXT"];
                        _delivery.Location = (string)COD_TABLE1.Rows[j]["VI_TRI"];
                        _listDelivery.Add(_delivery);
                    }
                    #region Dữ liệu báo phát EMS
                    try
                    {
                        _ems.Put_Pod_To_TMP_By_E1(code, ref m_type);
                        EMS_TRACKING_TABLE = _ems.Get_Pod_From_TMP_By_E1_order(code, 0).Tables[0];
                    }
                    catch
                    {
                        EMS_TRACKING_TABLE = null;
                    }
                    #endregion
                    if (EMS_TRACKING_TABLE != null)
                    {
                        for (int i = 0; i < EMS_TRACKING_TABLE.Rows.Count; i++)
                        {
                            _delivery = new Delivery();
                            _delivery.DateInt = Convert.ToInt32(EMS_TRACKING_TABLE.Rows[i]["ngaynhan"]);
                            _delivery.TimeInt = Convert.ToInt32(EMS_TRACKING_TABLE.Rows[i]["Gionhan"]);
                            _delivery.Date = Convert_Date(EMS_TRACKING_TABLE.Rows[i]["ngaynhan"]);
                            _delivery.Time = Convert_Time(EMS_TRACKING_TABLE.Rows[i]["Gionhan"]);
                            _delivery.Status = Get_EDI_EVENT((string)EMS_TRACKING_TABLE.Rows[i]["trangthai"]);
                            _delivery.Location = GET_TEN_BC_BY_MA_BC((string)EMS_TRACKING_TABLE.Rows[i]["MaBCTra"]);
                            if (String.IsNullOrEmpty(EMS_TRACKING_TABLE.Rows[i]["ngnhan"].ToString()))
                                _delivery.Note = (string)EMS_TRACKING_TABLE.Rows[i]["GHI_CHU"];
                            else
                                _delivery.Note = (string)EMS_TRACKING_TABLE.Rows[i]["ngnhan"] + " " + GET_Addresses_Reason((string)EMS_TRACKING_TABLE.Rows[i]["ngnhan"], (string)EMS_TRACKING_TABLE.Rows[i]["trangthai"]) + " - " + (string)EMS_TRACKING_TABLE.Rows[i]["GHI_CHU"];
                            _listDelivery.Add(_delivery);
                        }
                    }
                    #region Dữ liệu báo phát VNP
                    try
                    {
                        VNP_TRACKING_TABLE = tracking.TrackAndTrace_Items(code).Tables["TBL_DELIVERY"];
                    }
                    catch
                    {
                        VNP_TRACKING_TABLE = null;
                    }
                    #endregion
                    if (VNP_TRACKING_TABLE != null)
                    {
                        for (int k = 0; k < VNP_TRACKING_TABLE.Rows.Count; k++)
                        {
                            _delivery = new Delivery();
                            _delivery.DateInt = Convert_DateInt(VNP_TRACKING_TABLE.Rows[k]["DATE"]);
                            _delivery.TimeInt = Convert_TimeInt(VNP_TRACKING_TABLE.Rows[k]["timedetail"]);
                            _delivery.Date = VNP_TRACKING_TABLE.Rows[k]["DATE"].ToString();
                            _delivery.Time = VNP_TRACKING_TABLE.Rows[k]["timedetail"].ToString();
                            _delivery.Location = VNP_TRACKING_TABLE.Rows[k]["VI_TRI"].ToString();
                            if (substringindex(VNP_TRACKING_TABLE.Rows[k]["STATUSTEXT"].ToString()) == "Đã phát hoàn thành công")
                            {
                                _delivery.Status = VNP_TRACKING_TABLE.Rows[k]["STATUSTEXT"].ToString();
                                _listDelivery.Add(_delivery);
                            }
                        }
                    }
                    if (_listDelivery != null && _listDelivery.Count > 0)
                        _listDelivery = _listDelivery.OrderByDescending(o => o.DateInt).ThenByDescending(o => o.TimeInt).ToList();
                    return _listDelivery;
                }
                else
                {
                    _listDelivery = new List<Delivery>();
                    #region Dữ liệu báo phát EMS
                    try
                    {
                        _ems.Put_Pod_To_TMP_By_E1(code, ref m_type);
                        EMS_TRACKING_TABLE = _ems.Get_Pod_From_TMP_By_E1_order(code, 0).Tables[0];
                    }
                    catch
                    {
                        EMS_TRACKING_TABLE = null;
                    }
                    #endregion
                    if (EMS_TRACKING_TABLE != null)
                    {
                        for (int i = 0; i < EMS_TRACKING_TABLE.Rows.Count; i++)
                        {
                            _delivery = new Delivery();
                            _delivery.DateInt = Convert.ToInt32(EMS_TRACKING_TABLE.Rows[i]["ngaynhan"]);
                            _delivery.TimeInt = Convert.ToInt32(EMS_TRACKING_TABLE.Rows[i]["Gionhan"]);
                            _delivery.Date = Convert_Date(EMS_TRACKING_TABLE.Rows[i]["ngaynhan"]);
                            _delivery.Time = Convert_Time(EMS_TRACKING_TABLE.Rows[i]["Gionhan"]);
                            _delivery.Status = Get_EDI_EVENT((string)EMS_TRACKING_TABLE.Rows[i]["trangthai"]);
                            _delivery.Location = GET_TEN_BC_BY_MA_BC((string)EMS_TRACKING_TABLE.Rows[i]["MaBCTra"]);
                            if (String.IsNullOrEmpty(EMS_TRACKING_TABLE.Rows[i]["ngnhan"].ToString()))
                                _delivery.Note = (string)EMS_TRACKING_TABLE.Rows[i]["GHI_CHU"];
                            else
                                _delivery.Note = (string)EMS_TRACKING_TABLE.Rows[i]["ngnhan"] + " " + GET_Addresses_Reason((string)EMS_TRACKING_TABLE.Rows[i]["ngnhan"], (string)EMS_TRACKING_TABLE.Rows[i]["trangthai"]) + " - " + (string)EMS_TRACKING_TABLE.Rows[i]["GHI_CHU"];
                            _listDelivery.Add(_delivery);
                        }
                    }
                    #region Dữ liệu báo phát VNP
                    try
                    {
                        VNP_TRACKING_TABLE = tracking.TrackAndTrace_Items(code).Tables["TBL_DELIVERY"];
                    }
                    catch
                    {
                        VNP_TRACKING_TABLE = null;
                    }
                    #endregion
                    if (VNP_TRACKING_TABLE != null)
                    {
                        for (int k = 0; k < VNP_TRACKING_TABLE.Rows.Count; k++)
                        {
                            _delivery = new Delivery();
                            _delivery.DateInt = Convert_DateInt(VNP_TRACKING_TABLE.Rows[k]["DATE"]);
                            _delivery.TimeInt = Convert_TimeInt(VNP_TRACKING_TABLE.Rows[k]["timedetail"]);
                            _delivery.Date = VNP_TRACKING_TABLE.Rows[k]["DATE"].ToString();
                            _delivery.Time = VNP_TRACKING_TABLE.Rows[k]["timedetail"].ToString();
                            _delivery.Location = VNP_TRACKING_TABLE.Rows[k]["VI_TRI"].ToString();
                            if (substringindex(VNP_TRACKING_TABLE.Rows[k]["STATUSTEXT"].ToString()) == "Đã phát hoàn thành công")
                            {
                                _delivery.Status = VNP_TRACKING_TABLE.Rows[k]["STATUSTEXT"].ToString();
                                _listDelivery.Add(_delivery);
                            }
                        }
                    }
                    if (_listDelivery != null && _listDelivery.Count > 0)
                        _listDelivery = _listDelivery.OrderByDescending(o => o.DateInt).ThenByDescending(o => o.TimeInt).ToList();
                    else
                        _listDelivery = null;
                    return _listDelivery;
                }

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "TrackingProcess " + code + " " + ex.Message);
                _listDelivery = null;
            }
            return null;
        }
        #region datetime time
        protected string Convert_Date(object str_Date)
        {

            string str = "";
            str = string.Format("{0:dd/MM/yyyy}", str_Date.ToString());
            if ((str == "") || (str == "0"))
                return str;
            else
            {
                string ngay = str.Substring(6, 2);
                string thang = str.Substring(4, 2);
                string nam = str.Substring(0, 4);
                return ngay + "/" + thang + "/" + nam;
            }
        }
        protected int Convert_DateInt(object str_Date)
        {
            try
            {
                string str = "";
                str = string.Format("{0:dd/MM/yyyy}", str_Date.ToString());
                if ((str == "") || (str == "0"))
                    return 0;
                else
                {
                    string ngay = str.Substring(0, 2);
                    string thang = str.Substring(3, 2);
                    string nam = str.Substring(6, 4);
                    return int.Parse(nam + thang + ngay);
                }
            }
            catch
            {
                return 0;
            }

        }
        protected string Convert_Time(object str_Time)
        {
            try
            {
                string str = "";
                str = string.Format("{0:hh:mm:ss}", str_Time.ToString());
                str = str_Time.ToString();
                if ((str == "") || (str == "0"))
                    return str_Time.ToString();
                else
                {
                    string _gio = str.Substring(0, 2);
                    string _phut = str.Substring(2, 2);
                    return _gio + ":" + _phut;
                }
            }
            catch
            {
                return str_Time.ToString();
            }
        }
        protected int Convert_TimeInt(object str_Time)
        {
            try
            {
                string str = "";
                str = string.Format("{0:hh:mm:ss}", str_Time.ToString());
                if ((str == "") || (str == "0"))
                    return 0;
                else
                {
                    string _gio = str.Substring(0, 2);
                    string _phut = str.Substring(3, 2);
                    return int.Parse(_gio + _phut);
                }
            }
            catch
            {
                return 0;
            }

        }
        #endregion
        #region Partner_code
        protected string Partner_code(string code_f)
        {
            string code = "";
            #region partner group a
            string _char1 = (code_f).Substring(0, 1);
            if (_char1 == "S" || _char1 == "B" || _char1 == "C" || _char1 == "N" || _char1 == "L" || _char1 == "A")
            {
                if (_char1 == "S")
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        DataTable da = new DataTable();
                        cmd.Connection = Helper.OraDCOracleConnection;
                        cmd.CommandText = string.Format("select mae1 FROM E1_SHIPCHUNG WHERE TrackingNumber='" + code_f + "'");
                        cmd.CommandType = CommandType.Text;
                        OracleDataAdapter dr = new OracleDataAdapter(cmd);
                        dr.Fill(da);
                        da.TableName = "Getdata";
                        foreach (DataRow da_row in da.Rows)
                        {
                            code = da_row.ItemArray[0].ToString();
                        }
                    }
                }
                else if (_char1 == "B" || _char1 == "C" || _char1 == "N" || _char1 == "L" || _char1 == "A")
                {

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        DataTable da = new DataTable();
                        cmd.Connection = Helper.OraDCOracleConnection;
                        cmd.CommandText = string.Format("select   mae1 from e1e2_ph where so_tham_chieu='" + code_f + "' and manctra='VN' AND MABCTRA not in(0) and mae1 not in (select  mae1 from e1e2_ph where so_tham_chieu='" + code_f + "' and manctra='VN' AND MABCTRA not in(0) and mabc in (701255,702201)and rownum=1 ) and rownum=1 ");
                        cmd.CommandType = CommandType.Text;
                        OracleDataAdapter dr = new OracleDataAdapter(cmd);
                        dr.Fill(da);
                        da.TableName = "Getdata";
                        if (da.Rows.Count == 0)
                        {
                            OracleCommand cmd2 = new OracleCommand();
                            cmd2.Connection = Helper.OraDCOracleConnection;
                            cmd2.CommandText = string.Format("select  mae1 from e1e2_ph where so_tham_chieu='" + code_f + "' and manctra='VN' AND MABCTRA not in(0) and mabc in (701255,702201)and rownum=1 ");
                            cmd2.CommandType = CommandType.Text;
                            OracleDataAdapter dr_P = new OracleDataAdapter(cmd2);
                            dr_P.Fill(da);
                            da.TableName = "Getdata";
                        }
                        foreach (DataRow da_row in da.Rows)
                        {
                            code = da_row.ItemArray[0].ToString();
                        }
                    }
                }

            }
            #endregion
            #region Gss
            if (code == code_f)
            {
                Gss myE1_GSS = new Gss();
                DataTable myDatatable = new DataTable();
                myDatatable = myE1_GSS.Lay_Ma_E1(code_f);
                code_f = myDatatable.Rows[0]["MA_E1"].ToString();
            }
            #endregion
            return code;
        }
        #endregion
        #region GET_TEN_BC_BY_MA_BC
        protected String GET_TEN_BC_BY_MA_BC(string m_event)
        {
            EmsRepository _ems = new EmsRepository();
            return _ems.GET_TEN_BC_BY_MA_BC(m_event);
        }

        #endregion
        #region check_e1
        public bool checkcode(string str_mae1)
        {
            bool result = true;
            #region Kiểm tra mã E1
            if (str_mae1.Length != 13)
            {
                result = false;
            }
            else
            {
                string f = str_mae1.Substring(0, 1);
                string a = str_mae1.Substring(1, 1);
                string b = str_mae1.Substring(11, 1);
                string c = str_mae1.Substring(12, 1);
                if ((IsCharacter(f) == false) || (IsCharacter(a) == false) || (IsCharacter(b) == false) || (IsCharacter(c) == false))
                {
                    result = false;
                }
                else
                {
                    if (f.ToUpper() != "E" && f.ToUpper() != "C" && f.ToUpper() != "V")
                    {
                        result = false;
                    }
                    else
                    {
                        int[] myArray = new int[9];
                        int mySum;
                        int myP;
                        int myPP;
                        for (int i = 2; i < 11; i++)
                        {
                            string d = str_mae1.Substring(i, 1);
                            if (IsNumeric(d) == false)
                            {
                                result = false;
                            }
                            else
                            {
                                myArray[i - 2] = Int32.Parse(d);
                            }
                        }
                        mySum = (myArray[0] * 8) + (myArray[1] * 6) + (myArray[2] * 4) + (myArray[3] * 2) + (myArray[4] * 3) + (myArray[5] * 5) + (myArray[6] * 9) + (myArray[7] * 7);
                        myP = 11 - (mySum % 11);
                        if (myP == 10)
                            myPP = 0;
                        else
                            if (myP == 11)
                            myPP = 5;
                        else
                            myPP = myP;
                        if (myPP != myArray[8])
                        {
                            result = false;
                        }
                    }
                }
            }
            #endregion
            return result;
        }
        #endregion
        #region GET_Addresses_Reason
        protected String GET_Addresses_Reason(string m_ma_ly_do, string m_event)
        {
            EmsRepository _ems = new EmsRepository();
            if (m_event == "H")
            {
                string Ma_Ly_Do = "";
                try
                {
                    Ma_Ly_Do = m_ma_ly_do.Substring(0, 1);
                }
                catch
                {
                    Ma_Ly_Do = " ";
                }
                return _ems.GET_LY_DO(Xu_Ly_Lay_Ma_Ly_Do(m_ma_ly_do), "VN") + ',' + _ems.GET_XU_LY(Ma_Ly_Do, "VN");
            }
            else
                if (m_event == "I" || m_event == "P")
                if (m_ma_ly_do.Trim() == "")
                    return "Ðã phát";
                else
                    return m_ma_ly_do;
            else
                return "             ";
        }
        #endregion

        #region Xử lý mã lý do
        private string Xu_Ly_Lay_Ma_Ly_Do(string Ma_Ly_Do)
        {
            try
            {
                string L_Ma_Ly_Do = Ma_Ly_Do.Substring(Ma_Ly_Do.Length - 1, 1);
                string F_Ma_Ly_Do = Ma_Ly_Do.Substring(0, 1);
                if (IsCharacter(F_Ma_Ly_Do) == true)
                    return Ma_Ly_Do.Substring(1, 2);
                else if (IsCharacter(L_Ma_Ly_Do) == true)
                    return Ma_Ly_Do.Substring(0, 2);
                else
                    return Ma_Ly_Do;
            }
            catch
            {
                return "             ";
            }
        }
        #endregion
        #region Kiểm tra xem đây có phải là ký tự
        private Boolean IsCharacter(string a)
        {
            string alfa = "ABCDEFGHIJKMNLOPQRSTUVXYZW";
            int pos = alfa.IndexOf(a.ToUpper());
            if (pos > -1)
                return true;
            else
                return false;
        }
        #endregion
        #region Kiểm tra xem đây có phải là số không
        private Boolean IsNumeric(string a)
        {
            string alfa = "0123456789";
            int pos = alfa.IndexOf(a.ToUpper());
            if (pos > -1)
                return true;
            else
                return false;
        }
        #endregion
        #region substringindex
        private string substringindex(string _str)
        {
            try
            {
                int index;
                index = _str.IndexOf('.');
                return _str.Substring(0, index);
            }
            catch
            {
                return _str;
            }
        }
        #endregion

        #region Get EDI
        protected String Get_EDI_EVENT(string m_event)
        {
            string str = "";
            EmsRepository _ems = new EmsRepository();
            str = _ems.GET_EDI_EVENT(m_event, "VN");
            return str.Trim();
        }
        #endregion

    }
}