using MyInstagram.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MyInstagram.Data.Configuration
{
    public class ArticleCommentConfiguration : EntityTypeConfiguration<ArticleComment>
    {
        public ArticleCommentConfiguration()
        {         
            HasKey(x => new { x.ApplicationUserID, x.ArticleId, x.ArticleCommentId });
            HasRequired(x => x.ApplicationUser).WithMany(x => x.UsersComments)
                .HasForeignKey(x => x.ApplicationUserID).WillCascadeOnDelete(false);
            HasRequired(x => x.Article).WithMany(x => x.ArticleComments)
                .HasForeignKey(x => x.ArticleId).WillCascadeOnDelete(true);

            Property(x => x.ArticleCommentId).IsRequired();
            Property(x => x.CommentText).IsRequired().HasMaxLength(20);        
        }
    }
}
