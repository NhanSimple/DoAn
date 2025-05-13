using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Repository.Common
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;

        public UnitOfWork(DbContext context)
        {
            this.dbContext = context;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public DbContext Context()
        {
            return dbContext;
        }

        public DbContextTransaction CreateTransaction()
        {
            return dbContext.Database.BeginTransaction();
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
