using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeineFix.Repository.Infrastructure.Contract;

namespace CaffeineFix.Repository.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CaffeineFixEntities _dbContext;

        public UnitOfWork()
        {
            _dbContext = new CaffeineFixEntities();
        }

        public DbContext Db
        {
            get { return _dbContext; }
        }

        public void Dispose()
        {
        }
    }
}
