﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInstagram.Domain.Entities
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<UserArticle> UserArticles { get; set; }
        public ICollection<ArticleLike> ArticleLikes { get; set; }
        public ICollection<ArticleComment> ArticleComments { get; set; }

        public Article()
        {
            DateCreated = DateTime.Now;
            //UserArticles = new List<UserArticle>();
        }
    }
}
