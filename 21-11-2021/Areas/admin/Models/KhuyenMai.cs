using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Areas.admin.Models
{
    public class KhuyenMai
    {
        [Key]
        public int MaKhuyenMai { get;set; }
        public string TenKhuyenMai { get; set; }
        public decimal SoTienGiam { get; set; }
        public bool TrangThai { get; set; }
        public ICollection<SanPham> lstSanPham { get; set; }
    }
}
