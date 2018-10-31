using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    /// <summary>
    /// Tỉnh/Thành phố
    /// </summary>
    public class Province
    {
           

        /// <summary>        
        /// </summary>
        public int ProvinceCode { get; set; }

        /// <summary>
        
        /// </summary>
        [Required]
        public string ProvinceName { get; set; }

        /// <summary>
        
        /// </summary>
        [Required]
        public string Description { get; set; }

        public int ProvinceCode2 { get; set; }
    }
}