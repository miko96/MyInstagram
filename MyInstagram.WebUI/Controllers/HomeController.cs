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

namespace MyInstagram.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public string Index(string username)
        {
            if (username == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    username = User.Identity.GetUserName();
                }
                else return "error";
            }
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.Where(x => x.UserName == username).FirstOrDefault();
            var a = user.UserArticles;
            return "asdasd";
            //return user != null ?  a.Article.Description : "asdasd";
        }
    }
}