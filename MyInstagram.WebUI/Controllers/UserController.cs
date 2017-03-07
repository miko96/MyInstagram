using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyInstagram.Data.Entities;
using MyInstagram.Data.Infrastructure;
using MyInstagram.Service.Services;
using MyInstagram.WebUI.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        IUserService userService;
        IArticleService articleService;
        ApplicationUserManager userManager;

        public UserController(IUserProfileService userProfileService, IArticleService articleService,
            ApplicationUserManager userManager, IUserService userService)
        {
            this.userProfileService = userProfileService;
            this.userService = userService;
            this.articleService = articleService;
            this.userManager = userManager;
        }

        //???????????????????????????????
        public string FollowUnfollowUser(string toUserId)
        {
            string userId = User.Identity.GetUserId();
            var user = userManager.FindById(userId);
            var followingUser = userManager.FindById(toUserId);
            if (followingUser == null)
                return null;

            var isFollow = userService.isFollow(userId, toUserId);
            if (isFollow)
                user.Following.Remove(followingUser);
            else
                user.Following.Add(followingUser);
            userManager.Update(user);
            return followingUser.Followers.Count().ToString();
        }

        [ChildActionOnly]
        public PartialViewResult UserList()
        {
            ViewBag.ViewName = "All users";
            var userProfiles = userProfileService.GetProfiles().ToList();
            return PartialView("List", userProfiles);
        }

        public ActionResult FollowersList(string userId)
        {
            var userProfiles = userService.GetFollowers(userId)
                .Select(x => x.UserProfile).ToList();
            ViewBag.ViewName = "Followers";
            return View("List", userProfiles);
        }

        public ActionResult FollowingList(string userId)
        {
            var userProfiles = userService.GetFollowing(userId)
                .Select(x => x.UserProfile).ToList();
            ViewBag.ViewName = "Following";
            return View("List", userProfiles);
        }

        public ActionResult Page(string userName)
        {
            if (userName == null)
                userName = User.Identity.GetUserName();

            var user = userService.GetByName(userName);
            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var pageModel = new PageViewModel
            {
                UserId = user.Id,
                FirstName = user.UserProfile.FirstName,
                LastName = user.UserProfile.LastName,
                ArticlesCount = user.UserArticles.Count,
                FollowersCount = user.Followers.Count,
                FollowingCount = user.Following.Count
            };
            var currentUserId = User.Identity.GetUserId();
            //ViewBag.CurrentUserId = currentUserId;
            ViewBag.IsOwnPage = (user.Id == currentUserId);
            var isFollow = userService.isFollow(currentUserId, user.Id);
            ViewBag.buttonName = isFollow ? "Unfollow" : "Follow";

            return View(pageModel);
            //var currentUserId = User.Identity.GetUserId();
            //var isOwnPage = (user.Id == currentUserId);

            //bool isFollowing = false;
            //if (!isOwnPage)
            //{
            //    var currentUser = userManager.Users
            //        .Include(x => x.Following)
            //        .Where(x => x.Id == currentUserId).FirstOrDefault();
            //    isFollowing = currentUser.Following.Contains(user);
            //}


            //int articlesCount = userManager.Users.Where(x => x.UserName == userName).SelectMany(x => x.UserArticles).Count();
            //int followersCount = userManager.Users.Where(x => x.UserName == userName).SelectMany(x => x.Followers).Count();
            //int followingCount = userManager.Users.Where(x => x.UserName == userName).SelectMany(x => x.Following).Count();

            //var usModel = new UserViewModel()
            //{
            //    UserId = user.Id,
            //    CurrentUserId = currentUserId,
            //    FirstName = user.UserProfile.FirstName,
            //    LastName = user.UserProfile.LastName,
            //    IsAuthenticated = true,
            //    IsOwnPage = isOwnPage,
            //    IsFollowing = isFollowing,
            //    ArticlesCount = articlesCount,
            //    FollowersCount = followersCount,
            //    FollowingCount = followingCount

            //};

        }

        public ActionResult FindUser()
        {
            return View(new FindUserViewModel());
        }
        [HttpPost]
        public ActionResult FindUser(FindUserViewModel userData)
        {
            //return null;
            if (userData.FirstName == null
                && userData.LastName == null
                && userData.County == null
                && userData.Sex == null
                && userData.UserName == null)
                return PartialView("FindUserList", null);
            var profiles = userProfileService.GetProfiles();
            //    //var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (userData.UserName != null)
            {
                //var userProfile = userManager.Users
                //    .Where(x => x.UserName == userData.UserName)
                //    .Select(x => x.UserProfile).Include(x => x.ApplicationUser)
                //    .AsEnumerable();
                var userProfile = profiles
                    .Where(x => x.ApplicationUser.UserName == userData.UserName)
                    .ToList();
                if (userProfile.Count() != 0)
                    return PartialView("FindUserList", userProfile);
                return PartialView("FindUserList", null);
            }

            //    

            if (userData.FirstName != null)
                profiles = profiles.Where(x => x.FirstName == userData.FirstName);
            if (userData.LastName != null)
                profiles = profiles.Where(x => x.LastName == userData.LastName);
            if (userData.Sex != null)
                profiles = profiles.Where(x => x.Sex == userData.Sex);
            if (userData.County != null)
                profiles = profiles.Where(x => x.Country == userData.County);
            var profilesModel = profiles.Include(x => x.ApplicationUser).ToList();

            if (profilesModel.Count() != 0)
                return PartialView("FindUserList", profilesModel);
            return PartialView("FindUserList", null);
        }
    }
}
