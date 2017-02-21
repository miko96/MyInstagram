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
    public class ArticlesController : Controller
    {
        IArticleService articleService;
        //MyInstagramEntities my = new MyInstagramEntities();
        public ArticlesController(IArticleService articleService )
        {
            this.articleService = articleService;
        }

        // GET: Articles
        //public async Task<ActionResult> Index()
        //{
        //    IEnumerable<Article> articles = await my.Articles.ToListAsync();
        //    IEnumerable<Article> art = new List<Article>();
        //    return View(ar);
        //}
        public ActionResult Index()
        {
           
            return View();
        }

        public PartialViewResult Photos(string userId)
        {
            var articles = articleService.FindBy(x => x.applicationUserId == userId).Select(x =>new Article { ArticleID = x.ArticleID, Description = x.Description }).AsEnumerable();
            return PartialView("Photos", articles);
        }


        public PartialViewResult FollowingPhotos()
        {
            string userId = User.Identity.GetUserId();
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.Include(x => x.Following).Where(x => x.Id == userId).FirstOrDefault();
            var following = user.Following.Select(x => x.Id);

            var articles = articleService.FindBy(x => following.Contains(x.applicationUserId));
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

        //// GET: Articles/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Article article = db.Articles.Find(id);
        //    if (article == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(article);
        //}

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
                //article.UserArticles.Add()               
                
                //sarticleService.Create(article);
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = userManager.FindById(User.Identity.GetUserId());
                user.UserArticles.Add(article);

                userManager.Update(user);


                return RedirectToAction("Page","User");
            }

            return View(article);
        }

        //// GET: Articles/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Article article = db.Articles.Find(id);
        //    if (article == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(article);
        //}

        //// POST: Articles/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ArticleID,Description,ImageData,ImageMimeType,DateCreated")] Article article)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(article).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(article);
        //}

        //// GET: Articles/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Article article = db.Articles.Find(id);
        //    if (article == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(article);
        //}

        //// POST: Articles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Article article = db.Articles.Find(id);
        //    db.Articles.Remove(article);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
