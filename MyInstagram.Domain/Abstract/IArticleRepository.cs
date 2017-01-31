using System.Collections.Generic;
using MyInstagram.Domain.Entities;


namespace MyInstagram.Domain.Abstract
{
    public interface IArticleRepository
    {
        IEnumerable<Article> Articles { get; }

        void SaveArticle(Article article);
    }
}
