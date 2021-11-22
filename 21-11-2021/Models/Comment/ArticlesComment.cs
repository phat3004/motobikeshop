using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Models
{
    public class ArticlesComment
    {
        [Key]
        public int Id { get; set; }

        public string Comments { get; set; }

        public DateTime PublishedDate { get; set; }

        public int ArticlesId { get; set; }
        public Article Articles { get; set; }

        public int Rating { get; set; }
    }
}
