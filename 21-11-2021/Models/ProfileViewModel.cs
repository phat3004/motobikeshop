using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using _21_11_2021.Areas.admin.Models;

namespace _21_11_2021.Models
{
    public class ProfileViewModel 
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Họ tên không được bỏ trống")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Họ tên ít nhất 10 ký tự")]
        public string HoTen { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email không được trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "số điện thoại không hợp lệ")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được trống")]
        public string DiaChi { get; set; }

    }
}
