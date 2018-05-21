using System;

namespace Contexts.Contracts
{
    public interface IContextTransaction : IDisposable
    {
        void Commit();

        void Rollback();

        Guid TransactionId { get; }
    }
}
