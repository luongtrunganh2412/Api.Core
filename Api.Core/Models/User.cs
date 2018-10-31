using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    /// <summary>
    /// Thông tin tài khoản
    /// </summary>
    public class User
    {       
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        [Required]
        public string USER_NAME { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required]
        public string PASS_WORD { get; set; }
    }
}