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
        public int ArticleID { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public DateTime DateCreated { get; set; }

        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }


        public ICollection<ArticleLike> ArticleLikes { get; set; }
        public ICollection<ArticleComment> ArticleComments { get; set; }

        public Article()
        {
            DateCreated = DateTime.Now;
            
        }
    }
}
