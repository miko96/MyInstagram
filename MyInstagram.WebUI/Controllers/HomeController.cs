using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyInstagram.Data.Entities;
using MyInstagram.Data.Repository;
using MyInstagram.Service.Services;
using Microsoft.AspNet.Identity.Owin;
using MyInstagram.Data.Infrastructure;
using Microsoft.AspNet.Identity;
using MyInstagram.Data;
using System.Data.Entity;

namespace MyInstagram.WebUI.Controllers
{
    public class HomeController : Controller
    {
        MyInstagramEntities my = new MyInstagramEntities();
        IArticleService articleService;
        public HomeController(IArticleService articleService)
        {
            this.articleService = articleService;
        }
        public ActionResult Index(string username)
        {
            if (username == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    username = User.Identity.GetUserName();
                }
            }
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var user = userManager.Users
                //.Include(x => x.UserArticles)
                .Include(x => x.UserProfile).Where(x => x.UserName == username).FirstOrDefault();
            var us = userManager.Users.Select(x => x.Id);
            var a = user.UserArticles.AsEnumerable();


            return View(user);
            //return user != null ?  a.Article.Description : "asdasd";
        }


        public string Index2()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.Include(c=> c.Following).Where(x => x.UserName == "User1").FirstOrDefault();
            //var user1 = userManager.Users.Where(x => x.UserName == "User1").FirstOrDefault();
            //user.Following.Add(user1);
            //userManager.Update(user);
            //userManager.Update(user1);
            var Followers = user.Following.Select(x=>x.Id);
            //var articles = from art in my.Articles
            //               from us in my.Users where us.UserName =="miko"
            //               from fl in us.Followers
            //               where art.applicationUserId == fl.Id
            //               select art;
            var aritcle = my.Articles.Where(x => Followers.Contains(x.applicationUserId)).FirstOrDefault();
            var art = my.Articles;
            //var user1 = from us in my.Users
            //           where us.UserName == "miko"
            //           select us;

            return "";
        }
    }
}