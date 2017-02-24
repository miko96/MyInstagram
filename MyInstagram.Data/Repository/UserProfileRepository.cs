using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyInstagram.Data.Entities;
using MyInstagram.Data.Infrastructure;
using System.Linq.Expressions;

namespace MyInstagram.Data.Repository
{
    public class UserProfileRepository : BaseRepository<UserProfile>,  IUserProfileRepository
    {
        public UserProfileRepository(DbContext context)
            : base(context) { }

        public UserProfile GetById(string Id)
        {            
            return FindBy(x => x.Id == Id).FirstOrDefault();
        }
        public IQueryable<UserProfile> GetProfiles()
        {
            return dbset.AsQueryable<UserProfile>();
        }

    }


    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        UserProfile GetById(string Id);
        IQueryable<UserProfile> GetProfiles();
    }
}
