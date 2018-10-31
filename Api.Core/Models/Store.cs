using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    public class Store
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        public int AmndUser { get; set; }
        /// <summary>
        /// Mã Kho
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Khách hàng
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Địa chỉ Tỉnh
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// Địa chỉ Huyện
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// Địa chỉ Xã
        /// </summary>
        public int WardId { get; set; }

        /// <summary>
        /// Địa chỉ Thôn/Xóm
        /// </summary>
        public int HamletId { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Người liên hệ-Tên
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Người liên hệ-Mobile
        /// </summary>
        public string ContactMobile { get; set; }

        /// <summary>
        /// Người liên hệ-Email
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Khóa/Không
        /// </summary>
        public string IsLock { get; set; }

        /// <summary>
        /// Địa chỉ-Tỉnh
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Địa chỉ-Huyện
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Địa chỉ-Xã
        /// </summary>
        public string WardName { get; set; }

        /// <summary>
        /// Địa chỉ-Thôn
        /// </summary>
        public string HamletName { get; set; }
    }
}