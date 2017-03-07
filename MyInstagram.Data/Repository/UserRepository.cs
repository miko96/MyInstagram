using MyInstagram.Data.Entities;
using MyInstagram.Data.Infrastructure;
using System.Data.Entity;

namespace MyInstagram.Data.Repository
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context){}
    }
    public interface IUserRepository : IRepository <ApplicationUser>
    {

    }
}
