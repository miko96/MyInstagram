using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace MyInstagram.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateCreated { get; set; }

        public UserProfile UserProfile { get; set; }

        public ICollection<UserArticle> UserArticles { get; set; }
        public ICollection<ArticleComment> UserComments { get; set; }
        public ICollection<ArticleLike> UserLikes { get; set; }
        
        public ApplicationUser()
        {
            DateCreated = DateTime.Now;
            //UserArticles = new List<UserArticle>();
           
        }
    }
}
