using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace MyInstagram.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateCreated { get; set; }             
        public ICollection<ApplicationUser> Followers { get; set; }
        public ICollection<ApplicationUser> Following { get; set; }
        //public ICollection<Article> FavoriteArticles { get; set; }
        public ICollection<Article> UserArticles { get; set; }
        public ICollection<ArticleComment> UsersComments { get; set; }
        public ICollection<ArticleLike> FavoriteArticles { get; set; }
        public UserProfile UserProfile { get; set; }


        public ApplicationUser()
        {
            DateCreated = DateTime.Now;
            UserArticles = new List<Article>();
            Followers = new List<ApplicationUser>();
            Following = new List<ApplicationUser>();
            //FavoriteArticles = new List<Article>();
            UsersComments = new List<ArticleComment>();
        }


    }
}
