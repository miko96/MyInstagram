using MyInstagram.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MyInstagram.Data.Configuration
{
    public class ArticleConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            HasKey(x => x.ArticleId);                                   
            HasRequired(x => x.ApplicationUser).WithMany(x => x.UserArticles)
                .HasForeignKey(x => x.ApplicationUserId).WillCascadeOnDelete(true);

            Property(x => x.ArticleId).IsRequired();
            Property(x => x.Description).IsRequired().HasMaxLength(30);
            Property(x => x.ImageData).IsOptional();
            Property(x => x.ImageMimeType).IsOptional();
            Property(x => x.DateCreated).IsRequired();
        }
    }
}
