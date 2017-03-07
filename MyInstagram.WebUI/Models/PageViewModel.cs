using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyInstagram.WebUI.Models
{
    public class PageViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ArticlesCount { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
    }
}