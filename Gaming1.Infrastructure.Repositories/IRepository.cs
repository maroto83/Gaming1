using System;
using System.Threading.Tasks;

namespace Gaming1.Infrastructure.Repositories
{
    public interface IRepository<TEntity>
    {
        Task Save(TEntity entity);
        Task<TEntity> Get(Func<TEntity, bool> predicate);
    }
}