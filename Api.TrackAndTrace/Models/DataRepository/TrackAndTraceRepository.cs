using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.TrackAndTrace.Models.DataModel;
using System.Data;

namespace Api.TrackAndTrace.Models.DataRepository
{
    public class TrackAndTraceRepository
    {
        //Phần lấy dữ liệu table cho merchant site
        public ReturnTrackAndTrace ListTrackAndTrace(string itemcode)
        {
            TrackAndTrace_VNPOST.TrackAndTrace Tracking = new TrackAndTrace_VNPOST.TrackAndTrace();
            TrackAndTrace_VNPOST.UserCredentical uc = new TrackAndTrace_VNPOST.UserCredentical();
            uc.user = "vnpost";
            uc.pass = "vn!@#post";
            Tracking.EnableDecompression = true;
            Tracking.UserCredenticalValue = uc;
            Tracking.Timeout = 60000;
            DataSet ds = new DataSet();
            ds = Tracking.TrackAndTrace_Items(itemcode);

            DataTable da = new DataTable();
            ReturnTrackAndTrace _returnTrackAndTrace = new ReturnTrackAndTrace();

            //Phần lấy ra list TBL_INFO
            List<TBL_INFO> listTBL_INFODetail = null;
            TBL_INFO oTBL_INFODetail = null;

            //Phần lấy ra list TBL_DINH_VI
            List<TBL_DINH_VI> lisTBL_DINH_VIDetail = null;
            TBL_DINH_VI oTBL_DINH_VIDetail = null;

            //Phần lấy ra list TBL_CHUYEN_THU
            //List<TBL_CHUYEN_THU> lisTBL_CHUYEN_THUDetail = null;
            //TBL_CHUYEN_THU oTBL_CHUYEN_THUDetail = null;

            //Phần lấy ra list TBL_DELIVERY
            List<TBL_DELIVERY> lisTBL_DELIVERYDetail = null;
            TBL_DELIVERY oTBL_DELIVERYDetail = null;

            //Phần lấy ra list TBL_BD10
            //List<TBL_BD10> lisTBL_BD10Detail = null;
            //TBL_BD10 oTBL_BD10Detail = null;

            ////Phần lấy ra list TBL_BD10_STATUS
            //List<TBL_BD10_STATUS> lisTBL_BD10_STATUSDetail = null;
            //TBL_BD10_STATUS oTBL_BD10_STATUSDetail = null;

            try
            {
                // Gọi vào hàm tra cứu trạng thái VNPOST để lấy dữ liệu.

                //Lấy Table TBL_INFO
                DataTable tbl_info_Table = ds.Tables["TBL_INFO"];
                DataTableReader dr = tbl_info_Table.CreateDataReader();
                if (dr.HasRows)
                {
                    listTBL_INFODetail = new List<TBL_INFO>();
                    while (dr.Read())
                    {
                        oTBL_INFODetail = new TBL_INFO();
                        oTBL_INFODetail.Nuoc_Chapnhan = dr["Nuoc_Chapnhan"].ToString();
                        oTBL_INFODetail.Nuoc_Phat = dr["Nuoc_Phat"].ToString();
                        oTBL_INFODetail.BC_GUI = dr["BC_GUI"].ToString();
                        oTBL_INFODetail.BC_PHAT = dr["BC_PHAT"].ToString();
                        oTBL_INFODetail.HO_TEN_GUI = dr["HO_TEN_GUI"].ToString();
                        oTBL_INFODetail.DIA_CHI_GUI = dr["DIA_CHI_GUI"].ToString();
                        oTBL_INFODetail.SenderMobile = dr["SenderMobile"].ToString();
                        oTBL_INFODetail.KL_QUI_DOI = dr["KL_QUI_DOI"].ToString();
                        oTBL_INFODetail.HO_TEN_NHAN = dr["HO_TEN_NHAN"].ToString();
                        oTBL_INFODetail.DIA_CHI_NHAN = dr["DIA_CHI_NHAN"].ToString();
                        oTBL_INFODetail.ReceiverMobile = dr["ReceiverMobile"].ToString();
                        oTBL_INFODetail.CMT_Nhan = dr["CMT_Nhan"].ToString();
                        oTBL_INFODetail.CMT_NgayCap_Nhan = dr["CMT_NgayCap_Nhan"].ToString();
                        oTBL_INFODetail.LO = dr["LO"].ToString();
                        oTBL_INFODetail.ID = dr["ID"].ToString();
                        oTBL_INFODetail.BC16 = dr["BC16"].ToString();
                        oTBL_INFODetail.isCOD = dr["isCOD"].ToString();
                        oTBL_INFODetail.ExecuteOrder = dr["ExecuteOrder"].ToString();
                        oTBL_INFODetail.CustomerCode = dr["CustomerCode"].ToString();
                        oTBL_INFODetail.CustomerGroupCode = dr["CustomerGroupCode"].ToString();
                        oTBL_INFODetail.ServiceCode = dr["ServiceCode"].ToString();
                        oTBL_INFODetail.DataCode = dr["DataCode"].ToString();
                        oTBL_INFODetail.Weight = dr["Weight"].ToString();
                        oTBL_INFODetail.TotalFreightVAT = dr["TotalFreightVAT"].ToString();
                        oTBL_INFODetail.SendingContent = dr["SendingContent"].ToString();
                        oTBL_INFODetail.ReceiverTel = dr["ReceiverTel"].ToString();
                        oTBL_INFODetail.VAS = dr["VAS"].ToString();
                        oTBL_INFODetail.isDomestic = dr["isDomestic"].ToString();
                        oTBL_INFODetail.isAffair = dr["isAffair"].ToString();
                        oTBL_INFODetail.OriItemCode = dr["OriItemCode"].ToString();
                        oTBL_INFODetail.AccPOS = dr["AccPOS"].ToString();
                        oTBL_INFODetail.CountryCode = dr["CountryCode"].ToString();
                        oTBL_INFODetail.MainFreight = dr["MainFreight"].ToString();
                        oTBL_INFODetail.FuelSurchargeFreight = dr["FuelSurchargeFreight"].ToString();
                        oTBL_INFODetail.FarRegionFreight = dr["FarRegionFreight"].ToString();
                        oTBL_INFODetail.SubFreight = dr["SubFreight"].ToString();
                        oTBL_INFODetail.TotalFreight = dr["TotalFreight"].ToString();
                        oTBL_INFODetail.TotalFreightdiscountvat = dr["TotalFreightdiscountvat"].ToString();
                        oTBL_INFODetail.Width = dr["Width"].ToString();
                        oTBL_INFODetail.Height = dr["Height"].ToString();
                        oTBL_INFODetail.Length = dr["Length"].ToString();
                        oTBL_INFODetail.ValueAddedServiceName = dr["ValueAddedServiceName"].ToString();
                        listTBL_INFODetail.Add(oTBL_INFODetail);

                    }
                    _returnTrackAndTrace.Code = "00";
                    _returnTrackAndTrace.Message = "Lấy dữ liệu thành công.";
                    _returnTrackAndTrace.TBL_INFO_Report = oTBL_INFODetail;
                }


                //Lấy Table TBL_DINH_VI
                DataTable tbl_dinh_vi_Table = ds.Tables["TBL_DINH_VI"];
                if (tbl_dinh_vi_Table != null)
                {
                    dr = tbl_dinh_vi_Table.CreateDataReader();
                    if (dr.HasRows)
                    {
                        lisTBL_DINH_VIDetail = new List<TBL_DINH_VI>();
                        while (dr.Read())
                        {
                            oTBL_DINH_VIDetail = new TBL_DINH_VI();
                            oTBL_DINH_VIDetail.TraceDate = dr["TraceDate"].ToString();
                            oTBL_DINH_VIDetail.Date = dr["Date"].ToString();
                            oTBL_DINH_VIDetail.TimeDetail = dr["TimeDetail"].ToString();
                            oTBL_DINH_VIDetail.StatusText = dr["StatusText"].ToString();
                            oTBL_DINH_VIDetail.VI_TRI = dr["VI_TRI"].ToString();
                            oTBL_DINH_VIDetail.POSTel = dr["POSTel"].ToString();
                            lisTBL_DINH_VIDetail.Add(oTBL_DINH_VIDetail);

                        }
                        _returnTrackAndTrace.Code = "00";
                        _returnTrackAndTrace.Message = "Lấy dữ liệu thành công.";
                        _returnTrackAndTrace.List_TBL_DINH_VI_Report = lisTBL_DINH_VIDetail;

                    }
                }



                //Lấy Table TBL_CHUYEN_THU
                //DataTable tbl_chuyen_thu_Table = ds.Tables["TBL_CHUYEN_THU"];
                //if (tbl_chuyen_thu_Table != null)
                //{
                //    dr = tbl_chuyen_thu_Table.CreateDataReader();
                //    if (dr.HasRows)
                //    {
                //        lisTBL_CHUYEN_THUDetail = new List<TBL_CHUYEN_THU>();
                //        while (dr.Read())
                //        {
                //            oTBL_CHUYEN_THUDetail = new TBL_CHUYEN_THU();
                //            oTBL_CHUYEN_THUDetail.DATE = dr["DATE"].ToString();
                //            oTBL_CHUYEN_THUDetail.TIMEDETAIL = dr["TIMEDETAIL"].ToString();
                //            oTBL_CHUYEN_THUDetail.BCG = dr["BCG"].ToString();
                //            oTBL_CHUYEN_THUDetail.BCN = dr["BCN"].ToString();
                //            oTBL_CHUYEN_THUDetail.CHUYEN_THU = dr["CHUYEN_THU"].ToString();
                //            lisTBL_CHUYEN_THUDetail.Add(oTBL_CHUYEN_THUDetail);

                //        }
                //        _returnTrackAndTrace.Code = "00";
                //        _returnTrackAndTrace.Message = "Lấy dữ liệu thành công.";
                //        _returnTrackAndTrace.List_TBL_CHUYEN_THU_Report = lisTBL_CHUYEN_THUDetail;
                //    }
                //}



                //Lấy Table TBL_DELIVERY
                DataTable tbl_delivery_Table = ds.Tables["TBL_DELIVERY"];
                if (tbl_delivery_Table != null)
                {
                    dr = tbl_delivery_Table.CreateDataReader();
                    if (dr.HasRows)
                    {
                        lisTBL_DELIVERYDetail = new List<TBL_DELIVERY>();
                        while (dr.Read())
                        {
                            oTBL_DELIVERYDetail = new TBL_DELIVERY();
                            oTBL_DELIVERYDetail.DATE = dr["DATE"].ToString();
                            oTBL_DELIVERYDetail.TIMEDETAIL = dr["TIMEDETAIL"].ToString();
                            oTBL_DELIVERYDetail.NGAY_PHAT = dr["NGAY_PHAT"].ToString();
                            oTBL_DELIVERYDetail.NGAY_NHAP = dr["NGAY_NHAP"].ToString();
                            oTBL_DELIVERYDetail.NGAY_TRUYEN = dr["NGAY_TRUYEN"].ToString();
                            oTBL_DELIVERYDetail.NGAY_CN = dr["NGAY_CN"].ToString();
                            oTBL_DELIVERYDetail.VI_TRI = dr["VI_TRI"].ToString();
                            oTBL_DELIVERYDetail.STATUSTEXT = dr["STATUSTEXT"].ToString();
                            oTBL_DELIVERYDetail.IsDeliverable = dr["IsDeliverable"].ToString();
                            oTBL_DELIVERYDetail.CauseCode = dr["CauseCode"].ToString();
                            lisTBL_DELIVERYDetail.Add(oTBL_DELIVERYDetail);

                        }
                        _returnTrackAndTrace.Code = "00";
                        _returnTrackAndTrace.Message = "Lấy dữ liệu thành công. Đủ các bảng";
                        _returnTrackAndTrace.TBL_INFO_Report = oTBL_INFODetail;
                        _returnTrackAndTrace.List_TBL_DINH_VI_Report = lisTBL_DINH_VIDetail;
                        _returnTrackAndTrace.List_TBL_DELIVERY_Report = lisTBL_DELIVERYDetail;

                    }
                }




                //Lấy Table TBL_BD10
                //DataTable tbl_bd10_Table = ds.Tables["TBL_BD10"];
                //if (tbl_bd10_Table != null)
                //{
                //    dr = tbl_bd10_Table.CreateDataReader();
                //    if (dr.HasRows)
                //    {
                //        lisTBL_BD10Detail = new List<TBL_BD10>();
                //        while (dr.Read())
                //        {
                //            oTBL_BD10Detail = new TBL_BD10();
                //            oTBL_BD10Detail.DATE = dr["DATE"].ToString();
                //            oTBL_BD10Detail.TIMEDETAIL = dr["TIMEDETAIL"].ToString();
                //            oTBL_BD10Detail.BCG = dr["BCG"].ToString();
                //            oTBL_BD10Detail.BCN = dr["BCN"].ToString();
                //            oTBL_BD10Detail.BC37_INFO = dr["BC37_INFO"].ToString();
                //            oTBL_BD10Detail.SendingTime = dr["SendingTime"].ToString();
                //            oTBL_BD10Detail.BC37Date = dr["BC37Date"].ToString();
                //            lisTBL_BD10Detail.Add(oTBL_BD10Detail);

                //        }
                //        _returnTrackAndTrace.Code = "00";
                //        _returnTrackAndTrace.Message = "Lấy dữ liệu thành công.";
                //        _returnTrackAndTrace.List_TBL_BD10_Report = lisTBL_BD10Detail;
                //    }
                //}



                //Lấy Table TBL_BD10_STATUS
                //DataTable tbl_bd10_status_Table = ds.Tables["TBL_BD10_STATUS"];
                //if (tbl_bd10_status_Table != null)
                //{
                //    dr = tbl_bd10_status_Table.CreateDataReader();
                //    if (dr.HasRows)
                //    {
                //        lisTBL_BD10_STATUSDetail = new List<TBL_BD10_STATUS>();
                //        while (dr.Read())
                //        {
                //            oTBL_BD10_STATUSDetail = new TBL_BD10_STATUS();
                //            oTBL_BD10_STATUSDetail.DATE = dr["DATE"].ToString();
                //            oTBL_BD10_STATUSDetail.TIMEDETAIL1 = dr["TIMEDETAIL1"].ToString();
                //            oTBL_BD10_STATUSDetail.TIMEDETAIL = dr["TIMEDETAIL"].ToString();
                //            oTBL_BD10_STATUSDetail.STATUSTEXT = dr["STATUSTEXT"].ToString();
                //            oTBL_BD10_STATUSDetail.VI_TRI = dr["VI_TRI"].ToString();
                //            oTBL_BD10_STATUSDetail.ConfirmDate = dr["ConfirmDate"].ToString();
                //            oTBL_BD10_STATUSDetail.BC37Date = dr["BC37Date"].ToString();
                //            lisTBL_BD10_STATUSDetail.Add(oTBL_BD10_STATUSDetail);

                //        }
                //        _returnTrackAndTrace.Code = "00";
                //        _returnTrackAndTrace.Message = "Lấy dữ liệu thành công. Đủ các bảng";
                //        //_returnTrackAndTrace.List_TBL_INFO_Report = listTBL_INFODetail;

                //        _returnTrackAndTrace.TBL_INFO_Report = oTBL_INFODetail;
                //        _returnTrackAndTrace.List_TBL_DINH_VI_Report = lisTBL_DINH_VIDetail;
                //        _returnTrackAndTrace.List_TBL_CHUYEN_THU_Report = lisTBL_CHUYEN_THUDetail;
                //        _returnTrackAndTrace.List_TBL_DELIVERY_Report = lisTBL_DELIVERYDetail;
                //        _returnTrackAndTrace.List_TBL_BD10_Report = lisTBL_BD10Detail;
                //        _returnTrackAndTrace.List_TBL_BD10_STATUS_Report = lisTBL_BD10_STATUSDetail;
                //    }
                //}



                else
                {
                    _returnTrackAndTrace.Code = "01";
                    _returnTrackAndTrace.Message = "Không có dữ liệu";
                    _returnTrackAndTrace.TBL_INFO_Report = null;
                    _returnTrackAndTrace.List_TBL_DINH_VI_Report = null;
                    _returnTrackAndTrace.List_TBL_DELIVERY_Report = null;
                }



            }
            catch (Exception ex)
            {
                _returnTrackAndTrace.Code = "99";
                _returnTrackAndTrace.Message = "Lỗi xử lý dữ liệu";
                _returnTrackAndTrace.TBL_INFO_Report = null;
                _returnTrackAndTrace.List_TBL_DINH_VI_Report = null;
                _returnTrackAndTrace.List_TBL_DELIVERY_Report = null;
            }
            return _returnTrackAndTrace;



        }

