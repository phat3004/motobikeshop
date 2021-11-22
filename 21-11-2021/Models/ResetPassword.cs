using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Models
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Mật khẩu cần kí tự đặc biệt chữ hoa và thường")]
        [Compare("MatKhau", ErrorMessage = "mật khẩu chưa khớp.")]
        public string XacNhanMK { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
