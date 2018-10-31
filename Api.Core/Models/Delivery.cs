using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    public class Delivery
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public int DateInt { get; set; }
        public int TimeInt { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string  Note { get; set; }
        public string Cause { get; set; }
        public string Solution { get; set; }

        public string  ReceiverName { get; set; }
    }
}