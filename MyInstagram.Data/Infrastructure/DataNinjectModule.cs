using Ninject.Modules;
using MyInstagram.Data.Repository;


namespace MyInstagram.Data.Infrastructure
{
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IArticleRepository>().To<ArticleRepository>();
           // Bind<IUserArticleRepository>().To<UserArticleRepository>();
            Bind<IUserProfileRepository>().To<UserProfileRepository>();
            
        }
    }
}
