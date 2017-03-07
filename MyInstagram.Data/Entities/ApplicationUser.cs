using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace MyInstagram.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateCreated { get; set; }             
        public virtual ICollection<ApplicationUser> Followers { get; set; }
        public virtual ICollection<ApplicationUser> Following { get; set; }
        public virtual ICollection<Article> UserArticles { get; set; }
        public virtual ICollection<ArticleComment> UsersComments { get; set; }
        public virtual ICollection<ArticleLike> FavoriteArticles { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public ApplicationUser()
        {
            DateCreated = DateTime.Now;
            Followers = new List<ApplicationUser>();
            Following = new List<ApplicationUser>();
            UserArticles = new List<Article>();           
            UsersComments = new List<ArticleComment>();
            FavoriteArticles = new List<ArticleLike>();
        }
    }
}
