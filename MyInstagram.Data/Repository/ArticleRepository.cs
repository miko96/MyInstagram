using System;
using System.Collections.Generic;
using System.Linq;
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

        //public override IQueryable<Article> FindBy(Expression<Func<Article, bool>> predicate)
        //{
        //    return dbset.Include(x => x.ApplicationUser).Where(predicate);
        //    //return base.FindBy(predicate);
        //}
        //public Article GetById(int id)
        //{
        //    return FindBy(x => x.ArticleId == id).FirstOrDefault();
        //}
    }

    public interface IArticleRepository : IRepository<Article>
    {
        //Article GetById(int id);
    }
}
