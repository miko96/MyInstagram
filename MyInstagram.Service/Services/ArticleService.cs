﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInstagram.Service.Infrastructure;
using MyInstagram.Data.Infrastructure;
using MyInstagram.Data.Repository;
using MyInstagram.Data.Entities;
using System.Linq.Expressions;

namespace MyInstagram.Service.Services
{
    public class ArticleService : EntityService<Article>, IArticleService
    {
        IUnitOfWork unitOfWork;
        IArticleRepository articleRepository;

        public ArticleService(IUnitOfWork unitOfWork, IArticleRepository articleRepository)
            : base(unitOfWork, articleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.articleRepository = articleRepository;
        }

        public Article GetById(int id)
        {
            return FindBy(x => x.ArticleId == id).FirstOrDefault();
        }
    }

    public interface IArticleService : IEntityService<Article>
    {
        Article GetById(int id);
    }
}
