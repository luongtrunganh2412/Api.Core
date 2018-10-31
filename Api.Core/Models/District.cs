using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    /// <summary>
    /// Quận/Huyện
    /// </summary>
    public class District
    {
      

        /// <summary>
        /// Mã huyện
        /// </summary>
        public string DistrictCode { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
        [Required]
        public string ProvinceCode { get; set; }

        public string ProvinceName { get; set; }

        /// <summary>
        /// Tên tỉnh
        /// </summary>
        [Required]
        public string DistrictName { get; set; }

        /// <summary>
        /// Nội thành
        /// </summary>
        public string Description { get; set; }

       
    }
}