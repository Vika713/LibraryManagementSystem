using System.Collections.Generic;

namespace Data.Repositories.Generic
{
    public interface IRepository<TModel> where TModel : class
    {
        void Add(TModel entity);
        void Update(TModel entity);
        void UpdateRange(List<TModel> entities);
        void Remove(TModel entity);
        void SaveChanges();
    }
}
