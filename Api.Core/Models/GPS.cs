using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    public class GPS
    {
        public int id { get; set; }

        public string plate { get; set; }

        public int groupid { get; set; }

        public string journey { get; set; }

        public DateTime gpstime { get; set; }
    }
}