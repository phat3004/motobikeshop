using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Areas.admin.Models
{
    public class LienHe
    {
        [Key]
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set;}
        public string SDT { get; set; }
        public string ChiTiet { get; set; }
        public DateTime NgayLienHe { get; set; }
    }
}
