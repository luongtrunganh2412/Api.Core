using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    public class Transfer_Info
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Trend { get; set; }
        public string Receive_Po { get; set; }
        public int trip { get; set; }
        public int bag { get; set; }
    }
}