using Ninject.Modules;
using MyInstagram.Data.Repository;
using System.Data.Entity;
using Ninject.Web.Common;
using MyInstagram.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace MyInstagram.Data.Infrastructure
{
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>()
                .InRequestScope();
                //.WithConstructorArgument("context", Kernel.GetService<DbContext>());
            Bind<ApplicationUserManager>().ToSelf().InRequestScope();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IArticleRepository>().To<ArticleRepository>();
            Bind<IUserProfileRepository>().To<UserProfileRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            
        }
    }
}
