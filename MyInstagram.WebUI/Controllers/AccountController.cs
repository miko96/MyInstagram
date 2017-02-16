using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MyInstagram.WebUI.Models;
using MyInstagram.Data.Infrastructure;
using MyInstagram.Data.Entities;
using MyInstagram.Service.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;


namespace MyInstagram.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IUserProfileService userProfileService;

        public AccountController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        public ViewResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser() { UserName = model.UserName };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var userProfile = new UserProfile() { Id = user.Id };
                    userProfileService.Create(userProfile);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }            
            }
            return View(model);
        }

        public ViewResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ApplicationUser user = await UserManager.FindAsync(model.UserName, model.Password);
            if(user == null)
                ModelState.AddModelError("", "Неверный логин или пароль");
            else
            {
                ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true
                }, claim);
                if (String.IsNullOrEmpty(returnUrl))
                    return RedirectToAction("Index", "Home");
                return Redirect(returnUrl);
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }


        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            //return View("Login");
            return RedirectToAction("Login", "Account");
        }
    }
}