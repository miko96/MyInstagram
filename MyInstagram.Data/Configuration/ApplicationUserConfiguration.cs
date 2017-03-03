using MyInstagram.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInstagram.Data.Configuration
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            ToTable("Users");

            HasRequired(x => x.UserProfile).WithRequiredPrincipal(x => x.ApplicationUser)
                .WillCascadeOnDelete(true);

            HasMany(x => x.Followers).WithMany(x => x.Following)
                .Map(m =>
                {
                    m.MapLeftKey("FollowersId");
                    m.MapRightKey("FollowingId");
                    m.ToTable("Followers");
                });

            //HasMany(x => x.FavoriteArticles).WithMany(x => x.UsersLikes)
            //    .Map(m =>
            //    {
            //        m.MapLeftKey("UserId");
            //        m.MapRightKey("ArticleId");
            //        m.ToTable("ArticleLikes");
            //    });


        }
    }
}
