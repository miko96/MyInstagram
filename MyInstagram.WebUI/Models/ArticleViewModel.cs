using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyInstagram.WebUI.Models
{
    public class ArticleViewModel
    {
        public int ArticleID { get; set; }
        public string Description { get; set; }
        
        public string ArticleUserId { get; set; }

    }
}