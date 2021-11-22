using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace _21_11_2021.Areas.admin.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int MaChiTietHoaDon { get; set; }
        public int MaSanPham { get; set; }
        [ForeignKey("MaSanPham")]
        public virtual SanPham MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
        public decimal ChietKhau { get; set; }
        public decimal ThanhTien { get; set; }
        public string MaKhachHang { get; set; }
        [ForeignKey("MaKhachHang")]
        public virtual User User { get; set; }
        public DateTime NgayDatHang { get; set; }
        public int IdDonHang { get; set; }
        [ForeignKey("IdDonHang")]
        public virtual DonHang DonHang { get; set; }

    }
}
