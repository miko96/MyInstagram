using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MyInstagram.WebUI.Models;
using MyInstagram.Data.Infrastructure;
using MyInstagram.Service.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MyInstagram.WebUI.Infrastructure;
using AutoMapper;
using MyInstagram.Service.Models;

namespace MyInstagram.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IUserService userService;
        ApplicationUserManager userManager;

        public AccountController(IUserService userService, ApplicationUserManager userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [RedirectAuthenticatedRequests]
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
                Mapper.Initialize(x => x.CreateMap<RegisterViewModel, RegisterServiceModel>());
                var serviceModel = Mapper.Map<RegisterViewModel, RegisterServiceModel>(model);
                IdentityResult result = await userService.CreateUser(serviceModel);
                if (result.Succeeded)
                    return RedirectToAction("Login", "Account");//------------------------------------------
                else
                    foreach (string error in result.Errors)
                        ModelState.AddModelError("", error);      
            }
            return View(model);
        }

        [RedirectAuthenticatedRequests]
        public ViewResult Login(string returnUrl)
        {
            //ViewBag.returnUrl = returnUrl;
            return View();
        }

        public async Task<ActionResult> Test(string userName)
        {
            var result = await userService.DeleteUser(userName);
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(x => x.CreateMap<LoginViewModel, LoginServiceModel>());
                var seviceModel = Mapper.Map<LoginViewModel, LoginServiceModel>(model);
                var calim = await userService.Authenticate(seviceModel);
                if (calim == null) 
                    ModelState.AddModelError("", "Error login or password");
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, calim);
                    return RedirectToAction("Page", "User"); //------------------------------------------
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");//------------------------------------------
        }
    }
}