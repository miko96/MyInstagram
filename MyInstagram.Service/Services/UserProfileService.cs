using MyInstagram.Service.Infrastructure;
using MyInstagram.Data.Infrastructure;
using MyInstagram.Data.Repository;
using MyInstagram.Data.Entities;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public UserProfile GetById(string Id)
        {
            return FindBy(x => x.UserId == Id).FirstOrDefault();
        }

        public IQueryable<UserProfile> GetProfiles()
        {
            return userProfileRepository.GetProfiles();
        }

    }
    public interface IUserProfileService : IEntityService<UserProfile>
    {
        UserProfile GetById(string Id);
        IQueryable<UserProfile> GetProfiles();      
    }
}
