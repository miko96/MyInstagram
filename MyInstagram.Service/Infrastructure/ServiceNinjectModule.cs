using Ninject.Modules;
using MyInstagram.Service.Services;

namespace MyInstagram.Service.Infrastructure
{
    public class ServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IArticleService>().To<ArticleService>();
            Bind<IUserArticleService>().To<UserArticleService>();
            Bind<IUserProfileService>().To<UserProfileService>();
        }
    }
}
