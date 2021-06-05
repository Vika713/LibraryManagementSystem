using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data.Repositories.Generic
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        protected readonly DbContext DatabaseContext;

        public Repository(DbContext context)
        {
            this.DatabaseContext = context;
        }

        public void Add(TModel entity)
        {
            DatabaseContext.Set<TModel>().Add(entity);
        }

        public void Update(TModel entity)
        {
            DatabaseContext.Set<TModel>().Update(entity);
        }

        public void UpdateRange(List<TModel> entities)
        {
            DatabaseContext.Set<TModel>().UpdateRange(entities);
        }

        public void Remove(TModel entity)
        {
            DatabaseContext.Set<TModel>().Remove(entity);
        }

        public virtual void SaveChanges()
        {
            DatabaseContext.SaveChanges();
        }
    }
}
