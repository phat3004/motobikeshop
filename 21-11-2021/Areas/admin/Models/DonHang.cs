using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Areas.admin.Models
{
    public class DonHang
    {
        [Key]
        public int IdDonHang { get; set; }
        public Guid MaDonHang { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public int TrangThai { get; set; }
        public ICollection<ChiTietHoaDon> lstChiTietHoaDon { get; set; }
    }
}
