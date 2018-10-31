using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core.Models
{
    public class Account
    {
        public int Id { get; set; }

        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Mật Khẩu
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public string BirthDay { get; set; }

        /// <summary>
        /// Số giấy tờ tùy thân
        /// </summary>
        public string PIdNumber { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Tỉnh
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// Huyện
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// Xã
        /// </summary>
        public int WardId { get; set; }

        /// <summary>
        /// Thôn/Xóm
        /// </summary>
        public int HamletId { get; set; }

        /// <summary>
        /// Đường
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Cửa hàng/Đại lý
        /// </summary>
        public int CustemerId { get; set; }

        /// <summary>
        /// Cấp
        /// </summary>
        public string AccountLevel { get; set; }
        /// <summary>
        /// Nhóm Người Dùng
        /// </summary>
        public string AccoutGroupId { get; set; }
        /// <summary>
        /// Khóa/Không khóa
        /// </summary>
        public string IsLock { get; set; }

        /// <summary>
        /// Địa chỉ Tỉnh
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Địa chỉ Huyện
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Địa chỉ Xã
        /// </summary>
        public string WardName { get; set; }

        /// <summary>
        /// Địa chỉ Tổ/Thôn/Xóm
        /// </summary>
        public string HamletName { get; set; }
    }
}