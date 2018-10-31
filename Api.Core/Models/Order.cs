using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
   
    public class ReturnOrderByCode
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public Order Order { get; set; }
    }
    public class OrderCode
    {
        public string order_code { get; set; }

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
}