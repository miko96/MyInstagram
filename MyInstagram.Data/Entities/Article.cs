using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInstagram.Data.Entities
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public DateTime DateCreated { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<ArticleLike> ArticleLikes { get; set; }
        public ICollection<ArticleComment> ArticleComments { get; set; }

        public Article()
        {
            DateCreated = DateTime.Now;
            ArticleLikes = new List<ArticleLike>();
            ArticleComments = new List<ArticleComment>();
        }
    }
}
