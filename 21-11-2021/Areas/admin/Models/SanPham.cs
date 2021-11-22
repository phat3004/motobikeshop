using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Areas.admin.Models
{
    public class SanPham
    {
        [Key]
        public int MaSanPham { get; set; }
        public string HinhAnh { get; set; }
        public string TenSanPham { get; set; }
        public decimal Gia { get; set; }
        public decimal GiaKhuyenMai { get; set; }
        public decimal GiaDaKhuyenMai { get; set; }
        public string ChiTiet { get; set; }
        public int MaKhuyenMai { get; set; }
        [ForeignKey("MaLoaiSanPham")]
        public virtual KhuyenMai KhuyenMai { get; set; }
        public int MaLoaiSanPham { get; set; }
        [ForeignKey("MaLoaiSanPham")]
        public virtual LoaiSanPham Loai { get; set; }
        public string HangMuc { get; set; }
        public bool TrangThai { get; set; }
        public ICollection<ChiTietHoaDon> chitiethoadon { get; set; }
        public ICollection<DanhGia> danhgia { get; set; }
    }
}
