using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    public class BusinessProfile
    {
        public string CUSTOMER_CODE { get; set; }
        public string GENERAL_SHORT_NAME { get; set; }
        public string GENERAL_EMAIL { get; set; }
        public string GENERAL_ACCOUNT_TYPE { get; set; }
        public string GENERAL_FULL_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string SYSTEM_IS_ACTIVE { get; set; }
        public string BUSINESS_TAX { get; set; }
        public string BUSINESS_WEBSITE { get; set; }
        public string CONTACT_NAME { get; set; }
        public string CONTACT_ADDRESS { get; set; }
        public string CONTACT_DISTRICT { get; set; }
        public string CONTACT_PROVINCE { get; set; }
        public string CONTACT_PHONE_MOBILE { get; set; }
        public string CONTACT_PHONE_WORK { get; set; }
        public string CONTACT_PHONE_FAX { get; set; }
        public string LEGACY_NO { get; set; }
        public string LEGACY_ISSUED_BY { get; set; }        
        public string LEGACY_ISSUED_DATE { get; set; }
        public string SETTLEMENT_CHANNEL { get; set; }
        public string CONTRACT { get; set; }
        public string ACTIVE { get; set; }
        public string PAYMENT_TYPE { get; set; }
        public string UNIT_CODE { get; set; }
        public int CONTACT_WARD { get; set; }
        public string STREET { get; set; }
        public string SYSTEM_REF_CODE { get; set; }
        public string API_KEY { get; set; }
    }

    public class Business_Profile
    {      
        public string GENERAL_EMAIL { get; set; }       
        public string GENERAL_FULL_NAME { get; set; }    
        public string CONTACT_NAME { get; set; }
        public string CONTACT_ADDRESS { get; set; }
        public int CONTACT_DISTRICT { get; set; }
        public int CONTACT_PROVINCE { get; set; }       
        public string CONTACT_PHONE_WORK { get; set; }
        public int CUSTOMER_ID { get; set; }
    }
    public class Business_Profile_Channel
    {
        public string GENERAL_EMAIL { get; set; }
        public string CONTACT_PHONE_WORK { get; set; }   
        public string CUSTOMER_CODE { get; set; }
        public string CONTACT_NAME { get; set; }
        public string CONTACT_ADDRESS { get; set; }        
        public int CONTACT_PROVINCE { get; set; }
        public int CONTACT_DISTRICT { get; set; }

    }


    public class BusinessProfileRef
    {
        public string REF_CODE { get; set; }
        public string REF_DESCRIPTION { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string UNIT_CODE { get; set; }
        public int DATE_CREATE { get; set; }
    }
    public class CustomerEE
    {
        public string  Ma_KH { get; set; }
        public string Ten_Khach_Hang { get; set; }
        public int Ngay_Khoi_Tao { get; set; }
        public int Ngay_Ket_Thuc { get; set; }
        public string Dien_Thoai { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Dia_Chi { get; set; }
        public string Ma_So_Thue { get; set; }
        public double Vat { get; set; }
        public int Khach_Hang_Toan_Quoc { get; set; }
        public int Truyen_Khai_Thac { get; set; }
        public int Ma_Bc_Khai_Thac { get; set; }
        public int Tiem_Nang { get; set; }
        public int Giam_Cuoc { get; set; }
        public string Ma_KH_Tong { get; set; }
        public string Dia_Chi_Thanh_Toan { get; set; }
        public string Hinh_Thuc_Thanh_Toan { get; set; }
        public string So_Hop_Dong { get; set; }  
        public string So_Tai_Khoan { get; set; }
        public string Ngan_Hang { get; set; }
        public string Ma_Quan { get; set; }
        public string Ma_NV_Thu_No { get; set; }
        public string Ma_NV_Sale { get; set; }
        public string Ma_KH_Moi_Gioi { get; set; }
        public int KCX { get; set; }
        public int Loai_Muc_Chi { get; set; }
        public int Ghi_No { get; set; }
        public int Kieu_Chi { get; set; }
        public int Giam_Cuoc_QT { get; set; }
        public int Loai_Dv { get; set; }
        public int Su_Dung_Chung { get; set; }
        public int Tinh_Cuoc_Rieng { get; set; }
    }
}