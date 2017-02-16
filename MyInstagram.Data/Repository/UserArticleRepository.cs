using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyInstagram.Data.Entities;
using MyInstagram.Data.Infrastructure;

namespace MyInstagram.Data.Repository
{
    public class UserArticleRepository : BaseRepository<UserArticle>, IUserArticleRepository
    {
        public UserArticleRepository(DbContext dbContext) 
            :base(dbContext){}

    }

    public interface IUserArticleRepository : IRepository<UserArticle>
    {

    }
}
