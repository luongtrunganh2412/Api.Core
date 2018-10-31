using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Id nhân viên
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Id nhóm người dùng
        /// </summary>
        public int EmpGroupId { get; set; }

        /// <summary>
        /// Id đại lý
        /// </summary>
        public int AgentId { get; set; }

        /// <summary>
        /// Khóa người dùng
        /// </summary>
        public string IsLock { get; set; }
    }
}