using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyInstagram.Data.Infrastructure;
using MyInstagram.Data.Entities;
using System.Linq.Expressions;

namespace MyInstagram.Data.Repository
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(DbContext context)
            : base(context) { }

        public override IEnumerable<Article> FindBy(Expression<Func<Article, bool>> predicate)
        {
            return dbset.Include(x => x.applicationUser).Where(predicate).AsEnumerable();
            //return base.FindBy(predicate);
        }
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
