namespace MyInstagram.Data.Entities
{
    public class ArticleLike
    {
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
