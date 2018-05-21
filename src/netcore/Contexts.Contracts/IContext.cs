using System.Threading;
using System.Threading.Tasks;

namespace Contexts.Contracts
{
    public interface IContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int SaveChanges();

        IContextTransaction BeginTransaction();

        Task<IContextTransaction> BeginTransactionAsync();
    }
}
