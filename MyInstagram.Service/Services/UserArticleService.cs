using MyInstagram.Service.Infrastructure;
using MyInstagram.Data.Infrastructure;
using MyInstagram.Data.Repository;
using MyInstagram.Data.Entities;

namespace MyInstagram.Service.Services
{
    public class UserArticleService : EntityService<UserArticle>, IUserArticleService
    {
        IUnitOfWork unitOfWork;
        IUserArticleRepository userArticleRepository;

        public UserArticleService(IUnitOfWork unitOfWork, IUserArticleRepository userArticleRepository)
            :base(unitOfWork, userArticleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userArticleRepository = userArticleRepository;
        }
    }


    public interface IUserArticleService : IEntityService<UserArticle>
    {

    }
}
