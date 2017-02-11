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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
            }
        }
    }
}
