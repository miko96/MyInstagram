using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyInstagram.Data.Entities
{
    public class ArticleLike
    {
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
