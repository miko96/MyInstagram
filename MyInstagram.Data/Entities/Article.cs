using System;
using System.Collections.Generic;

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
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<ArticleLike> ArticleLikes { get; set; }
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }

        public Article()
        {
            DateCreated = DateTime.Now;
            ArticleLikes = new List<ArticleLike>();
            ArticleComments = new List<ArticleComment>();
        }
    }
}
