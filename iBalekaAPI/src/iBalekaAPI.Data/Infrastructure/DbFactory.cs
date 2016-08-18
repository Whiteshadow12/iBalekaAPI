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
using System.Data.SqlClient;

namespace iBalekaAPI.Data.Infastructure
{
    public class DbFactory:Disposable,IDbFactory, IDbContextFactory<iBalekaDBContext>
    {
        iBalekaDBContext dbContext;
        private readonly string ServerConnection = "Data Source=tcp:ibaleka.database.windows.net,1433;Initial Catalog = iBalekaDB; User Id = iBalekaAdmin@ibaleka;Password=AllBlacks2026;MultipleActiveResultSets=true;";
private readonly string DefaultConnection = "Server=(localdb)\\mssqllocaldb;Database=iBalekaDB;Trusted_Connection=True;MultiSubnetFailover=False;MultipleActiveResultSets=true;";
  
        public iBalekaDBContext Create(DbContextFactoryOptions opt)
        {
            
            var builder = new DbContextOptionsBuilder<iBalekaDBContext>();
            
            builder.UseSqlServer(ServerConnection);
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
