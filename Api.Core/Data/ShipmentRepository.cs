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
    public class ShipmentRepository
    {
        #region SC
        public List<Shipment> GET_SHIPMENT(string customer_code)
        {
            List<Shipment> _lstShipment = new List<Shipment>();
            Shipment objShipment = null;
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    //   cm.CommandText = string.Format("SELECT * FROM E1_SHIPCHUNG P WHERE P.STATUSINT = 0 and Customercode='" + customer_code + "' order by P.date_s asc");
                    cm.CommandText = string.Format("SELECT * FROM E1_SHIPCHUNG P WHERE TRACKINGNUMBER='SC5365020457' order by P.date_s asc");
                    cm.CommandType = CommandType.Text;
                    OracleDataReader dr = Helper.ExecuteDataReader(cm, Helper.OraDCOracleConnection);
                    while (dr.Read())
                    {
                        objShipment = new Shipment();
                        objShipment.CUSTOMER_CODE = dr["CUSTOMERCODE"].ToString();
                        objShipment.MAE1 = dr["MAE1"].ToString();
                        objShipment.TRACKING_NUMBER = dr["TRACKINGNUMBER"].ToString();
                        objShipment.ORDER_NUMBER = dr["ORDERNUMBER"].ToString();
                        _lstShipment.Add(objShipment);
                    }
                }
                return _lstShipment;
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "ShipmentRepository.GetShipment: " + ex.Message);
                _lstShipment = null;
            }
            return _lstShipment;
        }

        #endregion

        #region CREATE SHIPMENT

        #region SHIPMENT_CREATE

        public ReponseEntity SHIPMENT_CREATE(CShipment CShipment)
        {
            MongoRepository _mongoRepository = new MongoRepository();
            EmsRepository _emsRepository = new EmsRepository();
            CShipment shipment = new CShipment();
            ReponseEntity oReponseEntity = new ReponseEntity();
            int po = Convert.ToInt32(CShipment.PO_CREATE + "0000");
            try
            {
                if (TrackingCode_CHK(CShipment.ORDER_CODE) == false)
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        string _mae1 = "";
                        _mae1 = _emsRepository.GET_E1("HN_Dvkh", po, "VN");
                        //_mae1 = "EI714091535VN";
                        cmd.Connection = Helper.OraDCOracleConnection;
                        cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_CREATE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_ID", OracleDbType.Int32, ParameterDirection.ReturnValue);
                        cmd.Parameters.Add("v_ORDER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.ORDER_CODE;
                        cmd.Parameters.Add("v_TRACKING_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.TRACKING_CODE;
                        cmd.Parameters.Add("v_CUSTOMER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.CUSTOMER_CODE;
                        cmd.Parameters.Add("v_STORE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.STORE_ID;
                        cmd.Parameters.Add("v_SENDER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_CODE;
                        cmd.Parameters.Add("v_SENDER_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_NAME;
                        cmd.Parameters.Add("v_SENDER_PHONE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_PHONE;
                        cmd.Parameters.Add("v_SENDER_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_EMAIL;
                        cmd.Parameters.Add("v_SENDER_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_PROVINCE_ID;
                        cmd.Parameters.Add("v_SENDER_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_DISTRICT_ID;
                        cmd.Parameters.Add("v_SENDER_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_WARD_ID;
                        cmd.Parameters.Add("v_SENDER_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_HAMLET_ID;
                        cmd.Parameters.Add("v_SENDER_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_STREET;
                        cmd.Parameters.Add("v_SENDER_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_ADDRESS;
                        cmd.Parameters.Add("v_RECEIVER_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_NAME;
                        cmd.Parameters.Add("v_RECEIVER_EMAIL", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_EMAIL;
                        cmd.Parameters.Add("v_RECEIVER_PHONE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_PHONE;
                        cmd.Parameters.Add("v_RECEIVER_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_PROVINCE_ID;
                        cmd.Parameters.Add("v_RECEIVER_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_DISTRICT_ID;
                        cmd.Parameters.Add("v_RECEIVER_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_WARD_ID;
                        cmd.Parameters.Add("v_RECEIVER_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_HAMLET_ID;
                        cmd.Parameters.Add("v_RECEIVER_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_STREET;
                        cmd.Parameters.Add("v_RECEIVER_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_ADDRESS;
                        cmd.Parameters.Add("v_PRODUCT_QUANTITY", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.PRODUCT_QUANTITY;
                        cmd.Parameters.Add("v_PRODUCT_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_NAME;
                        cmd.Parameters.Add("v_PRODUCT_DESCRIPTION", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_DESCRIPTION;
                        cmd.Parameters.Add("v_STATUS", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.STATUS;
                        cmd.Parameters.Add("v_TOTAL_AMOUNT", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.TOTAL_AMOUNT;
                        cmd.Parameters.Add("v_WEIGHT", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.WEIGHT;
                        cmd.Parameters.Add("v_SERVICE_TYPE", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SERVICE_TYPE;
                        cmd.Parameters.Add("v_CHANNEL", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.CHANNEL;
                        cmd.Parameters.Add("v_PO_CREATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.PO_CREATE;
                        cmd.Parameters.Add("v_REF_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = _mae1;
                        cmd.Parameters.Add("v_FILE_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.FILE_NAME;
                        cmd.Parameters.Add("v_PRODUCT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_CODE;
                        cmd.Parameters.Add("v_SYSTEM_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SYSTEM_ID;
                        cmd.Parameters.Add("v_COD", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.COD;
                        cmd.Parameters.Add("v_PRODUCT_VALUE", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.PRODUCT_VALUE;
                        cmd.Parameters.Add("v_TO_COUNTRY", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.TO_COUNTRY;
                        cmd.Parameters.Add("v_TO_ZIPCODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.TO_ZIPCODE;
                        cmd.Parameters.Add("v_MASTER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.MASTER_CODE;
                        cmd.Parameters.Add("v_ADDITION_SERVICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.ADDITION_SERVICE;
                        cmd.Parameters.Add("v_SPECIAL_SERVICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SPECIAL_SERVICE;

                        cmd.ExecuteNonQuery();
                        var id = Convert.ToInt32(cmd.Parameters["P_ID"].Value.ToString());

                        if (id > 0)
                        {
                            oReponseEntity.Code = "00";
                            oReponseEntity.Message = "Cập nhật dữ liệu thành công";
                            oReponseEntity.Value = _mae1;
                        }
                        else
                        {
                            oReponseEntity.Code = "-99";
                            oReponseEntity.Message = "Lỗi cập nhật dữ liệu";
                            oReponseEntity.Value = string.Empty;
                        }
                    }

                }
                else
                {
                    oReponseEntity.Code = "01";
                    oReponseEntity.Message = "Order đã tồn tại";
                    oReponseEntity.Value = CShipment.ORDER_CODE;
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

        public ReponseEntity SHIPMENT_CREATE_FROM_FILE(CShipment CShipment)
        {
            EmsRepository _emsRepository = new EmsRepository();
            CShipment shipment = new CShipment();
            ReponseEntity oReponseEntity = new ReponseEntity();
            try
            {
                if (TrackingCode_CHK(CShipment.ORDER_CODE) == false)
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = Helper.OraDCOracleConnection;
                        cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_CREATE_FROM_FILE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_ID", OracleDbType.Int32, ParameterDirection.ReturnValue);
                        cmd.Parameters.Add("v_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.ID;
                        cmd.Parameters.Add("v_ORDER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.ORDER_CODE;
                        cmd.Parameters.Add("v_TRACKING_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.TRACKING_CODE;
                        cmd.Parameters.Add("v_CUSTOMER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.CUSTOMER_CODE;
                        cmd.Parameters.Add("v_STORE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.STORE_ID;
                        cmd.Parameters.Add("v_SENDER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_CODE;
                        cmd.Parameters.Add("v_SENDER_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_NAME;
                        cmd.Parameters.Add("v_SENDER_PHONE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_PHONE;
                        cmd.Parameters.Add("v_SENDER_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_EMAIL;
                        cmd.Parameters.Add("v_SENDER_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_PROVINCE_ID;
                        cmd.Parameters.Add("v_SENDER_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_DISTRICT_ID;
                        cmd.Parameters.Add("v_SENDER_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_WARD_ID;
                        cmd.Parameters.Add("v_SENDER_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_HAMLET_ID;
                        cmd.Parameters.Add("v_SENDER_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_STREET;
                        cmd.Parameters.Add("v_SENDER_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_ADDRESS;
                        cmd.Parameters.Add("v_RECEIVER_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_NAME;
                        cmd.Parameters.Add("v_RECEIVER_EMAIL", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_EMAIL;
                        cmd.Parameters.Add("v_RECEIVER_PHONE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_PHONE;
                        cmd.Parameters.Add("v_RECEIVER_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_PROVINCE_ID;
                        cmd.Parameters.Add("v_RECEIVER_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_DISTRICT_ID;
                        cmd.Parameters.Add("v_RECEIVER_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_WARD_ID;
                        cmd.Parameters.Add("v_RECEIVER_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_HAMLET_ID;
                        cmd.Parameters.Add("v_RECEIVER_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_STREET;
                        cmd.Parameters.Add("v_RECEIVER_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_ADDRESS;
                        cmd.Parameters.Add("v_PRODUCT_QUANTITY", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.PRODUCT_QUANTITY;
                        cmd.Parameters.Add("v_PRODUCT_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_NAME;
                        cmd.Parameters.Add("v_PRODUCT_DESCRIPTION", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_DESCRIPTION;
                        cmd.Parameters.Add("v_STATUS", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.STATUS;
                        cmd.Parameters.Add("v_TOTAL_AMOUNT", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.TOTAL_AMOUNT;
                        cmd.Parameters.Add("v_WEIGHT", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.WEIGHT;
                        cmd.Parameters.Add("v_SERVICE_TYPE", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SERVICE_TYPE;
                        cmd.Parameters.Add("v_CHANNEL", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.CHANNEL;
                        cmd.Parameters.Add("v_PO_CREATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.PO_CREATE;
                        cmd.Parameters.Add("v_REF_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.REF_CODE;
                        cmd.Parameters.Add("v_FILE_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.FILE_NAME;
                        cmd.Parameters.Add("v_PRODUCT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_CODE;
                        cmd.Parameters.Add("v_SYSTEM_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SYSTEM_ID;
                        cmd.Parameters.Add("v_COD", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.COD;
                        cmd.Parameters.Add("v_PRODUCT_VALUE", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.PRODUCT_VALUE;

                        cmd.ExecuteNonQuery();
                        var id = Convert.ToInt32(cmd.Parameters["P_ID"].Value.ToString());

                        if (id > 0)
                        {
                            oReponseEntity.Code = "00";
                            oReponseEntity.Message = "Cập nhật dữ liệu thành công";
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
                else
                {
                    oReponseEntity.Code = "01";
                    oReponseEntity.Message = "Order đã tồn tại";
                    oReponseEntity.Value = CShipment.ORDER_CODE;
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
        #region SHIPMENT_CREATE_PARTNER - Produciton
        public ReponseEntity SHIPMENT_CREATE_PARTNER(CShipment CShipment)
        {           
            CShipment shipment = new CShipment();
            ReponseEntity oReponseEntity = new ReponseEntity();
            int po = CShipment.SENDER_PROVINCE_ID;
            try
            {
                OracleCommand cmd = new OracleCommand(Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_CREATE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ID", OracleDbType.Int32, ParameterDirection.ReturnValue);
                cmd.Parameters.Add("v_ORDER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.ORDER_CODE.Trim().ToUpper();
                cmd.Parameters.Add("v_TRACKING_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.TRACKING_CODE;
                cmd.Parameters.Add("v_CUSTOMER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.CUSTOMER_CODE;
                cmd.Parameters.Add("v_STORE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.STORE_ID;
                cmd.Parameters.Add("v_SENDER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_CODE;
                cmd.Parameters.Add("v_SENDER_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_NAME;
                cmd.Parameters.Add("v_SENDER_PHONE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_PHONE;
                cmd.Parameters.Add("v_SENDER_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_EMAIL;
                cmd.Parameters.Add("v_SENDER_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_PROVINCE_ID;
                cmd.Parameters.Add("v_SENDER_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_DISTRICT_ID;
                cmd.Parameters.Add("v_SENDER_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_WARD_ID;
                cmd.Parameters.Add("v_SENDER_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_HAMLET_ID;
                cmd.Parameters.Add("v_SENDER_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_STREET;
                cmd.Parameters.Add("v_SENDER_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_ADDRESS;
                cmd.Parameters.Add("v_RECEIVER_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_NAME;
                cmd.Parameters.Add("v_RECEIVER_EMAIL", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_EMAIL;
                cmd.Parameters.Add("v_RECEIVER_PHONE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_PHONE;
                cmd.Parameters.Add("v_RECEIVER_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_PROVINCE_ID;
                cmd.Parameters.Add("v_RECEIVER_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_DISTRICT_ID;
                cmd.Parameters.Add("v_RECEIVER_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_WARD_ID;
                cmd.Parameters.Add("v_RECEIVER_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_HAMLET_ID;
                cmd.Parameters.Add("v_RECEIVER_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_STREET;
                cmd.Parameters.Add("v_RECEIVER_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_ADDRESS;
                cmd.Parameters.Add("v_PRODUCT_QUANTITY", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.PRODUCT_QUANTITY;
                cmd.Parameters.Add("v_PRODUCT_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_NAME;
                cmd.Parameters.Add("v_PRODUCT_DESCRIPTION", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_DESCRIPTION;
                cmd.Parameters.Add("v_STATUS", OracleDbType.Varchar2, ParameterDirection.Input).Value = "C1";
                cmd.Parameters.Add("v_TOTAL_AMOUNT", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.TOTAL_AMOUNT;
                cmd.Parameters.Add("v_WEIGHT", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.WEIGHT;
                cmd.Parameters.Add("v_SERVICE_TYPE", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SERVICE_TYPE;
                cmd.Parameters.Add("v_CHANNEL", OracleDbType.Varchar2, ParameterDirection.Input).Value = "API";
                cmd.Parameters.Add("v_PO_CREATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.PO_CREATE;
                cmd.Parameters.Add("v_REF_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.REF_CODE;
                cmd.Parameters.Add("v_FILE_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.FILE_NAME;
                cmd.Parameters.Add("v_PRODUCT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_CODE;
                cmd.Parameters.Add("v_SYSTEM_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = "KH";
                cmd.Parameters.Add("v_COD", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.COD;
                cmd.Parameters.Add("v_PRODUCT_VALUE", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.PRODUCT_VALUE;
                cmd.Parameters.Add("v_TO_COUNTRY", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.TO_COUNTRY;
                cmd.Parameters.Add("v_TO_ZIPCODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.TO_ZIPCODE;
                cmd.Parameters.Add("v_MASTER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.MASTER_CODE;
                cmd.Parameters.Add("v_ADDITION_SERVICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.ADDITION_SERVICE;
                cmd.Parameters.Add("v_SPECIAL_SERVICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SPECIAL_SERVICE;
                OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                var id = Convert.ToInt32(cmd.Parameters["P_ID"].Value.ToString());
                if (id > 0)
                {
                    oReponseEntity.Code = "00";
                    oReponseEntity.Message = "Success";
                    oReponseEntity.Value = CShipment.REF_CODE;
                    oReponseEntity.Id = id;
                }
                else if (id == -1)
                {
                    oReponseEntity.Code = "01";
                    oReponseEntity.Message = "Order is existing";
                    oReponseEntity.Value = string.Empty;
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
                oReponseEntity.Message = "System processing error";
                oReponseEntity.Value = string.Empty;
                
            }
            return oReponseEntity;
        }
        #endregion
        #region SHIPMENT_TMP_CREATE
        public ReponseEntity SHIPMENT_TMP_CREATE(CShipment CShipment)
        {
            MongoRepository _mongoRepository = new MongoRepository();
            AutogenCodeRepository _autoGenCodeRepository = new AutogenCodeRepository();
            EmsRepository _emsRepository = new EmsRepository();
            CShipment shipment = new CShipment();
            ReponseEntity oReponseEntity = new ReponseEntity();
            int po = Convert.ToInt32(CShipment.PO_CREATE + "0000");

            try
            {

                using (OracleCommand cmd = new OracleCommand())
                {
                    string _mae1 = "";
                    _mae1 = _emsRepository.GET_E1("HN_Dvkh", po, "VN");
                    //_mae1 = "EI714091535VN";
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_TMP_CREATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_ID", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("v_ORDER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.ORDER_CODE;
                    cmd.Parameters.Add("v_TRACKING_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.TRACKING_CODE;
                    cmd.Parameters.Add("v_CUSTOMER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.CUSTOMER_CODE;
                    cmd.Parameters.Add("v_STORE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.STORE_ID;
                    cmd.Parameters.Add("v_SENDER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_CODE;
                    cmd.Parameters.Add("v_SENDER_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_NAME;
                    cmd.Parameters.Add("v_SENDER_PHONE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_PHONE;
                    cmd.Parameters.Add("v_SENDER_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_EMAIL;
                    cmd.Parameters.Add("v_SENDER_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_PROVINCE_ID;
                    cmd.Parameters.Add("v_SENDER_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_DISTRICT_ID;
                    cmd.Parameters.Add("v_SENDER_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_WARD_ID;
                    cmd.Parameters.Add("v_SENDER_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_HAMLET_ID;
                    cmd.Parameters.Add("v_SENDER_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_STREET;
                    cmd.Parameters.Add("v_SENDER_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_ADDRESS;
                    cmd.Parameters.Add("v_RECEIVER_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_NAME;
                    cmd.Parameters.Add("v_RECEIVER_EMAIL", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_EMAIL;
                    cmd.Parameters.Add("v_RECEIVER_PHONE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_PHONE;
                    cmd.Parameters.Add("v_RECEIVER_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_PROVINCE_ID;
                    cmd.Parameters.Add("v_RECEIVER_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_DISTRICT_ID;
                    cmd.Parameters.Add("v_RECEIVER_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_WARD_ID;
                    cmd.Parameters.Add("v_RECEIVER_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_HAMLET_ID;
                    cmd.Parameters.Add("v_RECEIVER_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_STREET;
                    cmd.Parameters.Add("v_RECEIVER_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_ADDRESS;
                    cmd.Parameters.Add("v_PRODUCT_QUANTITY", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.PRODUCT_QUANTITY;
                    cmd.Parameters.Add("v_PRODUCT_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_NAME;
                    cmd.Parameters.Add("v_PRODUCT_DESCRIPTION", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_DESCRIPTION;
                    cmd.Parameters.Add("v_STATUS", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.STATUS;
                    cmd.Parameters.Add("v_TOTAL_AMOUNT", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.TOTAL_AMOUNT;
                    cmd.Parameters.Add("v_WEIGHT", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.WEIGHT;
                    cmd.Parameters.Add("v_SERVICE_TYPE", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SERVICE_TYPE;
                    cmd.Parameters.Add("v_CHANNEL", OracleDbType.Varchar2, ParameterDirection.Input).Value = "XLS";
                    cmd.Parameters.Add("v_PO_CREATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.PO_CREATE;
                    cmd.Parameters.Add("v_REF_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = _mae1;
                    //cmd.Parameters.Add("v_REF_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = _autoGenCodeRepository.CreateAutoGenCode(CShipment.CUSTOMER_CODE, CShipment.SENDER_PROVINCE_ID);
                    cmd.Parameters.Add("v_FILE_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.FILE_NAME;
                    cmd.Parameters.Add("v_PRODUCT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_CODE;
                    cmd.Parameters.Add("v_SYSTEM_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = "KH";
                    cmd.Parameters.Add("v_COD", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.COD;
                    cmd.Parameters.Add("v_PRODUCT_VALUE", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.PRODUCT_VALUE;
                    cmd.ExecuteNonQuery();
                    var id = Convert.ToInt32(cmd.Parameters["P_ID"].Value.ToString());

                    if (id > 0)
                    {
                        oReponseEntity.Code = "00";
                        oReponseEntity.Message = "Cập nhật dữ liệu thành công";
                        oReponseEntity.Value = _mae1;
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
        #region LIST_SHIPMENT_CREATE
        public ReponseEntity LIST_SHIPMENT_CREATE(List<CShipment> listCShipment)
        {
            CShipment shipment = new CShipment();
            ReponseEntity oReponseEntity = new ReponseEntity();
            //OracleTransaction transaction;
            //transaction = Helper.OraDCOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);

            OracleTransaction transaction = Helper.OraDCOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                foreach (CShipment CShipment in listCShipment)
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = Helper.OraDCOracleConnection;
                        cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.LIST_SHIPMENT_CREATE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Transaction = transaction;
                        cmd.CommandTimeout = 20000;
                        cmd.Parameters.Add("P_ID", OracleDbType.Int32, ParameterDirection.ReturnValue);
                        cmd.Parameters.Add("v_ORDER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.ORDER_CODE;
                        cmd.Parameters.Add("v_TRACKING_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.TRACKING_CODE;
                        cmd.Parameters.Add("v_CUSTOMER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.CUSTOMER_CODE;
                        cmd.Parameters.Add("v_STORE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.STORE_ID;
                        cmd.Parameters.Add("v_SENDER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_CODE;
                        cmd.Parameters.Add("v_SENDER_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_NAME;
                        cmd.Parameters.Add("v_SENDER_PHONE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_PHONE;
                        cmd.Parameters.Add("v_SENDER_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SENDER_EMAIL;
                        cmd.Parameters.Add("v_SENDER_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_PROVINCE_ID;
                        cmd.Parameters.Add("v_SENDER_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_DISTRICT_ID;
                        cmd.Parameters.Add("v_SENDER_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_WARD_ID;
                        cmd.Parameters.Add("v_SENDER_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SENDER_HAMLET_ID;
                        cmd.Parameters.Add("v_SENDER_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_STREET;
                        cmd.Parameters.Add("v_SENDER_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.SENDER_ADDRESS;
                        cmd.Parameters.Add("v_RECEIVER_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_NAME;
                        cmd.Parameters.Add("v_RECEIVER_EMAIL", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_EMAIL;
                        cmd.Parameters.Add("v_RECEIVER_PHONE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_PHONE;
                        cmd.Parameters.Add("v_RECEIVER_PROVINCE_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_PROVINCE_ID;
                        cmd.Parameters.Add("v_RECEIVER_DISTRICT_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_DISTRICT_ID;
                        cmd.Parameters.Add("v_RECEIVER_WARD_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_WARD_ID;
                        cmd.Parameters.Add("v_RECEIVER_HAMLET_ID", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.RECEIVER_HAMLET_ID;
                        cmd.Parameters.Add("v_RECEIVER_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_STREET;
                        cmd.Parameters.Add("v_RECEIVER_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.RECEIVER_ADDRESS;
                        cmd.Parameters.Add("v_PRODUCT_QUANTITY", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.PRODUCT_QUANTITY;
                        cmd.Parameters.Add("v_PRODUCT_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_NAME;
                        cmd.Parameters.Add("v_PRODUCT_DESCRIPTION", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_DESCRIPTION;
                        cmd.Parameters.Add("v_STATUS", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.STATUS;
                        cmd.Parameters.Add("v_TOTAL_AMOUNT", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.TOTAL_AMOUNT;
                        cmd.Parameters.Add("v_WEIGHT", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.WEIGHT;
                        cmd.Parameters.Add("v_SERVICE_TYPE", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.SERVICE_TYPE;
                        cmd.Parameters.Add("v_CHANNEL", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.CHANNEL;
                        cmd.Parameters.Add("v_PO_CREATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.PO_CREATE;
                        cmd.Parameters.Add("v_REF_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.REF_CODE;
                        cmd.Parameters.Add("v_FILE_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = CShipment.FILE_NAME;
                        cmd.Parameters.Add("v_PRODUCT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.PRODUCT_CODE;
                        cmd.Parameters.Add("v_SYSTEM_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SYSTEM_ID;
                        cmd.Parameters.Add("v_COD", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.COD;
                        cmd.Parameters.Add("v_PRODUCT_VALUE", OracleDbType.Int32, ParameterDirection.Input).Value = CShipment.PRODUCT_VALUE;
                        cmd.Parameters.Add("v_TO_COUNTRY", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.TO_COUNTRY;
                        cmd.Parameters.Add("v_TO_ZIPCODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.TO_ZIPCODE;
                        cmd.Parameters.Add("v_MASTER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.MASTER_CODE;
                        cmd.Parameters.Add("v_ADDITION_SERVICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.ADDITION_SERVICE;
                        cmd.Parameters.Add("v_SPECIAL_SERVICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = CShipment.SPECIAL_SERVICE;
                        cmd.ExecuteNonQuery();

                        //var id = Convert.ToInt32(cmd.Parameters["P_ID"].Value.ToString());

                        //if (id > 0)
                        //{
                        //    oReponseEntity.Code = "00";
                        //    oReponseEntity.Message = "Cập nhật dữ liệu thành công";
                        //    oReponseEntity.Value = listCShipment.Count.ToString();
                        //}
                        //else
                        //{
                        //    oReponseEntity.Code = "-99";
                        //    oReponseEntity.Message = "Lỗi cập nhật dữ liệu (region LIST_SHIPMENT_CREATE)";
                        //    oReponseEntity.Value = string.Empty;
                        //}
                    }
                }
                transaction.Commit();
                oReponseEntity.Code = "00";
                oReponseEntity.Message = "Cập nhật dữ liệu thành công";
                oReponseEntity.Value = listCShipment.Count.ToString();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                LogAPI.LogToFile(LogFileType.EXCEPTION, "region LIST_SHIPMENT_CREATE" + ex.Message);
                oReponseEntity.Code = "99";
                oReponseEntity.Message = "Lỗi xử lý dữ liệu";
                oReponseEntity.Value = string.Empty;
                
            }
            return oReponseEntity;
        }
        #endregion
        #endregion

        #region GET SHIPMENT
        public ReturnShipment SHIPMENT_GET(string code, string order_code, string status, string from_date, string to_date, string province_id, string customer_code, int page_size, int page_index)
        {
            ReturnShipment _returnShipment = new ReturnShipment();
            ReponseEntity oReponseEntity = new ReponseEntity();
            List<CShipment> listCShipment = null;
            CShipment oCShipment = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_GET";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_ORDER_CODE", OracleDbType.Varchar2)).Value = order_code;
                    cmd.Parameters.Add(new OracleParameter("P_REF_CODE", OracleDbType.Varchar2)).Value = code;
                    cmd.Parameters.Add(new OracleParameter("P_CUSTOMER_CODE", OracleDbType.Varchar2)).Value = customer_code;
                    cmd.Parameters.Add(new OracleParameter("P_STATUS", OracleDbType.Varchar2)).Value = status;
                    cmd.Parameters.Add(new OracleParameter("P_FROM_DATE", OracleDbType.Int32)).Value = (!String.IsNullOrEmpty(from_date)) ? Convert.ToInt32(from_date) : 0;
                    cmd.Parameters.Add(new OracleParameter("P_TO_DATE", OracleDbType.Int32)).Value = (!String.IsNullOrEmpty(to_date)) ? Convert.ToInt32(to_date) : 0;
                    cmd.Parameters.Add(new OracleParameter("P_RECEIVER_PROVINCE_ID", OracleDbType.Int32)).Value = (!String.IsNullOrEmpty(province_id)) ? Convert.ToInt32(province_id) : 0;
                    cmd.Parameters.Add(new OracleParameter("P_PAGE_INDEX", OracleDbType.Int32)).Value = page_index;
                    cmd.Parameters.Add(new OracleParameter("P_PAGE_SIZE", OracleDbType.Int32)).Value = page_size;
                    cmd.Parameters.Add("P_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        listCShipment = new List<CShipment>();
                        while (dr.Read())
                        {
                            oCShipment = new CShipment();
                            if (dr["ID"] != null)
                                oCShipment.ID = Convert.ToInt32(dr["ID"]);
                            oCShipment.CODE = dr["CODE"].ToString();
                            oCShipment.ORDER_CODE = dr["ORDER_CODE"].ToString();
                            oCShipment.TRACKING_CODE = dr["TRACKING_CODE"].ToString();

                            oCShipment.STORE_ID = Convert.ToInt32(dr["STORE_ID"].ToString());
                            oCShipment.SENDER_CODE = dr["SENDER_CODE"].ToString();
                            oCShipment.SENDER_NAME = dr["SENDER_NAME"].ToString();

                            oCShipment.SENDER_PHONE = dr["SENDER_PHONE"].ToString();
                            oCShipment.SENDER_EMAIL = dr["SENDER_EMAIL"].ToString();
                            oCShipment.SENDER_PROVINCE_ID = Convert.ToInt32(dr["SENDER_PROVINCE_ID"].ToString());
                            oCShipment.SENDER_DISTRICT_ID = Convert.ToInt32(dr["SENDER_DISTRICT_ID"].ToString());
                            oCShipment.SENDER_WARD_ID = Convert.ToInt32(dr["SENDER_WARD_ID"].ToString());
                            oCShipment.SENDER_HAMLET_ID = Convert.ToInt32(dr["SENDER_HAMLET_ID"].ToString());
                            oCShipment.SENDER_ADDRESS = dr["SENDER_ADDRESS"].ToString();

                            oCShipment.RECEIVER_NAME = dr["RECEIVER_NAME"].ToString();
                            oCShipment.RECEIVER_EMAIL = dr["RECEIVER_EMAIL"].ToString();
                            oCShipment.RECEIVER_PHONE = dr["RECEIVER_PHONE"].ToString();
                            oCShipment.RECEIVER_PROVINCE_ID = Convert.ToInt32(dr["RECEIVER_PROVINCE_ID"].ToString());
                            oCShipment.RECEIVER_DISTRICT_ID = Convert.ToInt32(dr["RECEIVER_DISTRICT_ID"].ToString());
                            oCShipment.RECEIVER_WARD_ID = Convert.ToInt32(dr["RECEIVER_WARD_ID"].ToString());

                            oCShipment.RECEIVER_HAMLET_ID = Convert.ToInt32(dr["RECEIVER_HAMLET_ID"].ToString());
                            oCShipment.RECEIVER_STREET = dr["RECEIVER_STREET"].ToString();

                            oCShipment.RECEIVER_ADDRESS = dr["RECEIVER_ADDRESS"].ToString();
                            oCShipment.PRODUCT_QUANTITY = Convert.ToInt32(dr["PRODUCT_QUANTITY"].ToString());
                            oCShipment.PRODUCT_NAME = dr["PRODUCT_NAME"].ToString();
                            oCShipment.PRODUCT_DESCRIPTION = dr["PRODUCT_DESCRIPTION"].ToString();
                            oCShipment.STATUS = dr["STATUS_CODE"].ToString();
                            oCShipment.TOTAL_AMOUNT = Convert.ToInt32(dr["TOTAL_AMOUNT"].ToString());
                            oCShipment.WEIGHT = Convert.ToInt32(dr["WEIGHT"].ToString());
                            oCShipment.SERVICE_TYPE = Convert.ToInt32(dr["SERVICE_TYPE"].ToString());
                            oCShipment.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                            oCShipment.REF_CODE = dr["REF_CODE"].ToString();
                            oCShipment.PO_CREATE = dr["PO_CREATE"].ToString();
                            oCShipment.ORDER_JOIN_DATE = dr["ORDER_JOIN"].ToString();
                            oCShipment.FILE_NAME = dr["FILE_NAME"].ToString();
                            oCShipment.SYSTEM_ID = dr["SYSTEM_ID"].ToString();
                            oCShipment.PRODUCT_CODE = dr["PRODUCT_CODE"].ToString();
                            oCShipment.CHANNEL = dr["CHANNEL"].ToString();
                            oCShipment.COD = Convert.ToInt32(dr["COD"].ToString());
                            oCShipment.COD_FEE = Convert.ToInt32(dr["COD_FEE"].ToString());
                            oCShipment.MAIN_FEE = Convert.ToInt32(dr["MAIN_FEE"].ToString());
                            oCShipment.SERVICE_FEE = Convert.ToInt32(dr["SERVICE_FEE"].ToString());
                            oCShipment.TOTAL_FEE = Convert.ToInt32(dr["TOTAL_FEE"].ToString());
                            listCShipment.Add(oCShipment);
                        }
                        _returnShipment.Code = "00";
                        _returnShipment.Message = "Lấy dữ liệu thành công.";
                        _returnShipment.Total = Convert.ToInt32(cmd.Parameters["P_TOTAL"].Value.ToString());
                        _returnShipment.ListShipment = listCShipment;
                    }
                    else
                    {
                        _returnShipment.Code = "01";
                        _returnShipment.Message = "Không tồn tại bản ghi nào.";
                        _returnShipment.Total = 0;
                        _returnShipment.ListShipment = listCShipment;
                    }
                }
            }
            catch
            {
                _returnShipment.Code = "99";
                _returnShipment.Message = "Lỗi xử lý dữ liệu";
                _returnShipment.Total = 0;
                _returnShipment.ListShipment = null;
            }
            return _returnShipment;
        }

        public ReturnShipment SHIPMENT_TMP_GET(string from_date, string to_date, string customer_code, int page_size, int page_index, string filename, string update)
        {
            ReturnShipment _returnShipment = new ReturnShipment();
            ReponseEntity oReponseEntity = new ReponseEntity();
            List<CShipment> listCShipment = null;
            CShipment oCShipment = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    if (String.IsNullOrEmpty(update))
                        cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_TMP_GET";
                    else
                        cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_TMP_GET_UPDATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_CUSTOMER_CODE", OracleDbType.Varchar2)).Value = customer_code;
                    cmd.Parameters.Add(new OracleParameter("P_FILE_NAME", OracleDbType.Varchar2)).Value = filename;
                    cmd.Parameters.Add(new OracleParameter("P_FROM_DATE", OracleDbType.Int32)).Value = (!String.IsNullOrEmpty(from_date)) ? Convert.ToInt32(from_date) : 0;
                    cmd.Parameters.Add(new OracleParameter("P_TO_DATE", OracleDbType.Int32)).Value = (!String.IsNullOrEmpty(to_date)) ? Convert.ToInt32(to_date) : 0;
                    cmd.Parameters.Add(new OracleParameter("P_PAGE_INDEX", OracleDbType.Int32)).Value = page_index;
                    cmd.Parameters.Add(new OracleParameter("P_PAGE_SIZE", OracleDbType.Int32)).Value = page_size;
                    cmd.Parameters.Add("P_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        listCShipment = new List<CShipment>();
                        while (dr.Read())
                        {
                            oCShipment = new CShipment();
                            if (dr["ID"] != null)
                                oCShipment.ID = Convert.ToInt32(dr["ID"]);
                            oCShipment.CODE = dr["CODE"].ToString();
                            oCShipment.ORDER_CODE = dr["ORDER_CODE"].ToString();
                            oCShipment.TRACKING_CODE = dr["TRACKING_CODE"].ToString();

                            oCShipment.STORE_ID = Convert.ToInt32(dr["STORE_ID"].ToString());
                            oCShipment.SENDER_CODE = dr["SENDER_CODE"].ToString();
                            oCShipment.SENDER_NAME = dr["SENDER_NAME"].ToString();

                            oCShipment.SENDER_PHONE = dr["SENDER_PHONE"].ToString();
                            oCShipment.SENDER_EMAIL = dr["SENDER_EMAIL"].ToString();
                            oCShipment.SENDER_PROVINCE_ID = Convert.ToInt32(dr["SENDER_PROVINCE_ID"].ToString());
                            oCShipment.SENDER_DISTRICT_ID = Convert.ToInt32(dr["SENDER_DISTRICT_ID"].ToString());
                            oCShipment.SENDER_WARD_ID = Convert.ToInt32(dr["SENDER_WARD_ID"].ToString());
                            oCShipment.SENDER_HAMLET_ID = Convert.ToInt32(dr["SENDER_HAMLET_ID"].ToString());
                            oCShipment.SENDER_ADDRESS = dr["SENDER_ADDRESS"].ToString();

                            oCShipment.RECEIVER_NAME = dr["RECEIVER_NAME"].ToString();
                            oCShipment.RECEIVER_EMAIL = dr["RECEIVER_EMAIL"].ToString();
                            oCShipment.RECEIVER_PHONE = dr["RECEIVER_PHONE"].ToString();
                            oCShipment.RECEIVER_PROVINCE_ID = Convert.ToInt32(dr["RECEIVER_PROVINCE_ID"].ToString());
                            oCShipment.RECEIVER_DISTRICT_ID = Convert.ToInt32(dr["RECEIVER_DISTRICT_ID"].ToString());
                            oCShipment.RECEIVER_WARD_ID = Convert.ToInt32(dr["RECEIVER_WARD_ID"].ToString());

                            oCShipment.RECEIVER_HAMLET_ID = Convert.ToInt32(dr["RECEIVER_HAMLET_ID"].ToString());
                            oCShipment.RECEIVER_STREET = dr["RECEIVER_STREET"].ToString();

                            oCShipment.RECEIVER_ADDRESS = dr["RECEIVER_ADDRESS"].ToString();
                            oCShipment.PRODUCT_QUANTITY = Convert.ToInt32(dr["PRODUCT_QUANTITY"].ToString());
                            oCShipment.PRODUCT_NAME = dr["PRODUCT_NAME"].ToString();
                            oCShipment.PRODUCT_DESCRIPTION = dr["PRODUCT_DESCRIPTION"].ToString();
                            oCShipment.STATUS = dr["STATUS_CODE"].ToString();
                            oCShipment.TOTAL_AMOUNT = Convert.ToInt32(dr["TOTAL_AMOUNT"].ToString());
                            oCShipment.WEIGHT = Convert.ToInt32(dr["WEIGHT"].ToString());
                            oCShipment.SERVICE_TYPE = Convert.ToInt32(dr["SERVICE_TYPE"].ToString());
                            oCShipment.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                            oCShipment.REF_CODE = dr["REF_CODE"].ToString();
                            oCShipment.PO_CREATE = dr["PO_CREATE"].ToString();
                            oCShipment.ORDER_JOIN_DATE = dr["ORDER_JOIN"].ToString();
                            oCShipment.CREATE_TIME = dr["CREATE_TIME"].ToString();
                            oCShipment.FILE_NAME = dr["FILE_NAME"].ToString();
                            oCShipment.SYSTEM_ID = dr["SYSTEM_ID"].ToString();
                            oCShipment.PRODUCT_CODE = dr["PRODUCT_CODE"].ToString();
                            oCShipment.COD = Convert.ToInt32(dr["COD"].ToString());
                            listCShipment.Add(oCShipment);
                        }
                        _returnShipment.Code = "00";
                        _returnShipment.Message = "Lấy dữ liệu thành công.";
                        _returnShipment.Total = Convert.ToInt32(cmd.Parameters["P_TOTAL"].Value.ToString());
                        _returnShipment.ListShipment = listCShipment;
                    }
                    else
                    {
                        _returnShipment.Code = "01";
                        _returnShipment.Message = "Không tồn tại bản ghi nào.";
                        _returnShipment.Total = 0;
                        _returnShipment.ListShipment = listCShipment;
                    }
                }
            }
            catch
            {
                _returnShipment.Code = "99";
                _returnShipment.Message = "Lỗi xử lý dữ liệu";
                _returnShipment.Total = 0;
                _returnShipment.ListShipment = null;
            }
            return _returnShipment;
        }
        public ReturnShipment_v2 SHIPMENT_GET_ONE(string code)
        {
            ReturnShipment_v2 _returnShipment = new ReturnShipment_v2();
            ReponseEntity oReponseEntity = new ReponseEntity();
            List<CShipment_v2> listCShipment = null;
            CShipment_v2 oCShipment = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_GET_ONE";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new OracleParameter("P_REF_CODE", OracleDbType.Varchar2)).Value = code.Trim().ToUpper();
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            listCShipment = new List<CShipment_v2>();
                            while (dr.Read())
                            {
                                oCShipment = new CShipment_v2();
                                if (dr["ID"] != null)
                                    oCShipment.ID = Convert.ToInt32(dr["ID"]);
                                oCShipment.CODE = dr["CODE"].ToString();
                                oCShipment.ORDER_CODE = dr["ORDER_CODE"].ToString();
                                oCShipment.TRACKING_CODE = dr["TRACKING_CODE"].ToString();

                                oCShipment.STORE_ID = Convert.ToInt32(dr["STORE_ID"].ToString());
                                oCShipment.SENDER_CODE = dr["SENDER_CODE"].ToString();
                                oCShipment.SENDER_NAME = dr["SENDER_NAME"].ToString();

                                oCShipment.SENDER_PHONE = dr["SENDER_PHONE"].ToString();
                                oCShipment.SENDER_EMAIL = dr["SENDER_EMAIL"].ToString();
                                oCShipment.SENDER_PROVINCE_ID = Convert.ToInt32(dr["SENDER_PROVINCE_ID"].ToString());
                                oCShipment.SENDER_DISTRICT_ID = Convert.ToInt32(dr["SENDER_DISTRICT_ID"].ToString());
                                oCShipment.SENDER_WARD_ID = Convert.ToInt32(dr["SENDER_WARD_ID"].ToString());
                                oCShipment.SENDER_HAMLET_ID = Convert.ToInt32(dr["SENDER_HAMLET_ID"].ToString());
                                oCShipment.SENDER_ADDRESS = dr["SENDER_ADDRESS"].ToString();

                                oCShipment.RECEIVER_NAME = dr["RECEIVER_NAME"].ToString();
                                oCShipment.RECEIVER_EMAIL = dr["RECEIVER_EMAIL"].ToString();
                                oCShipment.RECEIVER_PHONE = dr["RECEIVER_PHONE"].ToString();
                                oCShipment.RECEIVER_PROVINCE_ID = Convert.ToInt32(dr["RECEIVER_PROVINCE_ID"].ToString());
                                oCShipment.RECEIVER_DISTRICT_ID = Convert.ToInt32(dr["RECEIVER_DISTRICT_ID"].ToString());
                                oCShipment.RECEIVER_WARD_ID = Convert.ToInt32(dr["RECEIVER_WARD_ID"].ToString());

                                oCShipment.RECEIVER_HAMLET_ID = Convert.ToInt32(dr["RECEIVER_HAMLET_ID"].ToString());
                                oCShipment.RECEIVER_STREET = dr["RECEIVER_STREET"].ToString();

                                oCShipment.RECEIVER_ADDRESS = dr["RECEIVER_ADDRESS"].ToString();
                                oCShipment.PRODUCT_QUANTITY = Convert.ToInt32(dr["PRODUCT_QUANTITY"].ToString());
                                oCShipment.PRODUCT_NAME = dr["PRODUCT_NAME"].ToString();
                                oCShipment.PRODUCT_DESCRIPTION = dr["PRODUCT_DESCRIPTION"].ToString();
                                // oCShipment.STATUS = dr["STATUS_CODE"].ToString();
                                oCShipment.TOTAL_AMOUNT = Convert.ToInt32(dr["TOTAL_AMOUNT"].ToString());
                                oCShipment.WEIGHT = Convert.ToInt32(dr["WEIGHT"].ToString());
                                oCShipment.SERVICE_TYPE = Convert.ToInt32(dr["SERVICE_TYPE"].ToString());
                                oCShipment.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                                oCShipment.REF_CODE = dr["REF_CODE"].ToString();
                                oCShipment.PO_CREATE = dr["PO_CREATE"].ToString();
                                oCShipment.ORDER_JOIN_DATE = dr["ORDER_JOIN"].ToString();
                                oCShipment.FILE_NAME = dr["FILE_NAME"].ToString();
                                oCShipment.SYSTEM_ID = dr["SYSTEM_ID"].ToString();
                                oCShipment.PRODUCT_CODE = dr["PRODUCT_CODE"].ToString();
                                oCShipment.PRODUCT_VALUE = Convert.ToInt32(dr["PRODUCT_VALUE"].ToString());
                                oCShipment.ADDITION_SERVICE = dr["ADDITION_SERVICE"].ToString();

                                _returnShipment.Code = "00";
                                _returnShipment.Message = "Lấy dữ liệu thành công.";
                                _returnShipment.Shipment = oCShipment;
                            }
                        }
                        else
                        {
                            _returnShipment.Code = "01";
                            _returnShipment.Message = "Không tôn tại dữ liệu.";
                            _returnShipment.Shipment = oCShipment;
                        }
                    }
                    else
                    {
                        _returnShipment.Code = "98";
                        _returnShipment.Message = "Lỗi xử lý hệ thống.";
                        _returnShipment.Shipment = oCShipment;
                    }
                }
            }
            catch
            {
                _returnShipment.Code = "99";
                _returnShipment.Message = "Lỗi xử lý dữ liệu";
                _returnShipment.Total = 0;
                _returnShipment.Shipment = oCShipment;
            }
            return _returnShipment;
        }

        public ReturnShipment_v2 SHIPMENT_GET_ONE_BY_ID(int id)
        {
            ReturnShipment_v2 _returnShipment = new ReturnShipment_v2();
            ReponseEntity oReponseEntity = new ReponseEntity();
            List<CShipment_v2> listCShipment = null;
            CShipment_v2 oCShipment = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_GET_ONE_BY_ID";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new OracleParameter("P_ID", OracleDbType.Int32)).Value = id;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        listCShipment = new List<CShipment_v2>();
                        while (dr.Read())
                        {
                            oCShipment = new CShipment_v2();
                            if (dr["ID"] != null)
                                oCShipment.ID = Convert.ToInt32(dr["ID"]);
                            oCShipment.CODE = dr["CODE"].ToString();
                            oCShipment.ORDER_CODE = dr["ORDER_CODE"].ToString();
                            oCShipment.TRACKING_CODE = dr["TRACKING_CODE"].ToString();

                            oCShipment.STORE_ID = Convert.ToInt32(dr["STORE_ID"].ToString());
                            oCShipment.SENDER_CODE = dr["SENDER_CODE"].ToString();
                            oCShipment.SENDER_NAME = dr["SENDER_NAME"].ToString();

                            oCShipment.SENDER_PHONE = dr["SENDER_PHONE"].ToString();
                            oCShipment.SENDER_EMAIL = dr["SENDER_EMAIL"].ToString();
                            oCShipment.SENDER_PROVINCE_ID = Convert.ToInt32(dr["SENDER_PROVINCE_ID"].ToString());
                            oCShipment.SENDER_DISTRICT_ID = Convert.ToInt32(dr["SENDER_DISTRICT_ID"].ToString());
                            oCShipment.SENDER_WARD_ID = Convert.ToInt32(dr["SENDER_WARD_ID"].ToString());
                            oCShipment.SENDER_HAMLET_ID = Convert.ToInt32(dr["SENDER_HAMLET_ID"].ToString());
                            oCShipment.SENDER_ADDRESS = dr["SENDER_ADDRESS"].ToString();

                            oCShipment.RECEIVER_NAME = dr["RECEIVER_NAME"].ToString();
                            oCShipment.RECEIVER_EMAIL = dr["RECEIVER_EMAIL"].ToString();
                            oCShipment.RECEIVER_PHONE = dr["RECEIVER_PHONE"].ToString();
                            oCShipment.RECEIVER_PROVINCE_ID = Convert.ToInt32(dr["RECEIVER_PROVINCE_ID"].ToString());
                            oCShipment.RECEIVER_DISTRICT_ID = Convert.ToInt32(dr["RECEIVER_DISTRICT_ID"].ToString());
                            oCShipment.RECEIVER_WARD_ID = Convert.ToInt32(dr["RECEIVER_WARD_ID"].ToString());

                            oCShipment.RECEIVER_HAMLET_ID = Convert.ToInt32(dr["RECEIVER_HAMLET_ID"].ToString());
                            oCShipment.RECEIVER_STREET = dr["RECEIVER_STREET"].ToString();

                            oCShipment.RECEIVER_ADDRESS = dr["RECEIVER_ADDRESS"].ToString();
                            oCShipment.PRODUCT_QUANTITY = Convert.ToInt32(dr["PRODUCT_QUANTITY"].ToString());
                            oCShipment.PRODUCT_NAME = dr["PRODUCT_NAME"].ToString();
                            oCShipment.PRODUCT_DESCRIPTION = dr["PRODUCT_DESCRIPTION"].ToString();

                            oCShipment.TOTAL_AMOUNT = Convert.ToInt32(dr["TOTAL_AMOUNT"].ToString());
                            oCShipment.WEIGHT = Convert.ToInt32(dr["WEIGHT"].ToString());
                            oCShipment.SERVICE_TYPE = Convert.ToInt32(dr["SERVICE_TYPE"].ToString());
                            oCShipment.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                            oCShipment.REF_CODE = dr["REF_CODE"].ToString();
                            oCShipment.PO_CREATE = dr["PO_CREATE"].ToString();
                            oCShipment.ORDER_JOIN_DATE = dr["ORDER_JOIN"].ToString();
                            oCShipment.FILE_NAME = dr["FILE_NAME"].ToString();
                            oCShipment.SYSTEM_ID = dr["SYSTEM_ID"].ToString();
                            oCShipment.PRODUCT_CODE = dr["PRODUCT_CODE"].ToString();
                            oCShipment.PRODUCT_VALUE = Convert.ToInt32(dr["PRODUCT_VALUE"].ToString());

                        }
                        _returnShipment.Code = "00";
                        _returnShipment.Message = "Lấy dữ liệu thành công.";
                        _returnShipment.Total = 1;
                        _returnShipment.Shipment = oCShipment;
                    }
                    else
                    {
                        _returnShipment.Code = "01";
                        _returnShipment.Message = "Không tồn tại bản ghi nào.";
                        _returnShipment.Total = 0;
                        _returnShipment.Shipment = oCShipment;
                    }
                }
            }
            catch
            {
                _returnShipment.Code = "99";
                _returnShipment.Message = "Lỗi xử lý dữ liệu";
                _returnShipment.Total = 0;
                _returnShipment.Shipment = oCShipment;
            }
            return _returnShipment;
        }
        public ReturnShipment SHIPMENT_TMP_GET_ONE(int id)
        {
            ReturnShipment _returnShipment = new ReturnShipment();
            ReponseEntity oReponseEntity = new ReponseEntity();
            List<CShipment> listCShipment = null;
            CShipment oCShipment = null;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_TMP_GET_ONE";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new OracleParameter("P_ID", OracleDbType.Int32)).Value = id;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        listCShipment = new List<CShipment>();
                        while (dr.Read())
                        {
                            oCShipment = new CShipment();
                            if (dr["ID"] != null)
                                oCShipment.ID = Convert.ToInt32(dr["ID"]);
                            oCShipment.CODE = dr["CODE"].ToString();
                            oCShipment.ORDER_CODE = dr["ORDER_CODE"].ToString();
                            oCShipment.TRACKING_CODE = dr["TRACKING_CODE"].ToString();

                            oCShipment.STORE_ID = Convert.ToInt32(dr["STORE_ID"].ToString());
                            oCShipment.SENDER_CODE = dr["SENDER_CODE"].ToString();
                            oCShipment.SENDER_NAME = dr["SENDER_NAME"].ToString();

                            oCShipment.SENDER_PHONE = dr["SENDER_PHONE"].ToString();
                            oCShipment.SENDER_EMAIL = dr["SENDER_EMAIL"].ToString();
                            oCShipment.SENDER_PROVINCE_ID = Convert.ToInt32(dr["SENDER_PROVINCE_ID"].ToString());
                            oCShipment.SENDER_DISTRICT_ID = Convert.ToInt32(dr["SENDER_DISTRICT_ID"].ToString());
                            oCShipment.SENDER_WARD_ID = Convert.ToInt32(dr["SENDER_WARD_ID"].ToString());
                            oCShipment.SENDER_HAMLET_ID = Convert.ToInt32(dr["SENDER_HAMLET_ID"].ToString());
                            oCShipment.SENDER_ADDRESS = dr["SENDER_ADDRESS"].ToString();

                            oCShipment.RECEIVER_NAME = dr["RECEIVER_NAME"].ToString();
                            oCShipment.RECEIVER_EMAIL = dr["RECEIVER_EMAIL"].ToString();
                            oCShipment.RECEIVER_PHONE = dr["RECEIVER_PHONE"].ToString();
                            oCShipment.RECEIVER_PROVINCE_ID = Convert.ToInt32(dr["RECEIVER_PROVINCE_ID"].ToString());
                            oCShipment.RECEIVER_DISTRICT_ID = Convert.ToInt32(dr["RECEIVER_DISTRICT_ID"].ToString());
                            oCShipment.RECEIVER_WARD_ID = Convert.ToInt32(dr["RECEIVER_WARD_ID"].ToString());

                            oCShipment.RECEIVER_HAMLET_ID = Convert.ToInt32(dr["RECEIVER_HAMLET_ID"].ToString());
                            oCShipment.RECEIVER_STREET = dr["RECEIVER_STREET"].ToString();

                            oCShipment.RECEIVER_ADDRESS = dr["RECEIVER_ADDRESS"].ToString();
                            oCShipment.PRODUCT_QUANTITY = Convert.ToInt32(dr["PRODUCT_QUANTITY"].ToString());
                            oCShipment.PRODUCT_NAME = dr["PRODUCT_NAME"].ToString();
                            oCShipment.PRODUCT_DESCRIPTION = dr["PRODUCT_DESCRIPTION"].ToString();
                            oCShipment.STATUS = dr["STATUS_CODE"].ToString();
                            oCShipment.TOTAL_AMOUNT = Convert.ToInt32(dr["TOTAL_AMOUNT"].ToString());
                            oCShipment.WEIGHT = Convert.ToInt32(dr["WEIGHT"].ToString());
                            oCShipment.SERVICE_TYPE = Convert.ToInt32(dr["SERVICE_TYPE"].ToString());
                            oCShipment.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                            oCShipment.REF_CODE = dr["REF_CODE"].ToString();
                            oCShipment.PO_CREATE = dr["PO_CREATE"].ToString();
                            oCShipment.ORDER_JOIN_DATE = dr["ORDER_JOIN"].ToString();
                            oCShipment.FILE_NAME = dr["FILE_NAME"].ToString();
                            oCShipment.SYSTEM_ID = dr["SYSTEM_ID"].ToString();
                            oCShipment.PRODUCT_CODE = dr["PRODUCT_CODE"].ToString();
                            oCShipment.PRODUCT_VALUE = Convert.ToInt32(dr["PRODUCT_VALUE"].ToString());
                        }
                        _returnShipment.Code = "00";
                        _returnShipment.Message = "Lấy dữ liệu thành công.";
                        _returnShipment.Total = 1;
                        _returnShipment.Shipment = oCShipment;
                    }
                    else
                    {
                        _returnShipment.Code = "01";
                        _returnShipment.Message = "Không tồn tại bản ghi nào.";
                        _returnShipment.Total = 0;
                        _returnShipment.Shipment = oCShipment;
                    }
                }
            }
            catch
            {
                _returnShipment.Code = "99";
                _returnShipment.Message = "Lỗi xử lý dữ liệu";
                _returnShipment.Total = 0;
                _returnShipment.Shipment = oCShipment;
            }
            return _returnShipment;
        }
        #endregion

        #region DELETE SHIPMENT
        public ReponseEntity SHIPMENT_DELETE(int id)
        {
            ReponseEntity oReponseEntity = new ReponseEntity();
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_DELETE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("V_CHECK", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add(new OracleParameter("P_ID", OracleDbType.Int32)).Value = id;
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["V_CHECK"].Value.ToString()) > 0)
                    {
                        oReponseEntity.Code = "00";
                        oReponseEntity.Message = "Cập nhật dữ liệu thành công.";
                        oReponseEntity.Value = cmd.Parameters["V_CHECK"].Value.ToString();
                    }
                    else
                    {
                        oReponseEntity.Code = "01";
                        oReponseEntity.Message = "Không tồn tại dữ liệu.";
                        oReponseEntity.Value = null;
                    }
                }
            }
            catch
            {
                oReponseEntity.Code = "99";
                oReponseEntity.Message = "Lỗi xử lý dữ liệu";
                oReponseEntity.Value = null;
            }
            return oReponseEntity;
        }

        public ReponseEntity SHIPMENT_CANCEL(string order_code)
        {
            ReponseEntity oReponseEntity = new ReponseEntity();
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_CANCEL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("V_CHECK", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add(new OracleParameter("P_ORDER_CODE", OracleDbType.Varchar2)).Value = order_code.Trim();
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["V_CHECK"].Value.ToString()) > 0)
                    {
                        oReponseEntity.Code = "00";
                        oReponseEntity.Message = "Cập nhật dữ liệu thành công.";
                        oReponseEntity.Value = cmd.Parameters["V_CHECK"].Value.ToString();
                    }
                    else
                    {
                        oReponseEntity.Code = "01";
                        oReponseEntity.Message = "Không tồn tại dữ liệu.";
                        oReponseEntity.Value = null;
                    }
                }
            }
            catch
            {
                oReponseEntity.Code = "99";
                oReponseEntity.Message = "Lỗi xử lý dữ liệu";
                oReponseEntity.Value = null;
            }
            return oReponseEntity;
        }
        #endregion

        #region DELETE SHIPMENT_TMP
        public ReponseEntity SHIPMENT_TMP_DELETE(int id)
        {
            ReponseEntity oReponseEntity = new ReponseEntity();
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_TMP_DELETE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("V_CHECK", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add(new OracleParameter("P_ID", OracleDbType.Int32)).Value = id;
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["V_CHECK"].Value.ToString()) > 0)
                    {
                        oReponseEntity.Code = "00";
                        oReponseEntity.Message = "Cập nhật dữ liệu thành công.";
                        oReponseEntity.Value = cmd.Parameters["V_CHECK"].Value.ToString();
                    }
                    else
                    {
                        oReponseEntity.Code = "01";
                        oReponseEntity.Message = "Không tồn tại dữ liệu.";
                        oReponseEntity.Value = null;
                    }
                }
            }
            catch
            {
                oReponseEntity.Code = "99";
                oReponseEntity.Message = "Lỗi xử lý dữ liệu";
                oReponseEntity.Value = null;
            }
            return oReponseEntity;
        }
        #endregion

        #region UPDATE SHIPMENT_TMP
        public ReponseEntity SHIPMENT_TMP_UPDATE(CShipment oCShipment)
        {
            ReponseEntity oReponseEntity = new ReponseEntity();
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "SHIPMENT_PKG.SHIPMENT_TMP_UPDATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("V_COUNT", OracleDbType.Int32, ParameterDirection.ReturnValue);

                    cmd.Parameters.Add(new OracleParameter("P_ID", OracleDbType.Int32)).Value = oCShipment.ID;
                    cmd.Parameters.Add(new OracleParameter("P_RECEIVER_NAME", OracleDbType.NVarchar2)).Value = oCShipment.RECEIVER_NAME;
                    cmd.Parameters.Add(new OracleParameter("P_RECEIVER_PHONE", OracleDbType.Varchar2)).Value = oCShipment.RECEIVER_PHONE;
                    cmd.Parameters.Add(new OracleParameter("P_RECEIVER_ADDRESS", OracleDbType.NVarchar2)).Value = oCShipment.RECEIVER_ADDRESS;
                    cmd.Parameters.Add(new OracleParameter("P_RECEIVER_PROVINCE_ID", OracleDbType.Int32)).Value = oCShipment.RECEIVER_PROVINCE_ID;
                    cmd.Parameters.Add(new OracleParameter("P_RECEIVER_DISTRICT_ID", OracleDbType.Int32)).Value = oCShipment.RECEIVER_DISTRICT_ID;

                    cmd.Parameters.Add(new OracleParameter("P_PRODUCT_QUANTITY", OracleDbType.Int32)).Value = oCShipment.PRODUCT_QUANTITY;
                    cmd.Parameters.Add(new OracleParameter("P_PRODUCT_NAME", OracleDbType.NVarchar2)).Value = oCShipment.PRODUCT_NAME;
                    cmd.Parameters.Add(new OracleParameter("P_PRODUCT_DESCRIPTION", OracleDbType.NVarchar2)).Value = oCShipment.PRODUCT_DESCRIPTION;
                    cmd.Parameters.Add(new OracleParameter("P_PRODUCT_VALUE", OracleDbType.Int32)).Value = oCShipment.PRODUCT_VALUE;
                    cmd.Parameters.Add(new OracleParameter("P_WEIGHT", OracleDbType.Int32)).Value = oCShipment.WEIGHT;
                    cmd.Parameters.Add(new OracleParameter("P_TOTAL_AMOUNT", OracleDbType.Int32)).Value = oCShipment.TOTAL_AMOUNT;


                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (float.Parse(cmd.Parameters["V_COUNT"].Value.ToString()) > 0)
                    {
                        oReponseEntity.Code = "00";
                        oReponseEntity.Message = "Cập nhật dữ liệu thành công.";
                        oReponseEntity.Value = cmd.Parameters["V_COUNT"].Value.ToString();
                    }
                    else
                    {
                        oReponseEntity.Code = "01";
                        oReponseEntity.Message = "Không tồn tại dữ liệu.";
                        oReponseEntity.Value = null;
                    }
                }
            }
            catch
            {
                oReponseEntity.Code = "99";
                oReponseEntity.Message = "Lỗi xử lý dữ liệu";
                oReponseEntity.Value = null;
            }
            return oReponseEntity;
        }
        #endregion
        public Boolean TrackingCode_CHK(string _TrackingCode)
        {
            using (OracleCommand cmd = new OracleCommand())
            {
                try
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    var _sql = "SELECT COUNT(0) FROM SHIPMENT S WHERE S.AMND_STATE='A' AND S.ORDER_CODE='" + _TrackingCode + "'";
                    cmd.CommandText = string.Format(_sql);
                    cmd.CommandType = CommandType.Text;
                    if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    LogAPI.LogToFile(LogFileType.EXCEPTION, "ShipmentRepository.TrackingCode_CHK" + ex.Message);
                    return false;
                }
            }
        }
    }
}