using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyInstagram.Data.Infrastructure;
using MyInstagram.Data.Entities;

namespace MyInstagram.Data.Repository
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(DbContext context)
            : base(context) { }
        
            public Article GetById(int id)
        {
            return FindBy(x => x.ArticleID == id).FirstOrDefault();
        }
    }

    

    public interface IArticleRepository : IRepository<Article>
    {
        Article GetById(int id);
    }
}
