using Contexts.Contracts;
using Crosscutting.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.Contexts
{
    class WhitelistContextTransaction : IContextTransaction
    {
        readonly IDbContextTransaction _dbContextTransaction;

        public WhitelistContextTransaction(IDbContextTransaction dbContextTransaction)
        {
            Guard.IsNotNull(dbContextTransaction, nameof(dbContextTransaction));

            _dbContextTransaction = dbContextTransaction;
        }

        public Guid TransactionId => Guid.NewGuid();

        public void Commit()
        {
            _dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }

        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContextTransaction.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
