using Crosscutting.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contexts.Contracts
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        readonly DbSet<TEntity> _dbSet;

        public Repository(DbSet<TEntity> dbSet)
        {
            Guard.IsNotNull(dbSet, nameof(dbSet));

            _dbSet = dbSet;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet
                .AddAsync(entity)
                .ConfigureAwait(false);
        }

        public async Task<IAsyncEnumerable<TEntity>> GetAllAsync()
        {
            // TODO: is this the wright way?
            return _dbSet
                .ToAsyncEnumerable();
        }

        public void Add(TEntity entity)
        { 
            Task.FromResult(AddAsync(entity));
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet;
        }
    }
}
