using MyInstagram.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInstagram.Data.Configuration
{
    public class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            HasKey(x => x.UserId);
            Property(x => x.FirstName).IsRequired().HasMaxLength(20);
            Property(x => x.LastName).IsRequired().HasMaxLength(20);
            Property(x => x.Sex).IsOptional().HasMaxLength(7);
            Property(x => x.Country).IsOptional().HasMaxLength(15);
            Property(x => x.ImageData).IsOptional();
            Property(x => x.ImageMimeType).IsOptional();
        }
    }
}
