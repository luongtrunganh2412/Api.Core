using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    /// <summary>
    /// Xã/Phường
    /// </summary>
    public class Ward
    {

        public string CommuneCode { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
      
        public string CommuneName { get; set; }

        /// <summary>
        /// Tên xã
        /// </summary>
    
        public string DistrictCode { get; set; }

     
    }
}