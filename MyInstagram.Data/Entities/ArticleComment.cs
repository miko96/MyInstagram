namespace MyInstagram.Data.Entities
{
    public class ArticleComment
    {
        public int ArticleCommentId { get; set; }
        public string CommentText { get; set; }

        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }     
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }      
    }
}
