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
        IUserProfileService userProfileService;

        public ProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        public ActionResult Edit()
        {
            var userProfile = userProfileService.GetById(User.Identity.GetUserId());
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

        public FileContentResult GetProfileImage(string Id)
        {
            UserProfile userProfile = userProfileService.GetById(Id);
            if (userProfile == null)
                return null;
            if (userProfile.ImageData != null)
                return File(userProfile.ImageData, userProfile.ImageMimeType);
            else
            {
                string imgPath = Server.MapPath("~/ProfileImage/profileimage.png");
                var imgBytes = System.IO.File.ReadAllBytes(imgPath);
                string contentType = "image/png";
                return File(imgBytes, contentType);
            }
        }
    }
}