using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyInstagram.Service.Infrastructure
{
    public interface IEntityService<T> where T : class
    {
        void Create(T entity);
        void Delete(T entity);
        void UpdateProperties(T entity, params Expression<Func<T, object>>[] properties);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}
