using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyInstagram.Service.Services;

namespace MyInstagram.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        IUserProfileService userProfileService;

        public ActionResult Index()
        {
            return View();
        }
    }
}