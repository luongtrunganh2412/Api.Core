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

namespace Api.Core.Data
{
    public class EmsRepository
    {
        #region getThongTinE1
        //new:

        public ReturnValue getThongTinE1NEW(String v_mae1)
        {
            Lading _lading = null;
            ReturnValue _response = new ReturnValue();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_TRACKING.GET_E1INFO";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_CODE", OracleDbType.Varchar2)).Value = v_mae1.Trim().ToUpper();
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            _lading = new Lading();
                            while (dr.Read())
                            {
                                _lading.Code = v_mae1;
                                _lading.From_PO = dr["BCGOC"].ToString();
                                _lading.To_PO = dr["BCTRA"].ToString();
                                _lading.ToCountry = dr["NUOCTRA"].ToString();
                                _lading.FromCountry = dr["NUOCNHAN"].ToString();
                                _lading.ReleasedDate = dr["NGAY"].ToString();
                                _lading.ReleasedTime = dr["GIO"].ToString();
                                _lading.Weight = Convert.ToInt32(dr["khoiluong"]);
                                _lading.Class = dr["phanloai"].ToString();
                                _lading.Service = dr["DVU"].ToString();
                                _lading.From_Name = dr["NGUOIGUI"].ToString();
                                _lading.From_Phone = dr["DIENTHOAIGUI"].ToString();
                                _lading.From_Address = dr["DIACHIGUI"].ToString();
                                _lading.To_Name = dr["NGUOINHAN"].ToString();
                                _lading.To_Phone = dr["DIENTHOAINHAN"].ToString();
                                _lading.To_Address = dr["DIACHI"].ToString();
                                _lading.To_Province = dr["MATINHTRA"].ToString();
                                _lading.To_Province_Name = dr["TENTINHTRA"].ToString();
                                _lading.From_Province = dr["MATINHGOC"].ToString();
                                _lading.From_Province_Name = dr["TENTINHGOC"].ToString();
                                _lading.Amount = Convert.ToInt32(dr["SO_TIEN_THU_HO"]);
                                _lading.Note = dr["TRANGTHAI"].ToString();
                                _lading.Customer_Code = dr["MAKH"].ToString();
                                _lading.Transport_Fee = Convert.ToInt32(dr["CUOC_CHINH_PUBLIC"]);
                                _lading.Total_Fee = Convert.ToInt32(dr["CUOC_E1_PUBLIC"]);
                                _lading.Reference_Code = dr["SO_THAM_CHIEU"].ToString();
                            }
                            _response.Code = "200";
                            _response.Message = "Lấy dữ liệu thành công.";
                            _response.Data = _lading;

                        }

                    }
                    else
                    {
                        _response.Code = "404";
                        _response.Message = "Không có dữ liệu";
                        _response.Data = null;
                    }



                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "(region: getThongTinE1, Controller: E1InfoController):" + ex.Message);
                _response.Code = "500";
                _response.Message = "Lỗi xử lý dữ liệu";
                _response.Data = null;

            }
            return _response;
        }


        //OLD:

        public Lading getThongTinE1(String v_mae1)
        {
            Lading _lading = new Lading();
            try
            {
                OracleCommand cmd = new OracleCommand("EMS_TRACKING.GET_E1INFO");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("v_CODE", OracleDbType.Varchar2)).Value = v_mae1.Trim().ToUpper();
                cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                OracleParameter pr = cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                {
                    while (dr.Read())
                    {
                        _lading.Code = v_mae1;
                        _lading.From_PO = dr["BCGOC"].ToString();
                        _lading.To_PO = dr["BCTRA"].ToString();
                        _lading.ToCountry = dr["NUOCTRA"].ToString();
                        _lading.FromCountry = dr["NUOCNHAN"].ToString();
                        _lading.ReleasedDate = dr["NGAY"].ToString();
                        _lading.ReleasedTime = dr["GIO"].ToString();
                        _lading.Weight = Convert.ToInt32(dr["khoiluong"]);
                        _lading.Class = dr["phanloai"].ToString();
                        _lading.Service = dr["DVU"].ToString();
                        _lading.From_Name = dr["NGUOIGUI"].ToString();
                        _lading.From_Phone = dr["DIENTHOAIGUI"].ToString();
                        _lading.From_Address = dr["DIACHIGUI"].ToString();
                        _lading.To_Name = dr["NGUOINHAN"].ToString();
                        _lading.To_Phone = dr["DIENTHOAINHAN"].ToString();
                        _lading.To_Address = dr["DIACHI"].ToString();
                        _lading.To_Province = dr["MATINHTRA"].ToString();
                        _lading.To_Province_Name = dr["TENTINHTRA"].ToString();
                        _lading.From_Province = dr["MATINHGOC"].ToString();
                        _lading.From_Province_Name = dr["TENTINHGOC"].ToString();
                        _lading.Amount = Convert.ToInt32(dr["SO_TIEN_THU_HO"]);
                        _lading.Note = dr["TRANGTHAI"].ToString();
                        _lading.Customer_Code = dr["MAKH"].ToString();
                        _lading.Transport_Fee = Convert.ToInt32(dr["CUOC_CHINH_PUBLIC"]);
                        _lading.Total_Fee = Convert.ToInt32(dr["CUOC_E1_PUBLIC"]);
                        _lading.Reference_Code = dr["SO_THAM_CHIEU"].ToString();
                    }
                }
                else
                    _lading = null;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return _lading;
        }
        #endregion

        #region GetPodByE1
        public void Put_Pod_To_TMP_By_E1(String v_mae1, ref int v_type)
        {
            DataSet da = new DataSet();
            OracleCommand cmd = new OracleCommand("EMS.GetPodByE1");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new OracleParameter("v_MaE1", OracleDbType.Varchar2)).Value = v_mae1.ToUpper();
            cmd.Parameters.Add(new OracleParameter("v_type", OracleDbType.Int32)).Direction = ParameterDirection.Output;
            OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
            v_type = Int32.Parse(cmd.Parameters["v_type"].Value.ToString());
        }
        #endregion

        #region Get_Pod_From_TMP_By_E1_order
        public DataSet Get_Pod_From_TMP_By_E1_order(String v_mae1, int v_type)
        {
            DataSet da = new DataSet();
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT mae1, ngaynhan, gionhan, mabctra, tenbc, trangthai,ngnhan, lienlac, ldchuaphat, manctra, tennc, loai,       ngay_tra, NVL(ghi_chu,'-') as ghi_chu from Tmp_POD where MaE1='" + v_mae1.ToUpper() + "'" + " and loai=" + v_type.ToString() + " order by ngaynhan, gionhan");
                    // Mark the Command as a SPROC
                    cm.CommandType = CommandType.Text;
                    // Open the database connection and execute the command
                    OracleDataAdapter dr = new OracleDataAdapter(cm);
                    dr.Fill(da);
                    // Return the datareader
                    if (da.Tables[0].Rows.Count == 0)
                        da = null;
                    return da;
                }

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_Pod_From_TMP_By_E1
        public DataTable Get_Pod_From_TMP_By_E1(String v_mae1, int v_type)
        {
            DataTable da = new DataTable();
            using (OracleCommand cm = new OracleCommand())
            {
                cm.Connection = Helper.OraDCOracleConnection;
                cm.CommandText = string.Format("Select Mae1,ngaynhan,gionhan,mabctra,tenbc,trangthai,ngnhan,lienlac,ldchuaphat,manctra,tennc,loai,case when max(ghi_chu) is null then '-' else max(ghi_chu) end as ghi_chu,ngay_tra from Tmp_POD where MaE1='" + v_mae1.ToUpper() + "'" + " and loai=" + v_type.ToString() + " group by Mae1,ngaynhan,gionhan,mabctra,tenbc,trangthai,ngnhan,lienlac,ldchuaphat,manctra,tennc,loai,ngay_tra order by ngaynhan desc, gionhan desc");
                // Mark the Command as a SPROC
                cm.CommandType = CommandType.Text;
                // Open the database connection and execute the command
                OracleDataAdapter dr = new OracleDataAdapter(cm);
                dr.Fill(da);
                return da;
            }
        }
        #endregion

        #region GetDeliveryByE1
        public DataTable GetDeliveryByE1(String v_mae1)
        {
            DataTable da = new DataTable();

            OracleCommand cmd = new OracleCommand("EMS.GET_DELIVERY_BY_E1");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("v_MaE1", OracleDbType.Varchar2)).Value = v_mae1.ToUpper();
            OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
            //// Open the database connection and execute the command              


            using (OracleCommand cm = new OracleCommand())
            {
                cm.Connection = Helper.OraDCOracleConnection;
                string sqlQuery = "";
                sqlQuery = "Select mae1,mabctra,manctra as EDI_CODE,mabc,tenbc,chthu,tuiso,ngay,gio, ";
                sqlQuery += "khoiluong, lienlac, mabc_kt, tenbc_kt, huong, tennctra,    trangthai, ";
                sqlQuery += "diachi, qt from Tmp_Delivery where MaE1='" + v_mae1.ToUpper() + "'" + " order by ngay desc, gio desc";
                cm.CommandText = string.Format(sqlQuery);

                // Mark the Command as a SPROC
                cm.CommandType = CommandType.Text;

                // Open the database connection and execute the command
                OracleDataAdapter dr1 = new OracleDataAdapter(cm);
                dr1.Fill(da);
                // Return the datareader
            }
            return da;

        }
        #endregion

        #region GetDeliveryByE1_STC
        public DataTable GetDeliveryByE1_STC(String v_mae1)
        {
            DataTable da = new DataTable();
            string s_sql;
            using (OracleCommand cm = new OracleCommand())
            {
                cm.Connection = Helper.OraDCOracleConnection;
                s_sql = "Select * from Tmp_Delivery where So_Tham_Chieu='" + v_mae1 + "' Order by ngay desc, gio desc";
                cm.CommandText = string.Format(s_sql);

                // Mark the Command as a SPROC
                cm.CommandType = CommandType.Text;

                // Open the database connection and execute the command
                OracleDataAdapter dr1 = new OracleDataAdapter(cm);
                dr1.Fill(da);
                return da;
            }
        }
        #endregion

        #region Get E1 by STC
        public string GET_E1_BY_STC(string code)
        {
            using (OracleCommand cm = new OracleCommand())
            {
                string _mae1 = "";
                cm.Connection = Helper.OraDCOracleConnection;
                cm.CommandText = string.Format("SELECT MAE1 FROM E1E2_PH WHERE SO_THAM_CHIEU = '" + code + "' and ROWNUM=1");
                cm.CommandType = CommandType.Text;
                using (OracleDataReader dr = cm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _mae1 = dr["MAE1"].ToString();
                    }
                }
                return _mae1;
            }
        }
        #endregion

        #region Chk E1
        public Boolean CHK_E1NH(string MAE1)
        {

            int myVal = 0;
            using (OracleCommand cm = new OracleCommand())
            {
                cm.Connection = Helper.OraDCOracleConnection;
                cm.CommandText = string.Format("select count(*) from E1NH_2015 where MAE1='" + MAE1 + "' AND MABCTRA>0");
                cm.CommandType = CommandType.Text;
                try
                {
                    myVal = Convert.ToInt32(cm.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                if (myVal == 0)
                    return false;
                else
                    return true;
            }

        }
        #endregion

        #region TTS_DELETIVERY
        public void TTS_DELETIVERY(String v_mae1)
        {
            DataSet da = new DataSet();


            OracleCommand cmd = new OracleCommand("EMS.TTS_DELETIVERY");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("v_MaE1", OracleDbType.Varchar2)).Value = v_mae1.ToUpper();
            OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
            //// Open the database connection and execute the command               




        }
        #endregion

        #region GetDeliveryByE1MultiData
        public DataTable GetDeliveryByE1MultiData(String v_mae1)
        {
            DataTable da = new DataTable();
            using (OracleCommand cm = new OracleCommand())
            {
                cm.Connection = Helper.OraDCOracleConnection;
                string sqlQuery = "";
                sqlQuery = "   Select  mae1, cast(mabctra as varchar2(9)) as mabctra, '' as STATUSTEXT,manctra as EDI_CODE,mabc,tenbc,chthu,tuiso,ngay,gio,khoiluong, lienlac, mabc_kt, tenbc_kt, huong, tennctra, trangthai, diachi, qt from Tmp_Delivery where MaE1 IN (" + v_mae1.ToUpper() + ")";
                sqlQuery += "union";
                sqlQuery += "    Select mae1, mabctra,case Tmp_POD.trangthai when 'H' then  ( SELECT tenld||'-'||tenxl as anhtu FROM xuly,LYDO where maXL=SUBSTR(ngnhan,1,1) and mald=SUBSTR(ngnhan,2,3)) else  ngnhan end AS STATUSTEXT,'' as EDI_CODE,0 as mabc,'' as tenbc,0 as chthu,0 as tuiso,ngaynhan as ngay,gionhan as gio,0 as khoiluong,'' as lienlac,0 as mabc_kt , '' as tenbc_kt, cast(0 as varchar2(50)) as huong, '' as tennctra,trangthai as trangthai,cast('' as nvarchar2(200)) as diachi,  0 as qt from Tmp_POD where MaE1 IN  (" + v_mae1.ToUpper() + ")and loai=0 order by mae1, ngay, gio ";
                cm.CommandText = string.Format(sqlQuery);
                // Mark the Command as a SPROC
                cm.CommandType = CommandType.Text;
                // Open the database connection and execute the command
                OracleDataAdapter dr = new OracleDataAdapter(cm);
                dr.Fill(da);
                // Return the datareader
                return da;
            }
        }
        #endregion

        #region Get_Pod_From_TMP_By_E1
        public DataTable Get_Pod_From_TMP_By_MultiE1(String v_mae1, int v_type)
        {
            DataTable da = new DataTable();
            using (OracleCommand cm = new OracleCommand())
            {
                cm.Connection = Helper.OraDCOracleConnection;
                cm.CommandText = string.Format("Select * from Tmp_POD where MaE1 IN (" + v_mae1.ToUpper() + ") and loai=" + v_type.ToString() + " order by mae1,ngaynhan,gionhan");
                // Mark the Command as a SPROC
                cm.CommandType = CommandType.Text;

                // Open the database connection and execute the command
                OracleDataAdapter dr = new OracleDataAdapter(cm);

                dr.Fill(da);

                // Return the datareader
                return da;

            }
        }
        #endregion

        #region SET_STATUSINT
        public Boolean SET_STATUSINT(String TRACKING_CODE)
        {
            try
            {
                int value = 0;
                OracleCommand cmd = new OracleCommand("EMS_TRACKING.SET_STATUSINT");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_TRACKING_CODE", OracleDbType.Varchar2)).Value = TRACKING_CODE;
                cmd.Parameters.Add("V_RETURN", OracleDbType.Int32, 0, ParameterDirection.Output);
                OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                value = int.Parse(cmd.Parameters["V_RETURN"].Value.ToString());
                if (value > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                return false;
            }
        }
        #endregion

        #region SQL SERVER

        #region Lấy Sự kiện GET_EDI_EVENT
        public String GET_EDI_EVENT(string m_event, string m_language)
        {
            string return_value;
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString_7"]);
            SqlCommand myCommand = new SqlCommand("GET_EDI_EVENT", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC            
            myCommand.Parameters.Add(new SqlParameter("@event_code", SqlDbType.VarChar, 1)).Value = m_event;
            myCommand.Parameters.Add(new SqlParameter("@language", SqlDbType.VarChar, 2)).Value = m_language;
            myCommand.Parameters.Add(new SqlParameter("@event", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            return_value = myCommand.Parameters["@event"].Value.ToString();
            myConnection.Close();

            // Return the DataSet
            return return_value;
        }
        #endregion

        #region Lấy Tên BC
        public string GET_TEN_BC_BY_MA_BC(string Ma_BC)
        {
            string return_value;
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString_7"]);
            SqlCommand myCommand = new SqlCommand("GET_TEN_BC_BY_MA_BC", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC            
            myCommand.Parameters.Add(new SqlParameter("@Ma_BC", SqlDbType.VarChar, 7)).Value = Ma_BC;
            myCommand.Parameters.Add(new SqlParameter("@TEN_BC", SqlDbType.NVarChar, 200)).Direction = ParameterDirection.Output;

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            return_value = myCommand.Parameters["@TEN_BC"].Value.ToString();
            myConnection.Close();
            return return_value;
        }
        #endregion

        #region Lấy Tên NC
        public string GET_TEN_NC_BY_MA_NC(string Ma_NC)
        {
            string return_value;
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString_7"]);
            SqlCommand myCommand = new SqlCommand("GET_TEN_NC_BY_MA_NC", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC             
            myCommand.Parameters.Add(new SqlParameter("@MA_NC", SqlDbType.VarChar, 2)).Value = Ma_NC;
            myCommand.Parameters.Add(new SqlParameter("@Ten_NC", SqlDbType.NVarChar, 100)).Direction = ParameterDirection.Output;

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            return_value = myCommand.Parameters["@Ten_NC"].Value.ToString();
            myConnection.Close();
            return return_value;
        }
        #endregion

        #region Lấy Lý do  GET_LY_DO
        public String GET_LY_DO(string m_event, string m_language)
        {
            string return_value;
            try
            {
                // Create Instance of Connection and Command Object
                SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString_7"]);
                SqlCommand myCommand = new SqlCommand("GET_LY_DO", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                // Add Parameters to SPROC            
                myCommand.Parameters.Add(new SqlParameter("@Ma_ly_do", SqlDbType.VarChar, 3)).Value = m_event;
                myCommand.Parameters.Add(new SqlParameter("@language", SqlDbType.VarChar, 2)).Value = m_language;
                myCommand.Parameters.Add(new SqlParameter("@Ten_Ly_Do", SqlDbType.NVarChar, 200)).Direction = ParameterDirection.Output;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                return_value = myCommand.Parameters["@Ten_Ly_Do"].Value.ToString();
                myConnection.Close();
                // Return the DataSet

            }
            catch
            {
                return_value = "Se co gang phat trong ngay hom sau";
            }
            return return_value;
        }
        #endregion

        #region Lấy Hướng xử lý  GET_XU_LY
        public String GET_XU_LY(string m_event, string m_language)
        {
            string return_value;
            try
            {
                // Create Instance of Connection and Command Object
                SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString_7"]);
                SqlCommand myCommand = new SqlCommand("GET_XU_LY", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                // Add Parameters to SPROC            
                myCommand.Parameters.Add(new SqlParameter("@Ma_xu_ly", SqlDbType.VarChar, 1)).Value = m_event;
                myCommand.Parameters.Add(new SqlParameter("@language", SqlDbType.VarChar, 2)).Value = m_language;
                myCommand.Parameters.Add(new SqlParameter("@Ten_Xu_Ly", SqlDbType.NVarChar, 200)).Direction = ParameterDirection.Output;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                return_value = myCommand.Parameters["@Ten_Xu_Ly"].Value.ToString();
                myConnection.Close();
            }
            catch
            {
                // Return the DataSet
                return_value = "Khong co nguoi nhan hoac nguoi nhan di vang";
            }
            return return_value;
            // Return the DataSet            
        }
        #endregion

        #region GET_E1
        public String GET_E1(string Ma_KH_E_Shipping, int Buu_Cuc_Khai_Thac, string Nuoc_Nhan)
        {
            string return_value;
            try
            {
                // Create Instance of Connection and Command Object
                SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]);
                SqlCommand myCommand = new SqlCommand("E1_Autogeneration_KH", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                // Add Parameters to SPROC            
                myCommand.Parameters.Add(new SqlParameter("@Ma_KH_E_Shipping", SqlDbType.VarChar, 50)).Value = Ma_KH_E_Shipping;
                myCommand.Parameters.Add(new SqlParameter("@Buu_Cuc_Khai_Thac", SqlDbType.Int, 50)).Value = Buu_Cuc_Khai_Thac;
                myCommand.Parameters.Add(new SqlParameter("@Nuoc_Nhan", SqlDbType.VarChar, 2)).Value = Nuoc_Nhan;
                myCommand.Parameters.Add(new SqlParameter("@Ma_E1_Return", SqlDbType.VarChar, 13)).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                return_value = myCommand.Parameters["@Ma_E1_Return"].Value.ToString();
                myConnection.Close();
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "EMSRepository.GET_E1: " + ex.Message);
                return_value = " ";
            }
            // Return the DataSet
            return return_value;
        }
        #endregion

        #endregion

        #region Postage

        public double Get_Postage(int from, int to, int weight)
        {
            double return_value = 0;
            try
            {
                // Create Instance of Connection and Command Object
                SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString_7"]);
                SqlCommand myCommand = new SqlCommand("dbo.Cuoc_Chinh_E1_CPN_0109", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                // Add Parameters to SPROC          
                myCommand.Parameters.Add(new SqlParameter("@Ma_Bc_Goc", SqlDbType.Int)).Value = from;
                myCommand.Parameters.Add(new SqlParameter("@Nuoc_Nhan", SqlDbType.VarChar, 2)).Value = "VN";
                myCommand.Parameters.Add(new SqlParameter("@Ma_Bc_Tra", SqlDbType.Int)).Value = to;
                myCommand.Parameters.Add(new SqlParameter("@Nuoc_Tra", SqlDbType.VarChar, 2)).Value = "VN";
                myCommand.Parameters.Add(new SqlParameter("@PostCode", SqlDbType.VarChar, 50)).Value = "";
                myCommand.Parameters.Add(new SqlParameter("@Phan_Loai_BP", SqlDbType.VarChar, 1)).Value = "D";
                myCommand.Parameters.Add(new SqlParameter("@Khoi_Luong", SqlDbType.Int)).Value = weight;
                myCommand.Parameters.Add(new SqlParameter("@Ngay_Gui", SqlDbType.Int)).Value = 20180601;
                myCommand.Parameters.Add(new SqlParameter("@Loai", SqlDbType.Int)).Value = 0;
                myCommand.Parameters.Add(new SqlParameter("@Cuoc_PPXD", SqlDbType.Int)).Value = 0;
                myCommand.Parameters.Add(new SqlParameter("@Ma_KH", SqlDbType.VarChar, 50)).Value = "";

                SqlParameter parameterReturnValue = new SqlParameter("@nReturn", SqlDbType.Float);
                parameterReturnValue.Direction = ParameterDirection.ReturnValue;
                myCommand.Parameters.Add(parameterReturnValue);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                return_value = Convert.ToDouble(myCommand.Parameters["@nReturn"].Value.ToString());
                myConnection.Close();
                //Insert 22/10/2018
                //myConnection.Dispose(); 
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "EMSRepository.Get_Postage: " + ex.Message);
                return_value = 0;
            }
            // Return the DataSet
            return return_value;
        }
            #endregion

        #region COD fee
        public int Get_Cod_Fee(int gia_tri)
        {
            Exchange exchange = new Exchange();
            int cod_fee;
            try
            {
                // Create Instance of Connection and Command Object
                SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString_7"]);
                SqlCommand myCommand = new SqlCommand("Tinh_Cuoc_Dich_Vu_COD_New", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                // Add Parameters to SPROC            

                myCommand.Parameters.Add(new SqlParameter("@Ma_Bc_Goc", SqlDbType.Int, 50)).Value = 0;
                myCommand.Parameters.Add(new SqlParameter("@Ma_Bc_Tra", SqlDbType.Int, 50)).Value = 0;
                myCommand.Parameters.Add(new SqlParameter("@Ma_Dich_Vu", SqlDbType.VarChar, 5)).Value = "COD";
                myCommand.Parameters.Add(new SqlParameter("@Ngay_Gui", SqlDbType.Int, 50)).Value = exchange.DateToInt();                
                myCommand.Parameters.Add(new SqlParameter("@Gia_Tri", SqlDbType.Int, 50)).Value = gia_tri;
                myCommand.Parameters.Add(new SqlParameter("@Cuoc_COD", SqlDbType.Int)).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                cod_fee = int.Parse(myCommand.Parameters["@Cuoc_COD"].Value.ToString());
                myConnection.Close();
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "EMSRepository.Get_Cod_Fee: " + ex.Message);
                cod_fee = 0;
            }
            // Return the DataSet
            return cod_fee;
        }
        #endregion

        #region Get ShipmentInfor
        //public ShipmentEntity GetShipment(string ShipmentNo)
        //{


        //    ShipmentEntity shipment = new ShipmentEntity();
        //    try
        //    {
        //        using (SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["SqlConnectionString"]))
        //        {
        //            using (SqlCommand command = new SqlCommand())
        //            {
        //                string oString = "select * from ShipmentInfor where ShipmentId='" + ShipmentNo + "';";
        //                SqlCommand oCmd = new SqlCommand(oString, myConnection);
        //                myConnection.Open();
        //                using (SqlDataReader oReader = oCmd.ExecuteReader())
        //                {
        //                    while (oReader.Read())
        //                    {
        //                        shipment.Id = int.Parse(oReader["Id"].ToString());
        //                        shipment.ShipmentId = oReader["ShipmentId"].ToString();
        //                        shipment.DateCreated = DateTime.Parse(oReader["DateCreated"].ToString());
        //                        shipment.Sender = oReader["Sender"].ToString();
        //                        shipment.Receiver = oReader["Receiver"].ToString();
        //                        shipment.ReceiverTel = oReader["TelReceiver"].ToString();
        //                        shipment.Description = oReader["Descrition"].ToString();
        //                        shipment.BoxId = int.Parse(oReader["BoxId"].ToString());
        //                        shipment.Weight = double.Parse(oReader["Weight"].ToString());
        //                        shipment.DeclarationNo = oReader["DeclarationNo"].ToString();
        //                        shipment.Consignee = oReader["Consignee"].ToString();
        //                        shipment.Address = oReader["Address"].ToString();
        //                        shipment.Content = oReader["Content"].ToString();
        //                    }
        //                    myConnection.Close();
        //                }
        //            }
        //        }
        //        return shipment;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //}
        #endregion
    }
}
