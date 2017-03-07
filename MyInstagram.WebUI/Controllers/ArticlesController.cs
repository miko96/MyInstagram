using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyInstagram.Data.Entities;
using MyInstagram.Service.Services;
using Microsoft.AspNet.Identity;
using MyInstagram.WebUI.Models;

namespace MyInstagram.WebUI.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        IArticleService articleService;
        IUserService userService;
        int blockSize = 1;
        //MyInstagramEntities my = new MyInstagramEntities();
        public ArticlesController(IArticleService articleService, IUserService userService)
        {
            this.articleService = articleService;
            this.userService = userService;
        }

        public ActionResult Index([System.Web.Http.FromBody] int count = 0)
        {
            string userId = User.Identity.GetUserId();
            var followingUsersId = userService.GetFollowing(userId).Select(x=>x.Id).ToList();
            var articles = articleService.FindBy(x => followingUsersId.Contains(x.ApplicationUserId))
                .OrderByDescending(x=>x.DateCreated);

            if(Request.IsAjaxRequest())
            {
                var articlesCount = articles.Count();
                if (count < articlesCount)
                {
                    int numberOfBlock = count / blockSize;
                    var articlesBlock = articles.Skip(numberOfBlock * blockSize)
                        .Take(blockSize).ToList();
                    return PartialView("ArticlesBlock", articlesBlock);
                }
                return null;
            }
            var arts = articles.Take(blockSize).ToList();
            return View(arts);
        }       

        //--------------------------------------------------------------------------//
        [ChildActionOnly]
        public PartialViewResult UserArticles(string userId)
        {
            var articles = articleService.FindBy(x => x.ApplicationUserId == userId)
                .Select(x => new ArticleViewModel
                {
                    ArticleId = x.ArticleId,
                    Description = x.Description,
                    ApplicationUserId = x.ApplicationUserId
                })
                .AsEnumerable();

            if (articles.Count() == 0)
                return PartialView(null);
            return PartialView(articles);
        }

        [ChildActionOnly]
        public PartialViewResult DeletePhotoButton(string userId, int articleId)
        {
            string currentUserId = User.Identity.GetUserId();
            if (userId == currentUserId)
                return PartialView(articleId);
            return null;
        }


        public FileContentResult GetArticleImage(int articleId)
        {
            Article article = articleService.GetById(articleId);
            if (article == null)
                return null;
            return File(article.ImageData, article.ImageMimeType);            
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleViewModel model, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid == false)
                return View(model);
            if (image == null)
            {
                ModelState.AddModelError("", "Add image, please");
                return View(model);
            }
            
            var article = new Article
            {
                Description = model.Description,
                ImageData = new byte[image.ContentLength],
                ImageMimeType = image.ContentType,
                ApplicationUserId = User.Identity.GetUserId()
            };
            image.InputStream.Read(article.ImageData, 0, image.ContentLength);
            articleService.Create(article);
            return RedirectToAction("Page", "User"); // ----------------------------------------------------            
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int id)
        {
            var article = articleService.GetById(id);
            if (article == null)
                return null;
            if (article.ApplicationUserId == User.Identity.GetUserId())
                articleService.Delete(article);
            return null;
        }
    }
}
