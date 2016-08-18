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
        private iBalekaDBContext DbContext;
        DbContextFactoryOptions opt { get; set; }

        public UnitOfWork(iBalekaDBContext dbContext)
        {
            DbContext = dbContext;
        }


        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
