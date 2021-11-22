using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Areas.admin.Models
{
    public class User : IdentityUser
    {
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string GioiTinh { get; set; }
        public string Hang { get; set; }

        public ICollection<ChiTietHoaDon> lstChiTietHoaDon { get; set; }
    }
}
