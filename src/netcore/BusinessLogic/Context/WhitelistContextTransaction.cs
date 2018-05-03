using Contexts.Contracts;
using Crosscutting.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.Context
{
    class WhitelistContextTransaction : IContextTransaction
    {
        readonly Task<IDbContextTransaction> _dbContextTransaction;

        public WhitelistContextTransaction(IDbContextTransaction dbContextTransaction)
        {
            Guard.IsNotNull(dbContextTransaction, nameof(dbContextTransaction));

            _dbContextTransaction = Task.FromResult(dbContextTransaction);
        }

        public WhitelistContextTransaction(Task<IDbContextTransaction> dbContextTransaction)
        {
            Guard.IsNotNull(dbContextTransaction, nameof(dbContextTransaction));

            _dbContextTransaction = dbContextTransaction;
        }

        public Guid TransactionId => Guid.NewGuid();

        public void Commit()
        {
            _dbContextTransaction.Result.Commit();
        }

        public void Rollback()
        {
            _dbContextTransaction.Result.Rollback();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContextTransaction.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~WhitelistContextTransaction() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
