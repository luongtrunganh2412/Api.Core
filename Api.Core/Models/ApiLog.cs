using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class ApiLog
    {
        public string CUSTOMER_CODE { get; set; }
        public string ERROR_CODE { get; set; }
        public string ERROR_MESSAGE { get; set; }
        public string SERVICE_NAME { get; set; }
        public string TRACKING_CODE { get; set; }
        public string API_KEY { get; set; }
        public int COUNT { get; set; }
        public string CLASS { get; set; }
        public string FUNCTION_NAME { get; set; }

        public string STATUS { get; set; }

    }
}