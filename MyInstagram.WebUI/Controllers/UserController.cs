using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyInstagram.Data;
using MyInstagram.Data.Entities;
using MyInstagram.Data.Infrastructure;
using MyInstagram.Service.Services;
using MyInstagram.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MyInstagram.WebUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        //MyInstagramEntities my = new MyInstagramEntities();
        IUserProfileService userProfileService;
        public UserController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        public ActionResult List()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userProfiles = userManager.Users
                .Select(x => x.UserProfile).Include(x => x.AppUser).AsEnumerable();
            return View("List", userProfiles);
        }

        public ActionResult PartList()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userProfiles = userManager.Users
                .Select(x => x.UserProfile).Include(x => x.AppUser).AsEnumerable();
            return PartialView("List", userProfiles);
        }

        public ActionResult FollowersList(string userId)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var user = userManager.Users
            //    .Include(x => x.Followers)
            //    .Include(x=>x.UserProfile)
            //    .Where(x => x.Id == userId)
            //    .FirstOrDefault();
            var userProfiles = userManager.Users.Where(x => x.Id == userId)
               .SelectMany(x => x.Followers)
               .Select(x => x.UserProfile)
               .Include(x => x.AppUser)
               .AsEnumerable();
            return View("List", userProfiles);
        }

        public ActionResult FollowingList(string userId)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var user = userManager.Users
            //    .Include(x => x.Following)
            //    .Include(x => x.UserProfile)
            //    .Where(x => x.Id == userId)
            //    .FirstOrDefault();
            var userProfiles = userManager.Users.Where(x => x.Id == userId)
                .SelectMany(x => x.Following)
                .Select(x => x.UserProfile)
                .Include(x => x.AppUser)
                .AsEnumerable();
            return View("List", userProfiles);
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

        public ActionResult FindUser()
        {
            return View(new FindUserViewModel());
        }
        [HttpPost]
        public ActionResult FindUser(FindUserViewModel userData)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (userData.UserName != null)
            {
                var user = userManager.Users
                    .Include(x => x.UserProfile)
                    .Where(x => x.UserName == userData.UserName).FirstOrDefault();
                if (user != null)
                    return PartialView("FindUserList");
            }
            var profiles = userProfileService.GetProfiles();

            if (userData.FirstName != null)
                profiles = profiles.Where(x => x.FirstName == userData.FirstName);
            if (userData.LastName != null)
                profiles = profiles.Where(x => x.LastName == userData.LastName);
            if (userData.Sex != null)
                profiles = profiles.Where(x => x.Sex == userData.Sex);
            if (userData.County != null)
                profiles = profiles.Where(x => x.Country == userData.County);
            var profilesModel = profiles.Include(x => x.AppUser).AsEnumerable();

            if (profilesModel.Count() != 0)
                return PartialView("FindUserList", profilesModel);
            //var userProfiles = userProfileService.FindBy(x=> x.

            return null;
        }


        public FileContentResult GetPageImage(string Id)
        {
            UserProfile userProfile = userProfileService.GetById(Id);
            if (userProfile != null)
            {
                if(userProfile.ImageData != null)
                return File(userProfile.ImageData, userProfile.ImageMimeType);
                return null;
            }
            else
            {
                return null;
            }
        }


        public ActionResult Test()
        {
            return View();
        }
    }
}