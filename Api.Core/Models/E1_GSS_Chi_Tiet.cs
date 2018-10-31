using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    public class E1_GSS_Chi_Tiet
    {
        public string getkey(string key)
        {

            string value = "";

            if (key == "Ma_E1")
            {
                value = this.Ma_E1;
            }
            if (key == "Ngay_Gui")
            {
                value = this.Ngay_Gui.ToString();
            }
            if (key == "Dot_Giao")
            {
                value = this.Dot_Giao.ToString();
            }
            if (key == "Khoi_Luong")
            {
                value = this.Khoi_Luong.ToString();
            }
            if (key == "Ngay_Phat_Hanh")
            {
                value = this.Ngay_Phat_Hanh.ToString();
            }
            if (key == "Gio_Phat_Hanh")
            {
                value = this.Gio_Phat_Hanh.ToString();
            }
            if (key == "Dien_Thoai_Nhan")
            {
                value = this.Dien_Thoai_Nhan;
            }
            if (key == "Nguoi_Nhan")
            {
                value = this.Nguoi_Nhan;
            }
            if (key == "Dia_Chi_Nhan")
            {
                value = this.Dia_Chi_Nhan;
            }
            if (key == "Dien_Thoai_Gui")
            {
                value = this.Dien_Thoai_Gui;
            }
            if (key == "Nguoi_Gui")
            {
                value = this.Nguoi_Nhan;
            }
            if (key == "Dia_Chi_Gui")
            {
                value = this.Dia_Chi_Nhan;
            }
            if (key == "Gia_Tri_Hang")
            {
                value = this.Gia_Tri_Hang.ToString();
            }
            if (key == "Cuoc_DV")
            {
                value = this.Cuoc_DV.ToString();
            }
            if (key == "Cuoc_Chinh")
            {
                value = this.Cuoc_Chinh.ToString();
            }
            if (key == "Cuoc_Cod")
            {
                value = this.Cuoc_Cod.ToString();
            }
            if (key == "Cuoc_E1")
            {
                value = this.Cuoc_E1.ToString();
            }
            if (key == "Tong_Tien")
            {
                value = this.Tong_Tien.ToString();
            }

            if (key == "Trang_Thai")
            {
                value = this.Trang_Thai;
            }
            if (key == "Gio_Nhan_DL")
            {
                value = this.Gio_Nhan_DL.ToString();
            }

            if (key == "Ngay_Nhan_DL")
            {
                value = this.Ngay_Nhan_DL.ToString();
            }

            if (key == "Chot_SL")
            {
                value = this.Chot_SL.ToString();
            }

            if (key == "Ghi_Chu")
            {
                value = this.Ghi_Chu;
            }

            if (key == "Phan_Loai")
            {
                value = this.Phan_Loai;
            }

            if (key == "Nuoc_Tra")
            {
                value = this.Nuoc_Tra;
            }

            if (key == "Ma_Bc_Tra")
            {
                value = this.Ma_Bc_Tra.ToString();
            }

            if (key == "Ma_Bc_Goc")
            {
                value = this.Ma_Bc_Goc.ToString();
            }

            if (key == "Cong_Ty")
            {
                value = this.Cong_Ty;
            }

            if (key == "MST")
            {
                value = this.MST;
            }
            return value;
            if (key == "Ngay_He_Thong")
            {
                value = this.Ngay_He_Thong.ToString();
            }
            if (key == "Tinh_Thanh")
            {
                value = this.Tinh_Thanh;
            }
            if (key == "Visa_Class")
            {
                value = this.Visa_Class.ToString();
            }
            if (key == "Schedule_Entry_Time")
            {
                value = this.Schedule_Entry_Time.ToString();
            }
            if (key == "Contact_DS160")
            {
                value = this.Contact_DS160.ToString();
            }
            if (key == "Priority_Name")
            {
                value = this.Priority_Name.ToString();
            }
            return value;
        }
        public string Ma_E1;
        public int Ngay_Gui;
        public int Ngay_Phat_Hanh;
        public int Gio_Phat_Hanh;
        public int Dot_Giao;
        public int Khoi_Luong;
        public int Gia_Tri_Hang;
        public int Cuoc_DV;
        public int Cuoc_Chinh;
        public int Cuoc_Cod;
        public int Cuoc_E1;
        public int Tong_Tien;
        public string Nguoi_Gui;
        public string Dien_Thoai_Gui;
        public string Dia_Chi_Gui;
        public string Nguoi_Nhan;
        public string Dien_Thoai_Nhan;
        public string Dia_Chi_Nhan;
        public string Trang_Thai;
        public string Ghi_Chu;
        public int Chot_SL;
        public int Ngay_Nhan_DL;
        public int Gio_Nhan_DL;
        public int Ma_Bc_Goc;
        public int Ma_Bc_Tra;
        public string Nuoc_Tra;
        public string Phan_Loai;
        public string Cong_Ty;
        public string MST;
        public DateTime Ngay_He_Thong;
        public string Tinh_Thanh;
        public string Visa_Class;
        public int Schedule_Entry_Time;
        public string Contact_DS160;
        public string Priority_Name;
    }
}