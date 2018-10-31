using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    /// <summary>
    /// Thôn/Xóm
    /// </summary>
    public class Hamlet
    {
        /// <summary>
        /// Id thôn
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Mã thôn
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Tên thôn
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Id Xã
        /// </summary>
        [Required]
        public int WardId { get; set; }
    }
}