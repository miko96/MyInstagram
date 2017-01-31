using System.Collections.Generic;
using MyInstagram.Domain.Abstract;
using MyInstagram.Domain.Entities;

namespace MyInstagram.Domain.Concrete
{
    public class ArticleRepository : IArticleRepository
    {
        private MyInstagramEntities dataContext = new MyInstagramEntities();

        public IEnumerable<Article> Articles
        {
            get { return dataContext.Articles; }
        }

        public void SaveArticle(Article article)
        {
            if (article.ArticleID == 0)
                dataContext.Articles.Add(article);
            else
            {
                Article dbArticle = dataContext.Articles.Find(article.ArticleID);
                if(dbArticle != null)
                {
                    dbArticle.Description = article.Description;
                    dbArticle.ImageData = article.ImageData;
                    dbArticle.ImageMimeType = article.ImageMimeType;
                }
            }
            dataContext.SaveChanges();
        }
    }
}
