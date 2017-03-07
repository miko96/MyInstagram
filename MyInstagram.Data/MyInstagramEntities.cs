using MyInstagram.Data.Entities;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using MyInstagram.Data.Configuration;

namespace MyInstagram.Data
{
    public class MyInstagramEntities : IdentityDbContext<ApplicationUser>
    {
        public MyInstagramEntities() : base ("MyInstagramEntities") {}

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleLike> ArticleLikes { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }        
        public DbSet<UserProfile> UserProfiles { get; set; }
              
        static MyInstagramEntities()
        {
            Database.SetInitializer<MyInstagramEntities>(new IdentityDbInit());
        }

        //public static MyInstagramEntities Create()
        //{
        //    return new MyInstagramEntities();
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new ArticleConfiguration());
            modelBuilder.Configurations.Add(new ArticleLikeConfiguration());
            modelBuilder.Configurations.Add(new ArticleCommentConfiguration());
            modelBuilder.Configurations.Add(new UserProfileConfiguration());
            
           
            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(c => c.Followers)
            //    .WithMany(c => c.Following)
            //    .Map(m =>
            //    {
            //        m.MapLeftKey("FollowersId");
            //        m.MapRightKey("FollowingId");
            //        m.ToTable("Followers");
            //    });

            //    modelBuilder.Entity<ApplicationUser>()
            //        .HasMany(c => c.FavoriteArticles)
            //        .WithMany(c => c.UsersLikes)                              
            //        .Map(m =>
            //        {
            //            m.MapLeftKey("FavoriteArticlesId");
            //            m.MapRightKey("UsersLikesId");
            //            m.ToTable("ArticleLikes");
            //        });          
        }

    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<MyInstagramEntities>
    {
        protected override void Seed(MyInstagramEntities context)
        {         
            base.Seed(context);
        }
    }
}
