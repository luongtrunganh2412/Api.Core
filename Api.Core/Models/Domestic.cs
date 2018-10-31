using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    public class Domestic
    {
        public string function { get; set; }
        public string Country { get; set; }

        public int FromProvince { get; set; }

        public int ToProvince { get; set; }

        public int Weight { get; set; }
        public int Species { get; set; }
    }    
}