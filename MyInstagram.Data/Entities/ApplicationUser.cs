using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace MyInstagram.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateCreated { get; set; }

        public UserProfile UserProfile { get; set; }

        public ICollection<Article> UserArticles { get; set; }

        public ICollection<ApplicationUser> Followers { get; set; }
        public ICollection<ApplicationUser> Following { get; set; }

        public ICollection<ArticleComment> UserComments { get; set; }
        public ICollection<ArticleLike> UserLikes { get; set; }
        
        public ApplicationUser()
        {
            DateCreated = DateTime.Now;
            UserArticles = new List<Article>();
            Followers = new List<ApplicationUser>();
            Following = new List<ApplicationUser>();          
        }


    }
}
