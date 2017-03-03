using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyInstagram.Data;
using MyInstagram.Data.Entities;
using MyInstagram.Service.Services;
using MyInstagram.WebUI.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNet.Identity.Owin;
using MyInstagram.Data.Infrastructure;

namespace MyInstagram.WebUI.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        IArticleService articleService;
        int blockSize = 4;
        ///MyInstagramEntities my = new MyInstagramEntities();
        public ArticlesController(IArticleService articleService )
        {
            this.articleService = articleService;
        }

      
   
        public ActionResult Index([System.Web.Http.FromBody] int count = 0)
        {
            string userId = User.Identity.GetUserId();
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager
                .Users
                .Include(x => x.Following)
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var following = user.Following.Select(x => x.Id);

            if (Request.IsAjaxRequest())
            {
                var articlesCount = articleService
                    .FindBy(x => following.Contains(x.ApplicationUserId)).Count();
                if (count < articlesCount)
                {
                    int numberOfBlock = count / blockSize;
                   
                    var articlesBlock = articleService
                                .FindBy(x => following.Contains(x.ApplicationUserId))
                                .OrderBy(x => x.DateCreated)
                                .Reverse()
                                .Skip(numberOfBlock * blockSize)
                                .Take(blockSize);
                    return PartialView("ArticlesBlock", articlesBlock);
                }
                return null;
            }
            var articles = articleService
                .FindBy(x => following.Contains(x.ApplicationUserId))
                .OrderBy(x=>x.DateCreated)
                .Reverse()
                .Take(blockSize);
            
            return View(articles);
        }


        public PartialViewResult Photos(string userId)
        {
            var artCount = articleService.FindBy(x => x.ApplicationUserId == userId).Count();

            if (artCount == 0)
                return PartialView("Photos", null);

            var articles = articleService.FindBy(x => x.ApplicationUserId == userId)
                .Select(x =>new Article { ArticleId = x.ArticleId, Description = x.Description, ApplicationUserId = x.ApplicationUserId })
                .Reverse()
                .AsEnumerable();
            return PartialView("Photos", articles);
        }

        public PartialViewResult DeletePhotoButton(string userId, int articleId)
        {
            string currentUserId = User.Identity.GetUserId();
            if (userId == currentUserId)
                return PartialView(articleId);
            return null;
        }


        public PartialViewResult FollowingPhotos()
        {
            string userId = User.Identity.GetUserId();
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.Include(x => x.Following).Where(x => x.Id == userId).FirstOrDefault();
            var following = user.Following.Select(x => x.Id);

            var articles = articleService.FindBy(x => following.Contains(x.ApplicationUserId));
            return PartialView("FollowingPhotos", articles);
        }



        public FileContentResult GetImage(int articleId) {
            Article article = articleService.GetById(articleId);
            if (article != null)
            {
                return File(article.ImageData, article.ImageMimeType);
            }
            else
            {                
                return null;
            }
        }


        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    article.ImageMimeType = image.ContentType;
                    article.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(article.ImageData, 0, image.ContentLength);
                }
                else
                {
                    ModelState.AddModelError("", "Add image, please");
                    return View(article);
                }
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = userManager.FindById(User.Identity.GetUserId());
                user.UserArticles.Add(article);

                userManager.Update(user);
                return RedirectToAction("Page","User");
            }

            return View(article);
        }





        // GET: Articles/Delete/5
        public ActionResult Delete(int id)
        {

            var article = articleService.GetById(id);
            if (article != null)
            {
                if (article.ApplicationUserId == User.Identity.GetUserId())
                {
                    articleService.Delete(article);
                }
            }
            return null;
        }
    }
}
