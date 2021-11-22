using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Areas.admin.Models
{
    public class TinTuc
    {
        [Key]
        public int MaTinTuc { get; set; }

        [Required(ErrorMessage = "Tên tin tức không được để trống")]
        public string TenTinTuc { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string MoTa { get; set; }

        public string Hinh { get; set; }

        [Required(ErrorMessage = "Chi tiết không được để trống")]
        public string ChiTiet { get; set; }

        [Required(ErrorMessage = "Chọn nổi bật")]
        public int NoiBat { get; set; }
        public bool TrangThai { get; set; }
    }
}
