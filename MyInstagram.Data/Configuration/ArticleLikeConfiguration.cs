using MyInstagram.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MyInstagram.Data.Configuration
{
    public class ArticleLikeConfiguration : EntityTypeConfiguration<ArticleLike>
    {
        public ArticleLikeConfiguration()
        {
            HasKey(x => new { x.ApplicationUserID, x.ArticleId });
            HasRequired(x => x.ApplicationUser).WithMany(x => x.FavoriteArticles)
                .HasForeignKey(x => x.ApplicationUserID).WillCascadeOnDelete(false);
            HasRequired(x => x.Article).WithMany(x => x.ArticleLikes)
                .HasForeignKey(x => x.ArticleId).WillCascadeOnDelete(true);
        }
    }
}
