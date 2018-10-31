using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using Api.Core.Models;
namespace Api.Core.Data
{
    public class Gss
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Ems_Enterprise_ConnectionString"].ToString();

        public E1_GSS_Chi_Tiet Lay(string Ma_E1)
        {
            // Tạo đối tượng connection và command
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("E1_GSS_Lay", myConnection);

            // Sử dụng Store Procedure
            myCommand.CommandType = CommandType.StoredProcedure;
            // Thêm các Parameters vào thủ tục            
            SqlParameter pMa_E1 = new SqlParameter("@Ma_E1", SqlDbType.VarChar, 13);
            pMa_E1.Value = Ma_E1;
            myCommand.Parameters.Add(pMa_E1);

            SqlParameter pNgay_Gui = new SqlParameter("@Ngay_Gui", SqlDbType.Int, 4);
            pNgay_Gui.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pNgay_Gui);
            SqlParameter pNgay_Phat_Hanh = new SqlParameter("@Ngay_Phat_Hanh", SqlDbType.Int, 4);
            pNgay_Phat_Hanh.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pNgay_Phat_Hanh);
            SqlParameter pGio_Phat_Hanh = new SqlParameter("@Gio_Phat_Hanh", SqlDbType.Int, 4);
            pGio_Phat_Hanh.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pGio_Phat_Hanh);
            SqlParameter pDot_Giao = new SqlParameter("@Dot_Giao", SqlDbType.Int, 4);
            pDot_Giao.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pDot_Giao);
            SqlParameter pKhoi_Luong = new SqlParameter("@Khoi_Luong", SqlDbType.Int, 4);
            pKhoi_Luong.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pKhoi_Luong);
            SqlParameter pGia_Tri_Hang = new SqlParameter("@Gia_Tri_Hang", SqlDbType.Int, 4);
            pGia_Tri_Hang.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pGia_Tri_Hang);
            SqlParameter pCuoc_DV = new SqlParameter("@Cuoc_DV", SqlDbType.Int, 4);
            pCuoc_DV.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pCuoc_DV);
            SqlParameter pCuoc_Chinh = new SqlParameter("@Cuoc_Chinh", SqlDbType.Int, 4);
            pCuoc_Chinh.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pCuoc_Chinh);
            SqlParameter pCuoc_Cod = new SqlParameter("@Cuoc_Cod", SqlDbType.Int, 4);
            pCuoc_Cod.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pCuoc_Cod);
            SqlParameter pCuoc_E1 = new SqlParameter("@Cuoc_E1", SqlDbType.Int, 4);
            pCuoc_E1.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pCuoc_E1);
            SqlParameter pTong_Tien = new SqlParameter("@Tong_Tien", SqlDbType.Int, 4);
            pTong_Tien.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pTong_Tien);
            SqlParameter pNguoi_Gui = new SqlParameter("@Nguoi_Gui", SqlDbType.NVarChar, 100);
            pNguoi_Gui.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pNguoi_Gui);
            SqlParameter pDien_Thoai_Gui = new SqlParameter("@Dien_Thoai_Gui", SqlDbType.VarChar, 100);
            pDien_Thoai_Gui.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pDien_Thoai_Gui);
            SqlParameter pDia_Chi_Gui = new SqlParameter("@Dia_Chi_Gui", SqlDbType.NVarChar, 200);
            pDia_Chi_Gui.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pDia_Chi_Gui);

            SqlParameter pNguoi_Nhan = new SqlParameter("@Nguoi_Nhan", SqlDbType.NVarChar, 100);
            pNguoi_Nhan.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pNguoi_Nhan);

            SqlParameter pTinh_Thanh = new SqlParameter("@Tinh_Thanh", SqlDbType.NVarChar, 100);
            pTinh_Thanh.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pTinh_Thanh);

            SqlParameter pDien_Thoai_Nhan = new SqlParameter("@Dien_Thoai_Nhan", SqlDbType.VarChar, 100);
            pDien_Thoai_Nhan.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pDien_Thoai_Nhan);
            SqlParameter pDia_Chi_Nhan = new SqlParameter("@Dia_Chi_Nhan", SqlDbType.NVarChar, 200);
            pDia_Chi_Nhan.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pDia_Chi_Nhan);
            SqlParameter pTrang_Thai = new SqlParameter("@Trang_Thai", SqlDbType.VarChar, 10);
            pTrang_Thai.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pTrang_Thai);
            SqlParameter pGhi_Chu = new SqlParameter("@Ghi_Chu", SqlDbType.NVarChar, 200);
            pGhi_Chu.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pGhi_Chu);
            SqlParameter pChot_SL = new SqlParameter("@Chot_SL", SqlDbType.Int, 4);
            pChot_SL.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pChot_SL);
            SqlParameter pNgay_Nhan_DL = new SqlParameter("@Ngay_Nhan_DL", SqlDbType.Int, 4);
            pNgay_Nhan_DL.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pNgay_Nhan_DL);
            SqlParameter pGio_Nhan_DL = new SqlParameter("@Gio_Nhan_DL", SqlDbType.Int, 4);
            pGio_Nhan_DL.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pGio_Nhan_DL);
            SqlParameter pMa_Bc_Goc = new SqlParameter("@Ma_Bc_Goc", SqlDbType.Int, 4);
            pMa_Bc_Goc.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pMa_Bc_Goc);
            SqlParameter pMa_Bc_Tra = new SqlParameter("@Ma_Bc_Tra", SqlDbType.Int, 4);
            pMa_Bc_Tra.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pMa_Bc_Tra);
            SqlParameter pNuoc_Tra = new SqlParameter("@Nuoc_Tra", SqlDbType.VarChar, 2);
            pNuoc_Tra.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pNuoc_Tra);
            SqlParameter pPhan_Loai = new SqlParameter("@Phan_Loai", SqlDbType.VarChar, 1);
            pPhan_Loai.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pPhan_Loai);
            SqlParameter pCong_Ty = new SqlParameter("@Cong_Ty", SqlDbType.NVarChar, 200);
            pCong_Ty.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pCong_Ty);
            SqlParameter pMST = new SqlParameter("@MST", SqlDbType.VarChar, 20);
            pMST.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pMST);
            SqlParameter pNgay_He_Thong = new SqlParameter("@Ngay_He_Thong", SqlDbType.DateTime);
            pNgay_He_Thong.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(pNgay_He_Thong);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            E1_GSS_Chi_Tiet myE1_GSS_Chi_Tiet = new E1_GSS_Chi_Tiet();
            myE1_GSS_Chi_Tiet.Ma_E1 = Ma_E1;
            myE1_GSS_Chi_Tiet.Ngay_Gui = (int)pNgay_Gui.Value;
            myE1_GSS_Chi_Tiet.Ngay_Phat_Hanh = (int)pNgay_Phat_Hanh.Value;
            myE1_GSS_Chi_Tiet.Gio_Phat_Hanh = (int)pGio_Phat_Hanh.Value;
            myE1_GSS_Chi_Tiet.Dot_Giao = (int)pDot_Giao.Value;
            myE1_GSS_Chi_Tiet.Khoi_Luong = (int)pKhoi_Luong.Value;
            myE1_GSS_Chi_Tiet.Gia_Tri_Hang = (int)pGia_Tri_Hang.Value;
            myE1_GSS_Chi_Tiet.Cuoc_DV = (int)pCuoc_DV.Value;
            myE1_GSS_Chi_Tiet.Cuoc_Chinh = (int)pCuoc_Chinh.Value;
            myE1_GSS_Chi_Tiet.Cuoc_Cod = (int)pCuoc_Cod.Value;
            myE1_GSS_Chi_Tiet.Cuoc_E1 = (int)pCuoc_E1.Value;
            myE1_GSS_Chi_Tiet.Tong_Tien = (int)pTong_Tien.Value;
            myE1_GSS_Chi_Tiet.Nguoi_Gui = (string)pNguoi_Gui.Value;
            myE1_GSS_Chi_Tiet.Dien_Thoai_Gui = (string)pDien_Thoai_Gui.Value;
            myE1_GSS_Chi_Tiet.Dia_Chi_Gui = (string)pDia_Chi_Gui.Value;
            myE1_GSS_Chi_Tiet.Nguoi_Nhan = (string)pNguoi_Nhan.Value;
            myE1_GSS_Chi_Tiet.Dien_Thoai_Nhan = (string)pDien_Thoai_Nhan.Value;
            myE1_GSS_Chi_Tiet.Dia_Chi_Nhan = (string)pDia_Chi_Nhan.Value;
            myE1_GSS_Chi_Tiet.Trang_Thai = (string)pTrang_Thai.Value;
            myE1_GSS_Chi_Tiet.Ghi_Chu = (string)pGhi_Chu.Value;
            myE1_GSS_Chi_Tiet.Chot_SL = (int)pChot_SL.Value;
            myE1_GSS_Chi_Tiet.Ngay_Nhan_DL = (int)pNgay_Nhan_DL.Value;
            myE1_GSS_Chi_Tiet.Gio_Nhan_DL = (int)pGio_Nhan_DL.Value;
            myE1_GSS_Chi_Tiet.Ma_Bc_Goc = (int)pMa_Bc_Goc.Value;
            myE1_GSS_Chi_Tiet.Ma_Bc_Tra = (int)pMa_Bc_Tra.Value;
            myE1_GSS_Chi_Tiet.Nuoc_Tra = (string)pNuoc_Tra.Value;
            myE1_GSS_Chi_Tiet.Phan_Loai = (string)pPhan_Loai.Value;
            myE1_GSS_Chi_Tiet.Cong_Ty = (string)pCong_Ty.Value;
            myE1_GSS_Chi_Tiet.MST = (string)pMST.Value;
            myE1_GSS_Chi_Tiet.Tinh_Thanh = (string)pTinh_Thanh.Value;
            myE1_GSS_Chi_Tiet.Ngay_He_Thong = (DateTime)pNgay_He_Thong.Value;
            return myE1_GSS_Chi_Tiet;
        }

        #region Them
        // Ngày tạo: 07/07/2008
        // Người tạo: Nguyễn Bằng
        // Nội dung: Thêm dữ liệu vào bảng E1_GSS
        // Input:  Ngay_Gui, Ngay_Phat_Hanh, Gio_Phat_Hanh, Dot_Giao, Khoi_Luong, Gia_Tri_Hang, Cuoc_DV, Cuoc_Chinh, Cuoc_Cod, Cuoc_E1, Tong_Tien, Nguoi_Gui, Dien_Thoai_Gui, Dia_Chi_Gui, Nguoi_Nhan, Dien_Thoai_Nhan, Dia_Chi_Nhan, Trang_Thai, Ghi_Chu, Chot_SL, Ngay_Nhan_DL, Gio_Nhan_DL, Ma_Bc_Goc, Ma_Bc_Tra, Nuoc_Tra, Phan_Loai, Cong_Ty, MST, Ngay_He_Thong,
        // Output: 
        public void Them(int Ngay_Gui, int Ngay_Phat_Hanh, int Gio_Phat_Hanh, int Dot_Giao, int Khoi_Luong, int Gia_Tri_Hang, int Cuoc_DV, int Cuoc_Chinh, int Cuoc_Cod, int Cuoc_E1, int Tong_Tien, string Nguoi_Gui, string Dien_Thoai_Gui, string Dia_Chi_Gui, string Nguoi_Nhan, string Dien_Thoai_Nhan, string Dia_Chi_Nhan, string Trang_Thai, string Ghi_Chu, int Chot_SL, int Ngay_Nhan_DL, int Gio_Nhan_DL, int Ma_Bc_Goc, int Ma_Bc_Tra, string Nuoc_Tra, string Phan_Loai, string Cong_Ty, string MST, DateTime Ngay_He_Thong)
        {
            // Tạo đối tượng connection và command
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("E1_GSS_Them", myConnection);

            // Sử dụng Store Procedure
            myCommand.CommandType = CommandType.StoredProcedure;
            // Thêm các Parameters vào thủ tục
            SqlParameter pNgay_Gui = new SqlParameter("@Ngay_Gui", SqlDbType.Int, 4);
            pNgay_Gui.Value = Ngay_Gui;
            myCommand.Parameters.Add(pNgay_Gui);

            SqlParameter pNgay_Phat_Hanh = new SqlParameter("@Ngay_Phat_Hanh", SqlDbType.Int, 4);
            pNgay_Phat_Hanh.Value = Ngay_Phat_Hanh;
            myCommand.Parameters.Add(pNgay_Phat_Hanh);

            SqlParameter pGio_Phat_Hanh = new SqlParameter("@Gio_Phat_Hanh", SqlDbType.Int, 4);
            pGio_Phat_Hanh.Value = Gio_Phat_Hanh;
            myCommand.Parameters.Add(pGio_Phat_Hanh);

            SqlParameter pDot_Giao = new SqlParameter("@Dot_Giao", SqlDbType.Int, 4);
            pDot_Giao.Value = Dot_Giao;
            myCommand.Parameters.Add(pDot_Giao);

            SqlParameter pKhoi_Luong = new SqlParameter("@Khoi_Luong", SqlDbType.Int, 4);
            pKhoi_Luong.Value = Khoi_Luong;
            myCommand.Parameters.Add(pKhoi_Luong);

            SqlParameter pGia_Tri_Hang = new SqlParameter("@Gia_Tri_Hang", SqlDbType.Int, 4);
            pGia_Tri_Hang.Value = Gia_Tri_Hang;
            myCommand.Parameters.Add(pGia_Tri_Hang);

            SqlParameter pCuoc_DV = new SqlParameter("@Cuoc_DV", SqlDbType.Int, 4);
            pCuoc_DV.Value = Cuoc_DV;
            myCommand.Parameters.Add(pCuoc_DV);

            SqlParameter pCuoc_Chinh = new SqlParameter("@Cuoc_Chinh", SqlDbType.Int, 4);
            pCuoc_Chinh.Value = Cuoc_Chinh;
            myCommand.Parameters.Add(pCuoc_Chinh);

            SqlParameter pCuoc_Cod = new SqlParameter("@Cuoc_Cod", SqlDbType.Int, 4);
            pCuoc_Cod.Value = Cuoc_Cod;
            myCommand.Parameters.Add(pCuoc_Cod);

            SqlParameter pCuoc_E1 = new SqlParameter("@Cuoc_E1", SqlDbType.Int, 4);
            pCuoc_E1.Value = Cuoc_E1;
            myCommand.Parameters.Add(pCuoc_E1);

            SqlParameter pTong_Tien = new SqlParameter("@Tong_Tien", SqlDbType.Int, 4);
            pTong_Tien.Value = Tong_Tien;
            myCommand.Parameters.Add(pTong_Tien);

            SqlParameter pNguoi_Gui = new SqlParameter("@Nguoi_Gui", SqlDbType.NVarChar, 100);
            pNguoi_Gui.Value = Nguoi_Gui;
            myCommand.Parameters.Add(pNguoi_Gui);

            SqlParameter pDien_Thoai_Gui = new SqlParameter("@Dien_Thoai_Gui", SqlDbType.VarChar, 100);
            pDien_Thoai_Gui.Value = Dien_Thoai_Gui;
            myCommand.Parameters.Add(pDien_Thoai_Gui);

            SqlParameter pDia_Chi_Gui = new SqlParameter("@Dia_Chi_Gui", SqlDbType.NVarChar, 200);
            pDia_Chi_Gui.Value = Dia_Chi_Gui;
            myCommand.Parameters.Add(pDia_Chi_Gui);

            SqlParameter pNguoi_Nhan = new SqlParameter("@Nguoi_Nhan", SqlDbType.NVarChar, 100);
            pNguoi_Nhan.Value = Nguoi_Nhan;
            myCommand.Parameters.Add(pNguoi_Nhan);

            SqlParameter pDien_Thoai_Nhan = new SqlParameter("@Dien_Thoai_Nhan", SqlDbType.VarChar, 100);
            pDien_Thoai_Nhan.Value = Dien_Thoai_Nhan;
            myCommand.Parameters.Add(pDien_Thoai_Nhan);

            SqlParameter pDia_Chi_Nhan = new SqlParameter("@Dia_Chi_Nhan", SqlDbType.NVarChar, 200);
            pDia_Chi_Nhan.Value = Dia_Chi_Nhan;
            myCommand.Parameters.Add(pDia_Chi_Nhan);

            SqlParameter pTrang_Thai = new SqlParameter("@Trang_Thai", SqlDbType.VarChar, 10);
            pTrang_Thai.Value = Trang_Thai;
            myCommand.Parameters.Add(pTrang_Thai);

            SqlParameter pGhi_Chu = new SqlParameter("@Ghi_Chu", SqlDbType.NVarChar, 200);
            pGhi_Chu.Value = Ghi_Chu;
            myCommand.Parameters.Add(pGhi_Chu);

            SqlParameter pChot_SL = new SqlParameter("@Chot_SL", SqlDbType.Int, 4);
            pChot_SL.Value = Chot_SL;
            myCommand.Parameters.Add(pChot_SL);

            SqlParameter pNgay_Nhan_DL = new SqlParameter("@Ngay_Nhan_DL", SqlDbType.Int, 4);
            pNgay_Nhan_DL.Value = Ngay_Nhan_DL;
            myCommand.Parameters.Add(pNgay_Nhan_DL);

            SqlParameter pGio_Nhan_DL = new SqlParameter("@Gio_Nhan_DL", SqlDbType.Int, 4);
            pGio_Nhan_DL.Value = Gio_Nhan_DL;
            myCommand.Parameters.Add(pGio_Nhan_DL);

            SqlParameter pMa_Bc_Goc = new SqlParameter("@Ma_Bc_Goc", SqlDbType.Int, 4);
            pMa_Bc_Goc.Value = Ma_Bc_Goc;
            myCommand.Parameters.Add(pMa_Bc_Goc);

            SqlParameter pMa_Bc_Tra = new SqlParameter("@Ma_Bc_Tra", SqlDbType.Int, 4);
            pMa_Bc_Tra.Value = Ma_Bc_Tra;
            myCommand.Parameters.Add(pMa_Bc_Tra);

            SqlParameter pNuoc_Tra = new SqlParameter("@Nuoc_Tra", SqlDbType.VarChar, 2);
            pNuoc_Tra.Value = Nuoc_Tra;
            myCommand.Parameters.Add(pNuoc_Tra);

            SqlParameter pPhan_Loai = new SqlParameter("@Phan_Loai", SqlDbType.VarChar, 1);
            pPhan_Loai.Value = Phan_Loai;
            myCommand.Parameters.Add(pPhan_Loai);

            SqlParameter pCong_Ty = new SqlParameter("@Cong_Ty", SqlDbType.NVarChar, 200);
            pCong_Ty.Value = Cong_Ty;
            myCommand.Parameters.Add(pCong_Ty);

            SqlParameter pMST = new SqlParameter("@MST", SqlDbType.VarChar, 20);
            pMST.Value = MST;
            myCommand.Parameters.Add(pMST);

            SqlParameter pNgay_He_Thong = new SqlParameter("@Ngay_He_Thong", SqlDbType.DateTime);
            pNgay_He_Thong.Value = Ngay_He_Thong;
            myCommand.Parameters.Add(pNgay_He_Thong);


            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
        #endregion
        #region Cap_Nhat
        // Ngày tạo: 07/07/2008
        // Người tạo: Nguyễn Bằng
        // Nội dung: Cập nhật dữ liệu vào bảng E1_GSS
        // Input: Ma_E1,  Ngay_Gui , Ngay_Phat_Hanh , Gio_Phat_Hanh , Dot_Giao , Khoi_Luong , Gia_Tri_Hang , Cuoc_DV , Cuoc_Chinh , Cuoc_Cod , Cuoc_E1 , Tong_Tien , Nguoi_Gui , Dien_Thoai_Gui , Dia_Chi_Gui , Nguoi_Nhan , Dien_Thoai_Nhan , Dia_Chi_Nhan , Trang_Thai , Ghi_Chu , Chot_SL , Ngay_Nhan_DL , Gio_Nhan_DL , Ma_Bc_Goc , Ma_Bc_Tra , Nuoc_Tra , Phan_Loai , Cong_Ty , MST , Ngay_He_Thong ,
        // Output: 
        public void Cap_Nhat(string Ma_E1, int Ngay_Gui, int Ngay_Phat_Hanh, int Gio_Phat_Hanh, int Dot_Giao, int Khoi_Luong, int Gia_Tri_Hang, int Cuoc_DV, int Cuoc_Chinh, int Cuoc_Cod, int Cuoc_E1, int Tong_Tien, string Nguoi_Gui, string Dien_Thoai_Gui, string Dia_Chi_Gui, string Nguoi_Nhan, string Dien_Thoai_Nhan, string Dia_Chi_Nhan, string Trang_Thai, string Ghi_Chu, int Chot_SL, int Ngay_Nhan_DL, int Gio_Nhan_DL, int Ma_Bc_Goc, int Ma_Bc_Tra, string Nuoc_Tra, string Phan_Loai, string Cong_Ty, string MST, DateTime Ngay_He_Thong)
        {
            // Tạo đối tượng connection và command
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("E1_GSS_Cap_Nhat", myConnection);

            // Sử dụng Store Procedure
            myCommand.CommandType = CommandType.StoredProcedure;
            // Thêm các Parameters vào thủ tục
            SqlParameter pMa_E1 = new SqlParameter("@Ma_E1", SqlDbType.VarChar, 13);
            pMa_E1.Value = Ma_E1;
            myCommand.Parameters.Add(pMa_E1);

            SqlParameter pNgay_Gui = new SqlParameter("@Ngay_Gui", SqlDbType.Int, 4);
            pNgay_Gui.Value = Ngay_Gui;
            myCommand.Parameters.Add(pNgay_Gui);

            SqlParameter pNgay_Phat_Hanh = new SqlParameter("@Ngay_Phat_Hanh", SqlDbType.Int, 4);
            pNgay_Phat_Hanh.Value = Ngay_Phat_Hanh;
            myCommand.Parameters.Add(pNgay_Phat_Hanh);

            SqlParameter pGio_Phat_Hanh = new SqlParameter("@Gio_Phat_Hanh", SqlDbType.Int, 4);
            pGio_Phat_Hanh.Value = Gio_Phat_Hanh;
            myCommand.Parameters.Add(pGio_Phat_Hanh);

            SqlParameter pDot_Giao = new SqlParameter("@Dot_Giao", SqlDbType.Int, 4);
            pDot_Giao.Value = Dot_Giao;
            myCommand.Parameters.Add(pDot_Giao);

            SqlParameter pKhoi_Luong = new SqlParameter("@Khoi_Luong", SqlDbType.Int, 4);
            pKhoi_Luong.Value = Khoi_Luong;
            myCommand.Parameters.Add(pKhoi_Luong);

            SqlParameter pGia_Tri_Hang = new SqlParameter("@Gia_Tri_Hang", SqlDbType.Int, 4);
            pGia_Tri_Hang.Value = Gia_Tri_Hang;
            myCommand.Parameters.Add(pGia_Tri_Hang);

            SqlParameter pCuoc_DV = new SqlParameter("@Cuoc_DV", SqlDbType.Int, 4);
            pCuoc_DV.Value = Cuoc_DV;
            myCommand.Parameters.Add(pCuoc_DV);

            SqlParameter pCuoc_Chinh = new SqlParameter("@Cuoc_Chinh", SqlDbType.Int, 4);
            pCuoc_Chinh.Value = Cuoc_Chinh;
            myCommand.Parameters.Add(pCuoc_Chinh);

            SqlParameter pCuoc_Cod = new SqlParameter("@Cuoc_Cod", SqlDbType.Int, 4);
            pCuoc_Cod.Value = Cuoc_Cod;
            myCommand.Parameters.Add(pCuoc_Cod);

            SqlParameter pCuoc_E1 = new SqlParameter("@Cuoc_E1", SqlDbType.Int, 4);
            pCuoc_E1.Value = Cuoc_E1;
            myCommand.Parameters.Add(pCuoc_E1);

            SqlParameter pTong_Tien = new SqlParameter("@Tong_Tien", SqlDbType.Int, 4);
            pTong_Tien.Value = Tong_Tien;
            myCommand.Parameters.Add(pTong_Tien);

            SqlParameter pNguoi_Gui = new SqlParameter("@Nguoi_Gui", SqlDbType.NVarChar, 100);
            pNguoi_Gui.Value = Nguoi_Gui;
            myCommand.Parameters.Add(pNguoi_Gui);

            SqlParameter pDien_Thoai_Gui = new SqlParameter("@Dien_Thoai_Gui", SqlDbType.VarChar, 100);
            pDien_Thoai_Gui.Value = Dien_Thoai_Gui;
            myCommand.Parameters.Add(pDien_Thoai_Gui);

            SqlParameter pDia_Chi_Gui = new SqlParameter("@Dia_Chi_Gui", SqlDbType.NVarChar, 200);
            pDia_Chi_Gui.Value = Dia_Chi_Gui;
            myCommand.Parameters.Add(pDia_Chi_Gui);

            SqlParameter pNguoi_Nhan = new SqlParameter("@Nguoi_Nhan", SqlDbType.NVarChar, 100);
            pNguoi_Nhan.Value = Nguoi_Nhan;
            myCommand.Parameters.Add(pNguoi_Nhan);

            SqlParameter pDien_Thoai_Nhan = new SqlParameter("@Dien_Thoai_Nhan", SqlDbType.VarChar, 100);
            pDien_Thoai_Nhan.Value = Dien_Thoai_Nhan;
            myCommand.Parameters.Add(pDien_Thoai_Nhan);

            SqlParameter pDia_Chi_Nhan = new SqlParameter("@Dia_Chi_Nhan", SqlDbType.NVarChar, 200);
            pDia_Chi_Nhan.Value = Dia_Chi_Nhan;
            myCommand.Parameters.Add(pDia_Chi_Nhan);

            SqlParameter pTrang_Thai = new SqlParameter("@Trang_Thai", SqlDbType.VarChar, 10);
            pTrang_Thai.Value = Trang_Thai;
            myCommand.Parameters.Add(pTrang_Thai);

            SqlParameter pGhi_Chu = new SqlParameter("@Ghi_Chu", SqlDbType.NVarChar, 200);
            pGhi_Chu.Value = Ghi_Chu;
            myCommand.Parameters.Add(pGhi_Chu);

            SqlParameter pChot_SL = new SqlParameter("@Chot_SL", SqlDbType.Int, 4);
            pChot_SL.Value = Chot_SL;
            myCommand.Parameters.Add(pChot_SL);

            SqlParameter pNgay_Nhan_DL = new SqlParameter("@Ngay_Nhan_DL", SqlDbType.Int, 4);
            pNgay_Nhan_DL.Value = Ngay_Nhan_DL;
            myCommand.Parameters.Add(pNgay_Nhan_DL);

            SqlParameter pGio_Nhan_DL = new SqlParameter("@Gio_Nhan_DL", SqlDbType.Int, 4);
            pGio_Nhan_DL.Value = Gio_Nhan_DL;
            myCommand.Parameters.Add(pGio_Nhan_DL);

            SqlParameter pMa_Bc_Goc = new SqlParameter("@Ma_Bc_Goc", SqlDbType.Int, 4);
            pMa_Bc_Goc.Value = Ma_Bc_Goc;
            myCommand.Parameters.Add(pMa_Bc_Goc);

            SqlParameter pMa_Bc_Tra = new SqlParameter("@Ma_Bc_Tra", SqlDbType.Int, 4);
            pMa_Bc_Tra.Value = Ma_Bc_Tra;
            myCommand.Parameters.Add(pMa_Bc_Tra);

            SqlParameter pNuoc_Tra = new SqlParameter("@Nuoc_Tra", SqlDbType.VarChar, 2);
            pNuoc_Tra.Value = Nuoc_Tra;
            myCommand.Parameters.Add(pNuoc_Tra);

            SqlParameter pPhan_Loai = new SqlParameter("@Phan_Loai", SqlDbType.VarChar, 1);
            pPhan_Loai.Value = Phan_Loai;
            myCommand.Parameters.Add(pPhan_Loai);

            SqlParameter pCong_Ty = new SqlParameter("@Cong_Ty", SqlDbType.NVarChar, 200);
            pCong_Ty.Value = Cong_Ty;
            myCommand.Parameters.Add(pCong_Ty);

            SqlParameter pMST = new SqlParameter("@MST", SqlDbType.VarChar, 20);
            pMST.Value = MST;
            myCommand.Parameters.Add(pMST);

            SqlParameter pNgay_He_Thong = new SqlParameter("@Ngay_He_Thong", SqlDbType.DateTime);
            pNgay_He_Thong.Value = Ngay_He_Thong;
            myCommand.Parameters.Add(pNgay_He_Thong);


            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();

            }
            catch
            {
            }
        }
        #endregion
        #region Cap_Nhat
        // Ngày tạo: 07/07/2008
        // Người tạo: Nguyễn Bằng
        // Nội dung: Cập nhật dữ liệu vào bảng E1_GSS
        // Input: đối tượng thuộc lớp E1_GSS_Chi_Tiet
        // Output: 
        public void Cap_Nhat(E1_GSS_Chi_Tiet myE1_GSS_Chi_Tiet)
        {
            // Tạo đối tượng connection và command
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("E1_GSS_Cap_Nhat", myConnection);

            // Sử dụng Store Procedure
            myCommand.CommandType = CommandType.StoredProcedure;
            // Thêm các Parameters vào thủ tục			
            SqlParameter pMa_E1 = new SqlParameter("@Ma_E1", SqlDbType.VarChar, 13);
            pMa_E1.Value = myE1_GSS_Chi_Tiet.Ma_E1;
            myCommand.Parameters.Add(pMa_E1);

            SqlParameter pNgay_Gui = new SqlParameter("@Ngay_Gui", SqlDbType.Int, 4);
            pNgay_Gui.Value = myE1_GSS_Chi_Tiet.Ngay_Gui;
            myCommand.Parameters.Add(pNgay_Gui);

            SqlParameter pNgay_Phat_Hanh = new SqlParameter("@Ngay_Phat_Hanh", SqlDbType.Int, 4);
            pNgay_Phat_Hanh.Value = myE1_GSS_Chi_Tiet.Ngay_Phat_Hanh;
            myCommand.Parameters.Add(pNgay_Phat_Hanh);

            SqlParameter pGio_Phat_Hanh = new SqlParameter("@Gio_Phat_Hanh", SqlDbType.Int, 4);
            pGio_Phat_Hanh.Value = myE1_GSS_Chi_Tiet.Gio_Phat_Hanh;
            myCommand.Parameters.Add(pGio_Phat_Hanh);

            SqlParameter pDot_Giao = new SqlParameter("@Dot_Giao", SqlDbType.Int, 4);
            pDot_Giao.Value = myE1_GSS_Chi_Tiet.Dot_Giao;
            myCommand.Parameters.Add(pDot_Giao);

            SqlParameter pKhoi_Luong = new SqlParameter("@Khoi_Luong", SqlDbType.Int, 4);
            pKhoi_Luong.Value = myE1_GSS_Chi_Tiet.Khoi_Luong;
            myCommand.Parameters.Add(pKhoi_Luong);

            SqlParameter pGia_Tri_Hang = new SqlParameter("@Gia_Tri_Hang", SqlDbType.Int, 4);
            pGia_Tri_Hang.Value = myE1_GSS_Chi_Tiet.Gia_Tri_Hang;
            myCommand.Parameters.Add(pGia_Tri_Hang);

            SqlParameter pCuoc_DV = new SqlParameter("@Cuoc_DV", SqlDbType.Int, 4);
            pCuoc_DV.Value = myE1_GSS_Chi_Tiet.Cuoc_DV;
            myCommand.Parameters.Add(pCuoc_DV);

            SqlParameter pCuoc_Chinh = new SqlParameter("@Cuoc_Chinh", SqlDbType.Int, 4);
            pCuoc_Chinh.Value = myE1_GSS_Chi_Tiet.Cuoc_Chinh;
            myCommand.Parameters.Add(pCuoc_Chinh);

            SqlParameter pCuoc_Cod = new SqlParameter("@Cuoc_Cod", SqlDbType.Int, 4);
            pCuoc_Cod.Value = myE1_GSS_Chi_Tiet.Cuoc_Cod;
            myCommand.Parameters.Add(pCuoc_Cod);

            SqlParameter pCuoc_E1 = new SqlParameter("@Cuoc_E1", SqlDbType.Int, 4);
            pCuoc_E1.Value = myE1_GSS_Chi_Tiet.Cuoc_E1;
            myCommand.Parameters.Add(pCuoc_E1);

            SqlParameter pTong_Tien = new SqlParameter("@Tong_Tien", SqlDbType.Int, 4);
            pTong_Tien.Value = myE1_GSS_Chi_Tiet.Tong_Tien;
            myCommand.Parameters.Add(pTong_Tien);

            SqlParameter pNguoi_Gui = new SqlParameter("@Nguoi_Gui", SqlDbType.NVarChar, 100);
            pNguoi_Gui.Value = myE1_GSS_Chi_Tiet.Nguoi_Gui;
            myCommand.Parameters.Add(pNguoi_Gui);

            SqlParameter pDien_Thoai_Gui = new SqlParameter("@Dien_Thoai_Gui", SqlDbType.VarChar, 100);
            pDien_Thoai_Gui.Value = myE1_GSS_Chi_Tiet.Dien_Thoai_Gui;
            myCommand.Parameters.Add(pDien_Thoai_Gui);

            SqlParameter pDia_Chi_Gui = new SqlParameter("@Dia_Chi_Gui", SqlDbType.NVarChar, 200);
            pDia_Chi_Gui.Value = myE1_GSS_Chi_Tiet.Dia_Chi_Gui;
            myCommand.Parameters.Add(pDia_Chi_Gui);

            SqlParameter pNguoi_Nhan = new SqlParameter("@Nguoi_Nhan", SqlDbType.NVarChar, 100);
            pNguoi_Nhan.Value = myE1_GSS_Chi_Tiet.Nguoi_Nhan;
            myCommand.Parameters.Add(pNguoi_Nhan);

            SqlParameter pDien_Thoai_Nhan = new SqlParameter("@Dien_Thoai_Nhan", SqlDbType.VarChar, 100);
            pDien_Thoai_Nhan.Value = myE1_GSS_Chi_Tiet.Dien_Thoai_Nhan;
            myCommand.Parameters.Add(pDien_Thoai_Nhan);

            SqlParameter pDia_Chi_Nhan = new SqlParameter("@Dia_Chi_Nhan", SqlDbType.NVarChar, 200);
            pDia_Chi_Nhan.Value = myE1_GSS_Chi_Tiet.Dia_Chi_Nhan;
            myCommand.Parameters.Add(pDia_Chi_Nhan);

            SqlParameter pTrang_Thai = new SqlParameter("@Trang_Thai", SqlDbType.VarChar, 10);
            pTrang_Thai.Value = myE1_GSS_Chi_Tiet.Trang_Thai;
            myCommand.Parameters.Add(pTrang_Thai);

            SqlParameter pGhi_Chu = new SqlParameter("@Ghi_Chu", SqlDbType.NVarChar, 200);
            pGhi_Chu.Value = myE1_GSS_Chi_Tiet.Ghi_Chu;
            myCommand.Parameters.Add(pGhi_Chu);

            SqlParameter pChot_SL = new SqlParameter("@Chot_SL", SqlDbType.Int, 4);
            pChot_SL.Value = myE1_GSS_Chi_Tiet.Chot_SL;
            myCommand.Parameters.Add(pChot_SL);

            SqlParameter pNgay_Nhan_DL = new SqlParameter("@Ngay_Nhan_DL", SqlDbType.Int, 4);
            pNgay_Nhan_DL.Value = myE1_GSS_Chi_Tiet.Ngay_Nhan_DL;
            myCommand.Parameters.Add(pNgay_Nhan_DL);

            SqlParameter pGio_Nhan_DL = new SqlParameter("@Gio_Nhan_DL", SqlDbType.Int, 4);
            pGio_Nhan_DL.Value = myE1_GSS_Chi_Tiet.Gio_Nhan_DL;
            myCommand.Parameters.Add(pGio_Nhan_DL);

            SqlParameter pMa_Bc_Goc = new SqlParameter("@Ma_Bc_Goc", SqlDbType.Int, 4);
            pMa_Bc_Goc.Value = myE1_GSS_Chi_Tiet.Ma_Bc_Goc;
            myCommand.Parameters.Add(pMa_Bc_Goc);

            SqlParameter pMa_Bc_Tra = new SqlParameter("@Ma_Bc_Tra", SqlDbType.Int, 4);
            pMa_Bc_Tra.Value = myE1_GSS_Chi_Tiet.Ma_Bc_Tra;
            myCommand.Parameters.Add(pMa_Bc_Tra);

            SqlParameter pNuoc_Tra = new SqlParameter("@Nuoc_Tra", SqlDbType.VarChar, 2);
            pNuoc_Tra.Value = myE1_GSS_Chi_Tiet.Nuoc_Tra;
            myCommand.Parameters.Add(pNuoc_Tra);

            SqlParameter pPhan_Loai = new SqlParameter("@Phan_Loai", SqlDbType.VarChar, 1);
            pPhan_Loai.Value = myE1_GSS_Chi_Tiet.Phan_Loai;
            myCommand.Parameters.Add(pPhan_Loai);

            SqlParameter pCong_Ty = new SqlParameter("@Cong_Ty", SqlDbType.NVarChar, 200);
            pCong_Ty.Value = myE1_GSS_Chi_Tiet.Cong_Ty;
            myCommand.Parameters.Add(pCong_Ty);

            SqlParameter pMST = new SqlParameter("@MST", SqlDbType.VarChar, 20);
            pMST.Value = myE1_GSS_Chi_Tiet.MST;
            myCommand.Parameters.Add(pMST);

            SqlParameter pNgay_He_Thong = new SqlParameter("@Ngay_He_Thong", SqlDbType.DateTime);
            pNgay_He_Thong.Value = myE1_GSS_Chi_Tiet.Ngay_He_Thong;
            myCommand.Parameters.Add(pNgay_He_Thong);


            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            catch
            {
            }
        }
        #endregion
        #region Cap_Nhat
        // Ngày tạo: 07/07/2008
        // Người tạo: Nguyễn Bằng
        // Nội dung: Cập nhật thêm mới một dataset vào bảng E1_GSS
        // Input: đối tượng dataset
        // Output: 
        public void Cap_Nhat(DataSet myDataSet, String strTableName)
        {
            // Tạo đối tượng connection và command
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            myConnection.Open();
            for (int i = 0; i < myDataSet.Tables[strTableName].Rows.Count; i++)
            {
                SqlCommand myCommand = new SqlCommand("E1_GSS_Cap_Nhat_Them", myConnection);
                // Sử dụng Store Procedure
                myCommand.CommandType = CommandType.StoredProcedure;
                // Thêm các Parameters vào thủ tục			
                SqlParameter pMa_E1 = new SqlParameter("@Ma_E1", SqlDbType.VarChar, 13);
                pMa_E1.Value = (string)myDataSet.Tables[strTableName].Rows[i]["Ma_E1"];
                myCommand.Parameters.Add(pMa_E1);

                SqlParameter pNgay_Gui = new SqlParameter("@Ngay_Gui", SqlDbType.Int, 4);
                pNgay_Gui.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Ngay_Gui"];
                myCommand.Parameters.Add(pNgay_Gui);

                SqlParameter pNgay_Phat_Hanh = new SqlParameter("@Ngay_Phat_Hanh", SqlDbType.Int, 4);
                pNgay_Phat_Hanh.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Ngay_Phat_Hanh"];
                myCommand.Parameters.Add(pNgay_Phat_Hanh);

                SqlParameter pGio_Phat_Hanh = new SqlParameter("@Gio_Phat_Hanh", SqlDbType.Int, 4);
                pGio_Phat_Hanh.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Gio_Phat_Hanh"];
                myCommand.Parameters.Add(pGio_Phat_Hanh);

                SqlParameter pDot_Giao = new SqlParameter("@Dot_Giao", SqlDbType.Int, 4);
                pDot_Giao.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Dot_Giao"];
                myCommand.Parameters.Add(pDot_Giao);

                SqlParameter pKhoi_Luong = new SqlParameter("@Khoi_Luong", SqlDbType.Int, 4);
                pKhoi_Luong.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Khoi_Luong"];
                myCommand.Parameters.Add(pKhoi_Luong);

                SqlParameter pGia_Tri_Hang = new SqlParameter("@Gia_Tri_Hang", SqlDbType.Int, 4);
                pGia_Tri_Hang.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Gia_Tri_Hang"];
                myCommand.Parameters.Add(pGia_Tri_Hang);

                SqlParameter pCuoc_DV = new SqlParameter("@Cuoc_DV", SqlDbType.Int, 4);
                pCuoc_DV.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Cuoc_DV"];
                myCommand.Parameters.Add(pCuoc_DV);

                SqlParameter pCuoc_Chinh = new SqlParameter("@Cuoc_Chinh", SqlDbType.Int, 4);
                pCuoc_Chinh.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Cuoc_Chinh"];
                myCommand.Parameters.Add(pCuoc_Chinh);

                SqlParameter pCuoc_Cod = new SqlParameter("@Cuoc_Cod", SqlDbType.Int, 4);
                pCuoc_Cod.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Cuoc_Cod"];
                myCommand.Parameters.Add(pCuoc_Cod);

                SqlParameter pCuoc_E1 = new SqlParameter("@Cuoc_E1", SqlDbType.Int, 4);
                pCuoc_E1.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Cuoc_E1"];
                myCommand.Parameters.Add(pCuoc_E1);

                SqlParameter pTong_Tien = new SqlParameter("@Tong_Tien", SqlDbType.Int, 4);
                pTong_Tien.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Tong_Tien"];
                myCommand.Parameters.Add(pTong_Tien);

                SqlParameter pNguoi_Gui = new SqlParameter("@Nguoi_Gui", SqlDbType.NVarChar, 100);
                pNguoi_Gui.Value = (string)myDataSet.Tables[strTableName].Rows[i]["Nguoi_Gui"];
                myCommand.Parameters.Add(pNguoi_Gui);

                SqlParameter pDien_Thoai_Gui = new SqlParameter("@Dien_Thoai_Gui", SqlDbType.VarChar, 100);
                pDien_Thoai_Gui.Value = (string)myDataSet.Tables[strTableName].Rows[i]["Dien_Thoai_Gui"];
                myCommand.Parameters.Add(pDien_Thoai_Gui);

                SqlParameter pDia_Chi_Gui = new SqlParameter("@Dia_Chi_Gui", SqlDbType.NVarChar, 200);
                pDia_Chi_Gui.Value = (string)myDataSet.Tables[strTableName].Rows[i]["Dia_Chi_Gui"];
                myCommand.Parameters.Add(pDia_Chi_Gui);

                SqlParameter pNguoi_Nhan = new SqlParameter("@Nguoi_Nhan", SqlDbType.NVarChar, 100);
                pNguoi_Nhan.Value = (string)myDataSet.Tables[strTableName].Rows[i]["Nguoi_Nhan"];
                myCommand.Parameters.Add(pNguoi_Nhan);

                SqlParameter pDien_Thoai_Nhan = new SqlParameter("@Dien_Thoai_Nhan", SqlDbType.VarChar, 100);
                pDien_Thoai_Nhan.Value = (string)myDataSet.Tables[strTableName].Rows[i]["Dien_Thoai_Nhan"];
                myCommand.Parameters.Add(pDien_Thoai_Nhan);

                SqlParameter pDia_Chi_Nhan = new SqlParameter("@Dia_Chi_Nhan", SqlDbType.NVarChar, 200);
                pDia_Chi_Nhan.Value = (string)myDataSet.Tables[strTableName].Rows[i]["Dia_Chi_Nhan"];
                myCommand.Parameters.Add(pDia_Chi_Nhan);

                SqlParameter pTrang_Thai = new SqlParameter("@Trang_Thai", SqlDbType.VarChar, 10);
                pTrang_Thai.Value = (string)myDataSet.Tables[strTableName].Rows[i]["Trang_Thai"];
                myCommand.Parameters.Add(pTrang_Thai);

                SqlParameter pGhi_Chu = new SqlParameter("@Ghi_Chu", SqlDbType.NVarChar, 200);
                pGhi_Chu.Value = (string)myDataSet.Tables[strTableName].Rows[i]["Ghi_Chu"];
                myCommand.Parameters.Add(pGhi_Chu);

                SqlParameter pChot_SL = new SqlParameter("@Chot_SL", SqlDbType.Int, 4);
                pChot_SL.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Chot_SL"];
                myCommand.Parameters.Add(pChot_SL);

                SqlParameter pNgay_Nhan_DL = new SqlParameter("@Ngay_Nhan_DL", SqlDbType.Int, 4);
                pNgay_Nhan_DL.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Ngay_Nhan_DL"];
                myCommand.Parameters.Add(pNgay_Nhan_DL);

                SqlParameter pGio_Nhan_DL = new SqlParameter("@Gio_Nhan_DL", SqlDbType.Int, 4);
                pGio_Nhan_DL.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Gio_Nhan_DL"];
                myCommand.Parameters.Add(pGio_Nhan_DL);

                SqlParameter pMa_Bc_Goc = new SqlParameter("@Ma_Bc_Goc", SqlDbType.Int, 4);
                pMa_Bc_Goc.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Ma_Bc_Goc"];
                myCommand.Parameters.Add(pMa_Bc_Goc);

                SqlParameter pMa_Bc_Tra = new SqlParameter("@Ma_Bc_Tra", SqlDbType.Int, 4);
                pMa_Bc_Tra.Value = (int)myDataSet.Tables[strTableName].Rows[i]["Ma_Bc_Tra"];
                myCommand.Parameters.Add(pMa_Bc_Tra);

                SqlParameter pNuoc_Tra = new SqlParameter("@Nuoc_Tra", SqlDbType.VarChar, 2);
                pNuoc_Tra.Value = (string)myDataSet.Tables[strTableName].Rows[i]["Nuoc_Tra"];
                myCommand.Parameters.Add(pNuoc_Tra);

                SqlParameter pPhan_Loai = new SqlParameter("@Phan_Loai", SqlDbType.VarChar, 1);
                pPhan_Loai.Value = (string)myDataSet.Tables[strTableName].Rows[i]["Phan_Loai"];
                myCommand.Parameters.Add(pPhan_Loai);

                SqlParameter pCong_Ty = new SqlParameter("@Cong_Ty", SqlDbType.NVarChar, 200);
                pCong_Ty.Value = (string)myDataSet.Tables[strTableName].Rows[i]["Cong_Ty"];
                myCommand.Parameters.Add(pCong_Ty);

                SqlParameter pMST = new SqlParameter("@MST", SqlDbType.VarChar, 20);
                pMST.Value = (string)myDataSet.Tables[strTableName].Rows[i]["MST"];
                myCommand.Parameters.Add(pMST);

                SqlParameter pNgay_He_Thong = new SqlParameter("@Ngay_He_Thong", SqlDbType.DateTime);
                pNgay_He_Thong.Value = (DateTime)myDataSet.Tables[strTableName].Rows[i]["Ngay_He_Thong"];
                myCommand.Parameters.Add(pNgay_He_Thong);


                myCommand.ExecuteNonQuery();

            }
            myConnection.Close();
        }
        #endregion

        #region Cap_Nhat_Chot_SL
        
        public void Cap_Nhat_Chot_SL(string Ma_E1)
        {
            // Tạo đối tượng connection và command
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("E1_GSS_Cap_Nhat_Chot_SL", myConnection);

            // Sử dụng Store Procedure
            myCommand.CommandType = CommandType.StoredProcedure;
            // Thêm các Parameters vào thủ tục

            SqlParameter pMa_E1 = new SqlParameter("@Ma_E1", SqlDbType.VarChar, 13);
            pMa_E1.Value = Ma_E1;
            myCommand.Parameters.Add(pMa_E1);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            catch
            {
            }
        }
        #endregion
        #region Cap_Nhat_RB

        public void Cap_Nhat_RB(string Ma_E1)
        {
            // Tạo đối tượng connection và command
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("E1_GSS_Cap_Nhat_RBG", myConnection);

            // Sử dụng Store Procedure
            myCommand.CommandType = CommandType.StoredProcedure;
            // Thêm các Parameters vào thủ tục

            SqlParameter pMa_E1 = new SqlParameter("@Ma_E1", SqlDbType.VarChar, 13);
            pMa_E1.Value = Ma_E1;
            myCommand.Parameters.Add(pMa_E1);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            catch
            {
            }
        }
        #endregion

        #region Xoa
        // Ngày tạo: 07/07/2008
        // Người tạo: Nguyễn Bằng
        // Nội dung: Xóa dữ liệu từ bảng E1_GSS
        // Input: Ma_E1
        // Output: 	
        public void Xoa(string Ma_E1)
        {
            // Tạo đối tượng connection và command
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("E1_GSS_Xoa", myConnection);

            // Sử dụng Store Procedure
            myCommand.CommandType = CommandType.StoredProcedure;
            // Thêm các Parameters vào thủ tục
            SqlParameter pMa_E1 = new SqlParameter("@Ma_E1", SqlDbType.VarChar, 13);
            pMa_E1.Value = Ma_E1;
            myCommand.Parameters.Add(pMa_E1);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();

            }
            catch
            {
            }
        }
        #endregion
        #region Danh_Sach
        // Ngày tạo: 07/07/2008
        // Người tạo: Nguyễn Bằng
        // Nội dung: Lấy toàn bộ dữ liệu từ bảng E1_GSS
        // Input: 
        // Output: DataSet chứa toàn bộ dữ liệu lấy về 
        public DataTable Danh_Sach()
        {
            // Tạo đối tượng connection và command
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("E1_GSS_Danh_Sach", myConnection);
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            DataSet myDataSet = new DataSet();

            // Sử dụng Store Procedure
            myCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            myAdapter.SelectCommand = myCommand;
            myAdapter.Fill(myDataSet, "E1_GSS_Danh_Sach");
            myConnection.Close();
            return myDataSet.Tables["E1_GSS_Danh_Sach"];

        }
        #endregion
        #region Lay_Ma_E1
        // Ngày tạo: 07/07/2008
        // Người tạo: Nguyễn Bằng
        // Nội dung: Lấy toàn bộ dữ liệu từ bảng E1_GSS
        // Input: 
        // Output: DataSet chứa toàn bộ dữ liệu lấy về 
        public DataTable Lay_Ma_E1(string MA_E1)
        {
            // Tạo đối tượng connection và command
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("E1_GSS_Lay_Ma_E1", myConnection);
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            DataSet myDataSet = new DataSet();

            // Sử dụng Store Procedure
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter pGhi_Chu = new SqlParameter("@PASS_PORT_NUMBER", SqlDbType.VarChar, 13);
            pGhi_Chu.Value = MA_E1;
            myCommand.Parameters.Add(pGhi_Chu);

            myConnection.Open();
            myAdapter.SelectCommand = myCommand;
            myAdapter.Fill(myDataSet, "E1_GSS_Lay_Ma_E1");
            myConnection.Close();
            return myDataSet.Tables["E1_GSS_Lay_Ma_E1"];

        }
        #endregion
        #region Danh_Sach_Lay_Boi_E1
        // Ngày tạo: 07/07/2008
        // Người tạo: Nguyễn Bằng
        // Nội dung: Lấy toàn bộ dữ liệu từ bảng E1_GSS
        // Input: 
        // Output: DataSet chứa toàn bộ dữ liệu lấy về 
        public DataTable Danh_Sach_Lay_Boi_E1(string MA_E1)
        {
            // Tạo đối tượng connection và command
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand("E1_GSS_Danh_Sach_Lay_Boi_E1", myConnection);
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            DataSet myDataSet = new DataSet();

            // Sử dụng Store Procedure
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter pMa_E1 = new SqlParameter("@Ma_E1", SqlDbType.VarChar, 13);
            pMa_E1.Value = MA_E1;
            myCommand.Parameters.Add(pMa_E1);

            myConnection.Open();
            myAdapter.SelectCommand = myCommand;
            myAdapter.Fill(myDataSet, "E1_GSS_Danh_Sach_Lay_Boi_E1");
            myConnection.Close();
            return myDataSet.Tables["E1_GSS_Danh_Sach_Lay_Boi_E1"];

        }
        #endregion
    }

       
}