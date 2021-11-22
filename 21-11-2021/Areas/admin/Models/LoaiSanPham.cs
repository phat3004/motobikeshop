using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Areas.admin.Models
{
    public class LoaiSanPham
    {
        [Key]
        public int MaLoaiSanPham { get; set; }
        
        public string TenLoaiSanPham { get; set; }
        public int MaDanhMuc { get; set; }
        [ForeignKey("MaDanhMuc")]
        public virtual DanhMuc DanhMuc { get; set; }
        public bool TrangThai { get; set; }

        public ICollection<SanPham> lstSanPham { get; set; }

    }
}
