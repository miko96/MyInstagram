using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyInstagram.Data.Infrastructure;
using MyInstagram.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyInstagram.WebUI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult List()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var users = userManager.Users.AsEnumerable();
            return View(users);
        }

        public ActionResult FollowersList(string userId)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.Include(x => x.Followers).Where(x => x.Id == userId).FirstOrDefault();
            return View("List", user.Followers);
        }

        public ActionResult FollowingList(string userId)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.Include(x => x.Following).Where(x => x.Id == userId).FirstOrDefault();
            return View("List", user.Following);
        }

        public ActionResult Page(string userName)
        {
            if (userName == null)  
                    userName = User.Identity.GetUserName();

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users
                .Include(x => x.UserProfile)
                .Where(x => x.UserName == userName).FirstOrDefault();

            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var currentUserId = User.Identity.GetUserId();
            var isOwnPage = (user.Id == currentUserId);

            bool isFollowing = false;
            if (!isOwnPage)
            {
                var currentUser = userManager.Users
                    .Include(x => x.Following)
                    .Where(x => x.Id == currentUserId).FirstOrDefault();
                isFollowing = currentUser.Following.Contains(user);
            }


            int articlesCount = userManager.Users.Where(x => x.UserName == userName).SelectMany(x => x.UserArticles).Count();
            int followersCount = userManager.Users.Where(x => x.UserName == userName).SelectMany(x => x.Followers).Count();
            int followingCount = userManager.Users.Where(x => x.UserName == userName).SelectMany(x => x.Following).Count();

            var usModel = new UserViewModel()
            {
                UserId = user.Id,
                CurrentUserId = currentUserId,
                FirstName = user.UserProfile.FirstName,
                LastName = user.UserProfile.LastName,
                IsAuthenticated = true,
                IsOwnPage = isOwnPage,
                IsFollowing = isFollowing,
                ArticlesCount = articlesCount,
                FollowersCount = followersCount,
                FollowingCount = followingCount

            };
            return View(usModel);
        }



        public ActionResult Test()
        {
            return View();
        }
    }
}