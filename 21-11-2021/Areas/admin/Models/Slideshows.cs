using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Areas.admin.Models
{
    public class Slideshows
    {
        [Key]
        public int IdSlideShow { get; set; }
        public string Hinh { get; set; }
        public bool TrangThai { get; set; }
        public int NoiBat { get; set; }
    }
}
