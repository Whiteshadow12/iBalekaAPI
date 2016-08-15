using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using iBalekaAPI.Data.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace iBalekaAPI.Data.Infastructure
{
    public class DbFactory:Disposable,IDbFactory, IDbContextFactory<iBalekaDBContext>
    {
        iBalekaDBContext dbContext;
        private readonly string ServerConnection = "Server=tcp: ibaleka.database.windows.net,1433;Initial Catalog=iBalekaDB;Persist Security Info=False;UserID='iBalekaAdmin';Password= 'AllBlacks2026';MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private readonly string DefaultConnection = "Server=(localdb)\\mssqllocaldb;Database=iBalekaDB;Trusted_Connection=True;MultiSubnetFailover=False;MultipleActiveResultSets=true;";
  
        public iBalekaDBContext Create(DbContextFactoryOptions opt)
        {
            
            var builder = new DbContextOptionsBuilder<iBalekaDBContext>();
            
            builder.UseSqlServer(DefaultConnection);
            return new iBalekaDBContext(builder.Options);
        }
        public iBalekaDBContext Init(DbContextFactoryOptions opt)
        {
            return dbContext ?? (dbContext = Create(opt));
        }
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();           
        }
    }
}
