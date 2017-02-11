using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInstagram.Data.Infrastructure;
namespace MyInstagram.Service.Infrastructure
{
    public abstract class EntityService<T> : IEntityService<T> 
        where T : class
    {
        IUnitOfWork unitOfWork;
        IRepository<T> repository;

        public EntityService(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        public virtual void Create(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            repository.Add(entity);
            unitOfWork.Commit();
        }
        public virtual void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            repository.Edit(entity);
            unitOfWork.Commit();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            repository.Delete(entity);
            unitOfWork.Commit();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return repository.GetAll();
        }
    }
}
