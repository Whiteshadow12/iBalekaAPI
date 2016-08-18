using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using iBalekaAPI.Data.Configurations;

namespace iBalekaAPI.Data.Infastructure
{
    public abstract class RepositoryBase<T> where T:class
    {
        #region Properties
        private iBalekaDBContext DbContext;
        private readonly DbSet<T> dbSet;

        #endregion

        protected RepositoryBase(iBalekaDBContext dbContext)
        {
            DbContext = dbContext;
            dbSet = DbContext.Set<T>();
        }
        #region Implementation
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        #endregion
    }
}
