using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using iBalekaAPI.Models;
using iBalekaAPI.Data.Configurations;

namespace iBalekaAPI.Data.Infastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private iBalekaDBContext dbContext;
        DbContextFactoryOptions opt { get; set; }

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public iBalekaDBContext DbContext
        {
            
            get { return dbContext ?? (dbContext = dbFactory.Init(opt)); }
        }

        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
