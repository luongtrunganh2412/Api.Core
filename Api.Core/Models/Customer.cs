using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    /// <summary>
    /// Khách hàng
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Loại
        /// </summary>
        public int CustomerType { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ Email
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Id Tỉnh/Thành phố
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// Tên Tỉnh/Thành phố
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Id Quận/Huyện
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// Tên Quận/Huyện
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Id Xã/Phường
        /// </summary>
        public int WardId { get; set; }

        /// <summary>
        /// Tên Xã/Phường
        /// </summary>
        public string WardName { get; set; }

        /// <summary>
        /// Id Thôn/Xóm
        /// </summary>
        public int HamletId { get; set; }

        /// <summary>
        /// Tên Thôn/Xóm
        /// </summary>
        public string HamletName { get; set; }
        
        /// <summary>
        /// Địa chỉ chi tiết
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Khóa sử dụng
        /// </summary>
        public string IsLock { get; set; }
    }
}