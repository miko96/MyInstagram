using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace MyInstagram.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;
        public UnitOfWork(DbContext context)
        {
            dbContext = context;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }      
    }
}
