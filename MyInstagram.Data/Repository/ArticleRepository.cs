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
    }

    public interface IArticleRepository : IRepository<Article>
    {}
}
