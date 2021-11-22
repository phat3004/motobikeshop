using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Models
{
    public class UserRegistrationModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Họ tên không được bỏ trống")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Họ tên ít nhất 10 ký tự")]
        public string HoTen { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email không được trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [DataType(DataType.Password, ErrorMessage = "Mật khẩu cần kí tự đặc biệt chữ hoa và thường")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "số điện thoại không hợp lệ")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được trống")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Nam hoặc nữ")]
        [EnumDataType(typeof(GioiTinh))]
        public GioiTinh GioiTinh { get; set; }
        [DataType(DataType.Password, ErrorMessage = "Mật khẩu cần kí tự đặc biệt chữ hoa và thường")]
        [Compare("MatKhau", ErrorMessage = "xác nhận mật khẩu chưa khớp.")]
        public string XacNhanMK { get; set; }
    }
    public enum GioiTinh
    {
        Nam = 1,
        Nữ = 2,
        Khác = 3
    }
}
