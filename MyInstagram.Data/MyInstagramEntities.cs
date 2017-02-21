using MyInstagram.Data.Entities;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace MyInstagram.Data
{
    public class MyInstagramEntities : IdentityDbContext<ApplicationUser>
    {
        public MyInstagramEntities() : base ("MyInstagramEntities") {}
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<ArticleLike> ArticleLikes { get; set; }
        //public DbSet<Follow> Follows { get; set; }
        //public DbSet<UserArticle> UserArticles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
              
        static MyInstagramEntities()
        {
            Database.SetInitializer<MyInstagramEntities>(new IdentityDbInit());
        }

        public static MyInstagramEntities Create()
        {
            return new MyInstagramEntities();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Followers)
                .WithMany(c => c.Following)
                .Map(m =>
                {
                    m.MapLeftKey("FollowersId");
                    m.MapRightKey("FollowingId");
                    m.ToTable("Followers");
                });
            base.OnModelCreating(modelBuilder);
        }

        //public System.Data.Entity.DbSet<MyInstagram.Data.Entities.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<MyInstagram.Data.Entities.ApplicationUser> ApplicationUsers { get; set; }
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
