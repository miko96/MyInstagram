using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyInstagram.Data.Infrastructure
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        protected DbContext entities;
        protected readonly IDbSet<T> dbset;
        public BaseRepository(DbContext context)
        {
            entities = context;
            dbset = entities.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.AsEnumerable<T>();
        }

        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public virtual T Add(T entity)
        {
            return dbset.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return dbset.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Save()
        {
            entities.SaveChanges();
        }
    }
}

