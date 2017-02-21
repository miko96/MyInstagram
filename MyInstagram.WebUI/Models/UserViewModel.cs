using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyInstagram.WebUI.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string CurrentUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsOwnPage { get; set; }
        public bool IsFollowing { get; set; }

        public int ArticlesCount { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }


    }
}