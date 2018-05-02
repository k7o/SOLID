using Xunit;
using Dtos.Query.InWhitelist;
using BusinessLogic.Entities;
using BusinessLogic.Query.InWhitelist.Handlers;
using Business.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace BusinessLogic.UnitTests.Query.Zoek.Handlers
{
    public class BsnHandlerTests : IDisposable
    {
        BsnQuery _zoekBsn;

        readonly WhitelistContext _whitelistContext;

        BsnInWhitelistHandler _sut;

        public BsnHandlerTests()
        {
            _whitelistContext = new WhitelistContext(
                new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
        }

        [Fact]
        public void Should_Return_InWhitelist_False_When_Bsn_Is_Not_Found()
        {
            _whitelistContext.Bsns.Add(new Bsn(2));
            _whitelistContext.SaveChanges();

            _zoekBsn = new BsnQuery(1);

            var result = ExecuteHandleOnSut();

            Assert.False(result.InWhitelist);
        }

        [Fact]
        public void Should_Return_InWhitelist_True_When_Bsn_Is_Found()
        {
            _whitelistContext.Bsns.Add(new Bsn(1));
            _whitelistContext.SaveChanges();
            
            _zoekBsn = new BsnQuery(1);

            var result = ExecuteHandleOnSut();

            Assert.True(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new BsnInWhitelistHandler(_whitelistContext);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            return _sut
                .Handle(_zoekBsn, new System.Threading.CancellationToken())
                .Result;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _whitelistContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BsnHandlerTests() {
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
