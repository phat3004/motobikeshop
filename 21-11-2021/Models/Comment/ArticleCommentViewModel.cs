using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Models
{
    public class ArticleCommentViewModel
    {
        public string Title { get; set; }
        public List<ArticlesComment> ListOfComments { get; set; }
        public string Comment { get; set; }
        public int ArticleId { get; set; }
        public int Rating { get; set; }
    }
}
