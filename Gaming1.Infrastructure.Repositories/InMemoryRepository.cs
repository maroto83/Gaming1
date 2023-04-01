using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gaming1.Infrastructure.Repositories
{
    public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : new()
    {
        public static readonly ConcurrentDictionary<Type, List<TEntity>> EntityList = new ConcurrentDictionary<Type, List<TEntity>>();

        public async Task Save(TEntity entity)
        {
            if (!EntityList.ContainsKey(typeof(TEntity)))
                EntityList[typeof(TEntity)] = new List<TEntity>();

            if (EntityList[typeof(TEntity)].Contains(entity))
            {
                await Remove(x => x.Equals(entity));
            }

            EntityList[typeof(TEntity)].Add(entity);
        }

        public async Task<TEntity> Get(Func<TEntity, bool> predicate)
        {
            // check list exist
            if (!EntityList.ContainsKey(typeof(TEntity)))
                return default;

            return EntityList[typeof(TEntity)].Where(predicate).FirstOrDefault();
        }

        private async Task Remove(Func<TEntity, bool> predicate)
        {
            // check list exist
            if (EntityList.ContainsKey(typeof(TEntity)))
            {
                foreach (var entity in await GetAll(predicate))
                {
                    EntityList[typeof(TEntity)].Remove(entity);
                }
            }
        }

        private async Task<List<TEntity>> GetAll(Func<TEntity, bool> predicate)
        {
            // check list exist
            if (!EntityList.ContainsKey(typeof(TEntity)))
                return null;

            return EntityList[typeof(TEntity)].Where(predicate).ToList();
        }
    }
}