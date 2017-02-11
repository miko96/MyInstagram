using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyInstagram.Data.Entities
{
    public class UserArticle
    {
        [Key]
        [Column("UserId", Order = 0)]
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Key]
        [Column(Order = 1)]
        public int? ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
