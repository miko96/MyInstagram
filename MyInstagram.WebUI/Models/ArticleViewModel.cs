using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyInstagram.WebUI.Models
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Description { get; set; }

        public string ApplicationUserId { get; set; }
    }
}