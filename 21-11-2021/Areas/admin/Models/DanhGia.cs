using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Areas.admin.Models
{
    public class DanhGia
    {
        [Key]
        public int MaDanhGia { get; set; }
        public int MaSanPham { get; set; }
        [ForeignKey("MaSanPham")]
        public virtual SanPham SanPham { get; set; }
        public string ChiTiet { get; set; }
        public DateTime NgayDanhGia { get; set; }
        public string MaKhachHang { get; set; }
        [ForeignKey("MaKhachHang")]
        public virtual User User{ get; set; }
        public bool TrangThai { get; set; }

    }
}
