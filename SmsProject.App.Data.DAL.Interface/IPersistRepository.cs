using System.Collections.Generic;

namespace SmsProject.App.Data.DAL.Interface
{
    public interface IPersistRepository<in TEntity> where TEntity : class
    {
        bool Add(TEntity entity);
        bool Add(IEnumerable<TEntity> items);
        bool Merge(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        bool Delete(IEnumerable<TEntity> entities);
    }
}