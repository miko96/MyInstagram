using MyInstagram.Service.Infrastructure;
using MyInstagram.Data.Infrastructure;
using MyInstagram.Data.Repository;
using MyInstagram.Data.Entities;

namespace MyInstagram.Service.Services
{
    public class UserProfileService : EntityService<UserProfile>, IUserProfileService
    {
        IUnitOfWork unitOfWork;
        IUserProfileRepository userProfileRepository;

        public UserProfileService(IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository)
            : base(unitOfWork, userProfileRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
        }

    }
    public interface IUserProfileService : IEntityService<UserProfile>
    {

    }
}
