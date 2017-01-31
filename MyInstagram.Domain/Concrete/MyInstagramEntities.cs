using MyInstagram.Domain.Entities;
using System.Data.Entity;

namespace MyInstagram.Domain.Concrete
{
    public class MyInstagramEntities : DbContext
    {
        public MyInstagramEntities() : base ("MyInstagramEntities") {}
        public DbSet<Article> Articles { get; set; }
    }
}