        //Phần lấy dữ liệu table cho Web report
        public ReturnTrackAndTrace_WebReport ListTrackAndTrace_WebReport(string itemcode)
        {
            TrackAndTrace_VNPOST.TrackAndTrace Tracking = new TrackAndTrace_VNPOST.TrackAndTrace();
            TrackAndTrace_VNPOST.UserCredentical uc = new TrackAndTrace_VNPOST.UserCredentical();
            uc.user = "vnpost";
            uc.pass = "vn!@#post";
            Tracking.EnableDecompression = true;
            Tracking.UserCredenticalValue = uc;
            Tracking.Timeout = 60000;
            DataSet ds = new DataSet();
            ds = Tracking.TrackAndTrace_Items(itemcode);

            DataTable da = new DataTable();
            ReturnTrackAndTrace_WebReport _returnTrackAndTrace = new ReturnTrackAndTrace_WebReport();

            //Phần lấy ra list TBL_INFO
            List<TBL_INFO> listTBL_INFODetail = null;
            TBL_INFO oTBL_INFODetail = null;

            //Phần lấy ra list TBL_DINH_VI
            List<TBL_DINH_VI> lisTBL_DINH_VIDetail = null;
            TBL_DINH_VI oTBL_DINH_VIDetail = null;

            //Phần lấy ra list TBL_CHUYEN_THU
            List<TBL_CHUYEN_THU> lisTBL_CHUYEN_THUDetail = null;
            TBL_CHUYEN_THU oTBL_CHUYEN_THUDetail = null;

            //Phần lấy ra list TBL_DELIVERY
            List<TBL_DELIVERY> lisTBL_DELIVERYDetail = null;
            TBL_DELIVERY oTBL_DELIVERYDetail = null;

            //Phần lấy ra list TBL_BD10
            List<TBL_BD10> lisTBL_BD10Detail = null;
            TBL_BD10 oTBL_BD10Detail = null;

            //Phần lấy ra list TBL_BD10_STATUS
            List<TBL_BD10_STATUS> lisTBL_BD10_STATUSDetail = null;
            TBL_BD10_STATUS oTBL_BD10_STATUSDetail = null;

            try
            {
                // Gọi vào hàm tra cứu trạng thái VNPOST để lấy dữ liệu.

                //Lấy Table TBL_INFO
                DataTable tbl_info_Table = ds.Tables["TBL_INFO"];
                DataTableReader dr = tbl_info_Table.CreateDataReader();
                if (dr.HasRows)
                {
                    listTBL_INFODetail = new List<TBL_INFO>();
                    while (dr.Read())
                    {
                        oTBL_INFODetail = new TBL_INFO();
                        oTBL_INFODetail.Nuoc_Chapnhan = dr["Nuoc_Chapnhan"].ToString();
                        oTBL_INFODetail.Nuoc_Phat = dr["Nuoc_Phat"].ToString();
                        oTBL_INFODetail.BC_GUI = dr["BC_GUI"].ToString();
                        oTBL_INFODetail.BC_PHAT = dr["BC_PHAT"].ToString();
                        oTBL_INFODetail.HO_TEN_GUI = dr["HO_TEN_GUI"].ToString();
                        oTBL_INFODetail.DIA_CHI_GUI = dr["DIA_CHI_GUI"].ToString();
                        oTBL_INFODetail.SenderMobile = dr["SenderMobile"].ToString();
                        oTBL_INFODetail.KL_QUI_DOI = dr["KL_QUI_DOI"].ToString();
                        oTBL_INFODetail.HO_TEN_NHAN = dr["HO_TEN_NHAN"].ToString();
                        oTBL_INFODetail.DIA_CHI_NHAN = dr["DIA_CHI_NHAN"].ToString();
                        oTBL_INFODetail.ReceiverMobile = dr["ReceiverMobile"].ToString();
                        oTBL_INFODetail.CMT_Nhan = dr["CMT_Nhan"].ToString();
                        oTBL_INFODetail.CMT_NgayCap_Nhan = dr["CMT_NgayCap_Nhan"].ToString();
                        oTBL_INFODetail.LO = dr["LO"].ToString();
                        oTBL_INFODetail.ID = dr["ID"].ToString();
                        oTBL_INFODetail.BC16 = dr["BC16"].ToString();
                        oTBL_INFODetail.isCOD = dr["isCOD"].ToString();
                        oTBL_INFODetail.ExecuteOrder = dr["ExecuteOrder"].ToString();
                        oTBL_INFODetail.CustomerCode = dr["CustomerCode"].ToString();
                        oTBL_INFODetail.CustomerGroupCode = dr["CustomerGroupCode"].ToString();
                        oTBL_INFODetail.ServiceCode = dr["ServiceCode"].ToString();
                        oTBL_INFODetail.DataCode = dr["DataCode"].ToString();
                        oTBL_INFODetail.Weight = dr["Weight"].ToString();
                        oTBL_INFODetail.TotalFreightVAT = dr["TotalFreightVAT"].ToString();
                        oTBL_INFODetail.SendingContent = dr["SendingContent"].ToString();
                        oTBL_INFODetail.ReceiverTel = dr["ReceiverTel"].ToString();
                        oTBL_INFODetail.VAS = dr["VAS"].ToString();
                        oTBL_INFODetail.isDomestic = dr["isDomestic"].ToString();
                        oTBL_INFODetail.isAffair = dr["isAffair"].ToString();
                        oTBL_INFODetail.OriItemCode = dr["OriItemCode"].ToString();
                        oTBL_INFODetail.AccPOS = dr["AccPOS"].ToString();
                        oTBL_INFODetail.CountryCode = dr["CountryCode"].ToString();
                        oTBL_INFODetail.MainFreight = dr["MainFreight"].ToString();
                        oTBL_INFODetail.FuelSurchargeFreight = dr["FuelSurchargeFreight"].ToString();
                        oTBL_INFODetail.FarRegionFreight = dr["FarRegionFreight"].ToString();
                        oTBL_INFODetail.SubFreight = dr["SubFreight"].ToString();
                        oTBL_INFODetail.TotalFreight = dr["TotalFreight"].ToString();
                        oTBL_INFODetail.TotalFreightdiscountvat = dr["TotalFreightdiscountvat"].ToString();
                        oTBL_INFODetail.Width = dr["Width"].ToString();
                        oTBL_INFODetail.Height = dr["Height"].ToString();
                        oTBL_INFODetail.Length = dr["Length"].ToString();
                        oTBL_INFODetail.ValueAddedServiceName = dr["ValueAddedServiceName"].ToString();
                        listTBL_INFODetail.Add(oTBL_INFODetail);

                    }
                    _returnTrackAndTrace.Code = "00";
                    _returnTrackAndTrace.Message = "Lấy dữ liệu thành công.";
                    _returnTrackAndTrace.List_TBL_INFO_Report = listTBL_INFODetail;
                }


                //Lấy Table TBL_DINH_VI
                DataTable tbl_dinh_vi_Table = ds.Tables["TBL_DINH_VI"];
                if (tbl_dinh_vi_Table != null)
                {
                    dr = tbl_dinh_vi_Table.CreateDataReader();
                    if (dr.HasRows)
                    {
                        lisTBL_DINH_VIDetail = new List<TBL_DINH_VI>();
                        while (dr.Read())
                        {
                            oTBL_DINH_VIDetail = new TBL_DINH_VI();
                            oTBL_DINH_VIDetail.TraceDate = dr["TraceDate"].ToString();
                            oTBL_DINH_VIDetail.Date = dr["Date"].ToString();
                            oTBL_DINH_VIDetail.TimeDetail = dr["TimeDetail"].ToString();
                            oTBL_DINH_VIDetail.StatusText = dr["StatusText"].ToString();
                            oTBL_DINH_VIDetail.VI_TRI = dr["VI_TRI"].ToString();
                            oTBL_DINH_VIDetail.POSTel = dr["POSTel"].ToString();
                            lisTBL_DINH_VIDetail.Add(oTBL_DINH_VIDetail);

                        }
                        _returnTrackAndTrace.Code = "00";
                        _returnTrackAndTrace.Message = "Lấy dữ liệu thành công.";
                        _returnTrackAndTrace.List_TBL_DINH_VI_Report = lisTBL_DINH_VIDetail;

                    }
                }



                //Lấy Table TBL_CHUYEN_THU
                DataTable tbl_chuyen_thu_Table = ds.Tables["TBL_CHUYEN_THU"];
                if (tbl_chuyen_thu_Table != null)
                {
                    dr = tbl_chuyen_thu_Table.CreateDataReader();
                    if (dr.HasRows)
                    {
                        lisTBL_CHUYEN_THUDetail = new List<TBL_CHUYEN_THU>();
                        while (dr.Read())
                        {
                            oTBL_CHUYEN_THUDetail = new TBL_CHUYEN_THU();
                            oTBL_CHUYEN_THUDetail.DATE = dr["DATE"].ToString();
                            oTBL_CHUYEN_THUDetail.TIMEDETAIL = dr["TIMEDETAIL"].ToString();
                            oTBL_CHUYEN_THUDetail.BCG = dr["BCG"].ToString();
                            oTBL_CHUYEN_THUDetail.BCN = dr["BCN"].ToString();
                            oTBL_CHUYEN_THUDetail.CHUYEN_THU = dr["CHUYEN_THU"].ToString();
                            lisTBL_CHUYEN_THUDetail.Add(oTBL_CHUYEN_THUDetail);

                        }
                        _returnTrackAndTrace.Code = "00";
                        _returnTrackAndTrace.Message = "Lấy dữ liệu thành công.";
                        _returnTrackAndTrace.List_TBL_CHUYEN_THU_Report = lisTBL_CHUYEN_THUDetail;
                    }
                }



                //Lấy Table TBL_DELIVERY
                DataTable tbl_delivery_Table = ds.Tables["TBL_DELIVERY"];
                if (tbl_delivery_Table != null)
                {
                    dr = tbl_delivery_Table.CreateDataReader();
                    if (dr.HasRows)
                    {
                        lisTBL_DELIVERYDetail = new List<TBL_DELIVERY>();
                        while (dr.Read())
                        {
                            oTBL_DELIVERYDetail = new TBL_DELIVERY();
                            oTBL_DELIVERYDetail.DATE = dr["DATE"].ToString();
                            oTBL_DELIVERYDetail.TIMEDETAIL = dr["TIMEDETAIL"].ToString();
                            oTBL_DELIVERYDetail.NGAY_PHAT = dr["NGAY_PHAT"].ToString();
                            oTBL_DELIVERYDetail.NGAY_NHAP = dr["NGAY_NHAP"].ToString();
                            oTBL_DELIVERYDetail.NGAY_TRUYEN = dr["NGAY_TRUYEN"].ToString();
                            oTBL_DELIVERYDetail.NGAY_CN = dr["NGAY_CN"].ToString();
                            oTBL_DELIVERYDetail.VI_TRI = dr["VI_TRI"].ToString();
                            oTBL_DELIVERYDetail.STATUSTEXT = dr["STATUSTEXT"].ToString();
                            oTBL_DELIVERYDetail.IsDeliverable = dr["IsDeliverable"].ToString();
                            oTBL_DELIVERYDetail.CauseCode = dr["CauseCode"].ToString();
                            lisTBL_DELIVERYDetail.Add(oTBL_DELIVERYDetail);

                        }
                        _returnTrackAndTrace.Code = "00";
                        _returnTrackAndTrace.Message = "Lấy dữ liệu thành công.";
                        _returnTrackAndTrace.List_TBL_DELIVERY_Report = lisTBL_DELIVERYDetail;
                    }
                }




                //Lấy Table TBL_BD10
                DataTable tbl_bd10_Table = ds.Tables["TBL_BD10"];
                if (tbl_bd10_Table != null)
                {
                    dr = tbl_bd10_Table.CreateDataReader();
                    if (dr.HasRows)
                    {
                        lisTBL_BD10Detail = new List<TBL_BD10>();
                        while (dr.Read())
                        {
                            oTBL_BD10Detail = new TBL_BD10();
                            oTBL_BD10Detail.DATE = dr["DATE"].ToString();
                            oTBL_BD10Detail.TIMEDETAIL = dr["TIMEDETAIL"].ToString();
                            oTBL_BD10Detail.BCG = dr["BCG"].ToString();
                            oTBL_BD10Detail.BCN = dr["BCN"].ToString();
                            oTBL_BD10Detail.BC37_INFO = dr["BC37_INFO"].ToString();
                            oTBL_BD10Detail.SendingTime = dr["SendingTime"].ToString();
                            oTBL_BD10Detail.BC37Date = dr["BC37Date"].ToString();
                            lisTBL_BD10Detail.Add(oTBL_BD10Detail);

                        }
                        _returnTrackAndTrace.Code = "00";
                        _returnTrackAndTrace.Message = "Lấy dữ liệu thành công.";
                        _returnTrackAndTrace.List_TBL_BD10_Report = lisTBL_BD10Detail;
                    }
                }



                //Lấy Table TBL_BD10_STATUS
                DataTable tbl_bd10_status_Table = ds.Tables["TBL_BD10_STATUS"];
                if (tbl_bd10_status_Table != null)
                {
                    dr = tbl_bd10_status_Table.CreateDataReader();
                    if (dr.HasRows)
                    {
                        lisTBL_BD10_STATUSDetail = new List<TBL_BD10_STATUS>();
                        while (dr.Read())
                        {
                            oTBL_BD10_STATUSDetail = new TBL_BD10_STATUS();
                            oTBL_BD10_STATUSDetail.DATE = dr["DATE"].ToString();
                            oTBL_BD10_STATUSDetail.TIMEDETAIL1 = dr["TIMEDETAIL1"].ToString();
                            oTBL_BD10_STATUSDetail.TIMEDETAIL = dr["TIMEDETAIL"].ToString();
                            oTBL_BD10_STATUSDetail.STATUSTEXT = dr["STATUSTEXT"].ToString();
                            oTBL_BD10_STATUSDetail.VI_TRI = dr["VI_TRI"].ToString();
                            oTBL_BD10_STATUSDetail.ConfirmDate = dr["ConfirmDate"].ToString();
                            oTBL_BD10_STATUSDetail.BC37Date = dr["BC37Date"].ToString();
                            lisTBL_BD10_STATUSDetail.Add(oTBL_BD10_STATUSDetail);

                        }
                        _returnTrackAndTrace.Code = "00";
                        _returnTrackAndTrace.Message = "Lấy dữ liệu thành công. Đủ các bảng";
                        _returnTrackAndTrace.List_TBL_INFO_Report = listTBL_INFODetail;
                        _returnTrackAndTrace.List_TBL_DINH_VI_Report = lisTBL_DINH_VIDetail;
                        _returnTrackAndTrace.List_TBL_CHUYEN_THU_Report = lisTBL_CHUYEN_THUDetail;
                        _returnTrackAndTrace.List_TBL_DELIVERY_Report = lisTBL_DELIVERYDetail;
                        _returnTrackAndTrace.List_TBL_BD10_Report = lisTBL_BD10Detail;
                        _returnTrackAndTrace.List_TBL_BD10_STATUS_Report = lisTBL_BD10_STATUSDetail;
                    }
                }



                else
                {
                    _returnTrackAndTrace.Code = "01";
                    _returnTrackAndTrace.Message = "Lấy dữ liệu thành công. Thiếu 1 số bảng";
                    _returnTrackAndTrace.List_TBL_INFO_Report = listTBL_INFODetail;
                    _returnTrackAndTrace.List_TBL_DINH_VI_Report = lisTBL_DINH_VIDetail;
                    _returnTrackAndTrace.List_TBL_CHUYEN_THU_Report = lisTBL_CHUYEN_THUDetail;
                    _returnTrackAndTrace.List_TBL_DELIVERY_Report = lisTBL_DELIVERYDetail;
                    _returnTrackAndTrace.List_TBL_BD10_Report = lisTBL_BD10Detail;
                    _returnTrackAndTrace.List_TBL_BD10_STATUS_Report = lisTBL_BD10_STATUSDetail;
                }



            }
            catch (Exception ex)
            {
                _returnTrackAndTrace.Code = "99";
                _returnTrackAndTrace.Message = "Lỗi xử lý dữ liệu";
                _returnTrackAndTrace.List_TBL_INFO_Report = null;
                _returnTrackAndTrace.List_TBL_DINH_VI_Report = null;
                _returnTrackAndTrace.List_TBL_CHUYEN_THU_Report = null;
                _returnTrackAndTrace.List_TBL_DELIVERY_Report = null;
                _returnTrackAndTrace.List_TBL_BD10_Report = null;
                _returnTrackAndTrace.List_TBL_BD10_STATUS_Report = null;
            }
            return _returnTrackAndTrace;



        }
    }
}