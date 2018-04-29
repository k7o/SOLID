using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contexts.Contracts
{
    public interface IRepository<TEntity>
    {
        Task<IAsyncEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);

        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
    }
}
