using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyInstagram.Service.Services;
using MyInstagram.Data;
using MyInstagram.Data.Entities;
using Microsoft.AspNet.Identity;

namespace MyInstagram.WebUI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        IUserProfileService userProfileService;
        //MyInstagramEntities my = new MyInstagramEntities();
        public ProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }


        public ActionResult Edit()
        {
            var userProfile =  userProfileService.GetById(User.Identity.GetUserId());
            if (userProfile == null)
                return HttpNotFound();
            return View(userProfile);
        }
        [HttpPost]
        public ActionResult Edit(UserProfile userProfile, HttpPostedFileBase profileImage = null)
        {
            if (!ModelState.IsValid)
                return View(userProfile);
            
            if (profileImage != null)
            {
                userProfile.ImageMimeType = profileImage.ContentType;
                userProfile.ImageData = new byte[profileImage.ContentLength];
                profileImage.InputStream.Read(userProfile.ImageData, 0, profileImage.ContentLength);
                userProfileService.Update(userProfile);
            }
            else
            {
                userProfileService.UpdateProperties(userProfile,
                    x => x.FirstName, x => x.LastName, x => x.Country, x => x.Sex);
            }
            return RedirectToAction("Page", "User");
        }
    }
}