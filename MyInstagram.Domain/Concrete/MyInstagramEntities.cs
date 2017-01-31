using MyInstagram.Domain.Entities;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace MyInstagram.Domain.Concrete
{
    public class MyInstagramEntities : IdentityDbContext<ApplicationUser>
    {
        public MyInstagramEntities() : base ("MyInstagramEntities") {}
        public DbSet<Article> Articles { get; set; }

        static MyInstagramEntities()
        {
            Database.SetInitializer<MyInstagramEntities>(new IdentityDbInit());
        }

        public static MyInstagramEntities Create()
        {
            return new MyInstagramEntities();
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<MyInstagramEntities>
    {
        protected override void Seed(MyInstagramEntities context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(MyInstagramEntities context)
        {
            // настройки конфигурации контекста будут указываться здесь
        }
    }
}
