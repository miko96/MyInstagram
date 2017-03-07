using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace MyInstagram.Data.Infrastructure
{
    public interface IRepository<T>  where T : class
    {
        IEnumerable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);   
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
        void UpdateProperties(T entity, params Expression<Func<T, object>>[] properties);
    }
}
