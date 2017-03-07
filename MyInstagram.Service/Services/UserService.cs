using Microsoft.AspNet.Identity;
using MyInstagram.Data.Entities;
using MyInstagram.Data.Infrastructure;
using MyInstagram.Data.Repository;
using MyInstagram.Service.Infrastructure;
using MyInstagram.Service.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyInstagram.Service.Services
{
    public class UserService : EntityService<ApplicationUser>, IUserService
    {
        IUnitOfWork unitOfWork;
        IUserRepository userRepository;
        ApplicationUserManager userManager;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, ApplicationUserManager userManager)
            : base(unitOfWork, userRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(RegisterServiceModel model)
        {
            var user = new ApplicationUser { UserName = model.UserName };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var userProfile = new UserProfile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Sex = model.Sex,
                    Country = model.Country
                };
                user.UserProfile = userProfile;
                await userManager.UpdateAsync(user);
            }
            return result;
        }

        public async Task<IdentityResult> DeleteUser(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
                return null;
            var result = await userManager.DeleteAsync(user);
            return result; 
        }

        public async Task<ClaimsIdentity> Authenticate(LoginServiceModel model)
        {
            var user = await userManager.FindAsync(model.UserName, model.Password);
            if (user == null)
                return null;
            var calim = await userManager.CreateIdentityAsync(user,
                                    DefaultAuthenticationTypes.ApplicationCookie);
            return calim;
        }

        public ApplicationUser GetByName(string userName)
        {
            return userManager.FindByName(userName);
        }

        public IQueryable<ApplicationUser> GetFollowing(string userId)
        {
            return userRepository.FindBy(x => x.Id == userId).SelectMany(x => x.Following);
        }

        public IQueryable<ApplicationUser> GetFollowers(string userId)
        {
            return userRepository.FindBy(x => x.Id == userId).SelectMany(x => x.Followers);
        }
        public bool isFollow(string userId, string toUserId)
        {
            var follower = GetFollowing(userId).Where(x=>x.Id == toUserId).FirstOrDefault();
            if (follower != null)
                return true;
            return false;
        }
    }

    public interface IUserService : IEntityService<ApplicationUser>
    {
        Task<IdentityResult> CreateUser(RegisterServiceModel model);
        Task<IdentityResult> DeleteUser(string userName);
        Task<ClaimsIdentity> Authenticate(LoginServiceModel model);
        ApplicationUser GetByName(string userName);
        IQueryable<ApplicationUser> GetFollowing(string userId);
        IQueryable<ApplicationUser> GetFollowers(string userId);
        bool isFollow(string userId, string toUserId);
    }
}

