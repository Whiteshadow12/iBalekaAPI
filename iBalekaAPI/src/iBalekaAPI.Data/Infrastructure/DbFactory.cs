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
        private readonly IConfigurationRoot config;
        public DbFactory(IConfigurationRoot _config)
        {
            config = _config;
        }
        public iBalekaDBContext Create(DbContextFactoryOptions opt)
        {
            
            var builder = new DbContextOptionsBuilder<iBalekaDBContext>();
            var cs = config.GetValue<string>("ConnectionStrings:DefaultConnection");
            builder.UseSqlServer(cs);
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
