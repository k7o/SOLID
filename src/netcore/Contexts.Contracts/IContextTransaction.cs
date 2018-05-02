using System;
using System.Collections.Generic;
using System.Text;

namespace Contexts.Contracts
{
    public interface IContextTransaction : IDisposable
    {
        void Commit();

        void Rollback();

        Guid TransactionId { get; }
    }
}
