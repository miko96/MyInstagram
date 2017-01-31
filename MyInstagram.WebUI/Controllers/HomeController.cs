using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyInstagram.Domain.Entities;
using MyInstagram.Domain.Abstract;

namespace MyInstagram.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IArticleRepository repository;

        public HomeController(IArticleRepository repo)
        {
            repository = repo;
        }
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }
        
        public ViewResult Create()
        {
            return View("Edit", new Article());
        }

        [HttpPost]
        public ActionResult Create(Article article, HttpPostedFileBase image = null)
        {
            if (image != null)
            {
                article.ImageMimeType = image.ContentType;
                article.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(article.ImageData, 0, image.ContentLength);
            }
            repository.SaveArticle(article);
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Edit(Article article)
        {
            if(ModelState.IsValid)
            {
                repository.SaveArticle(article);
                return RedirectToAction("Index");
            }
            else
            {
                return View(article);
            }
        }
    }
}