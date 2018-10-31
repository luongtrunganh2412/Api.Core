using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.App.Models
{
    public class OrderMobile
    {

        public String MAE1 { get; set; }

        public int NGAYDONG { get; set; }

        public int NGAYNHAN { get; set; }

        public int GIONHAN { get; set; }

        public string NGUOINHAN { get; set; }

        public string DIACHINHAN { get; set; }

        public int MABC { get; set; }

        public int KHOILUONG { get; set; }

        public string STATE { get; set; }

        public string STATE_NAME { get; set; }

        public string STATE_COD { get; set; }

        public int STATEINT { get; set; }

        public int STATECODINT { get; set; }

        public string MAKH { get; set; }

        public int MA_BC_KHAI_THAC { get; set; }

        public int CUOC_E1 { get; set; }

        public string DIEN_THOAI_NHAN { get; set; }

        public string NGUOINHAN1 { get; set; }

        public int BCCP { get; set; }

        public string SO_THAM_CHIEU { get; set; }

        public int SO_TIEN_THU_HO { get; set; }

        public string TRACEDATE { get; set; }

        public string GHI_CHU { get; set; }

        public string DICH_VU { get; set; }

        public string STATUSNOTE { get; set; }

        public int STATUSPOST { get; set; }
    }
    public class Order
    {

        public String MAE1 { get; set; }

        public int NGAYDONG { get; set; }

        public int NGAYNHAN { get; set; }

        public int GIONHAN { get; set; }

        public string NGUOINHAN { get; set; }

        public string DIACHINHAN { get; set; }

        public int MABC { get; set; }

        public int KHOILUONG { get; set; }

        public string STATE { get; set; }
        public string STATE_NAME { get; set; }

        public string STATE_COD { get; set; }

        public int STATEINT { get; set; }

        public int STATECODINT { get; set; }

        public string MAKH { get; set; }

        public int MA_BC_KHAI_THAC { get; set; }

        public int CUOC_E1 { get; set; }

        public string DIEN_THOAI_NHAN { get; set; }

        public string NGUOINHAN1 { get; set; }

        public int BCCP { get; set; }

        public string SO_THAM_CHIEU { get; set; }

        public int SO_TIEN_THU_HO { get; set; }

        public string TRACEDATE { get; set; }

        public string GHI_CHU { get; set; }

        public string DICH_VU { get; set; }

        public string STATUSNOTE { get; set; }

        public int STATUSPOST { get; set; }
    }
    public class ReturnOrder
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public int Total_Success { get; set; }
        public double Total_Amount { get; set; }
        public double Total_Amount_Success { get; set; }
        public double Total_Freight { get; set; }

        public List<OrderMobile> ListOrder;
    }
    public class ReturnOrderByCode
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public Order Order { get; set; }
    }
    public class OrderRequest
    {
        public string code { get; set; }

        public int from_date { get; set; }

        public int to_date { get; set; }

        public string status { get; set; }

        public string customer_code { get; set; }

        public int page_size { get; set; }

        public int page_index { get; set; }
    }

    public class OrderCode
    {
        public string order_code { get; set; }

    }

}