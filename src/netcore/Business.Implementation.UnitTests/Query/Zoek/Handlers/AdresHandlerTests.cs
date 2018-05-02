using System;
using Business.Context;
using BusinessLogic.Query.InWhitelist.Handlers;
using Xunit;
using Dtos.Query.InWhitelist;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Entities;

namespace BusinessLogic.UnitTests.Query.Zoek.Handlers
{
    public class AdresHandlerTests : IDisposable
    {
        WhitelistContext _whitelistContext;

        AdresQuery _zoekAdres;

        AdresInWhitelistHandler _sut;

        public AdresHandlerTests()
        {
            _whitelistContext = new WhitelistContext(
                new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
        }

        [Fact]
        public void Should_Return_InWhitelist_False_When_Adress_Is_Not_Found()
        {
            _whitelistContext.Add(new Adres("1111AA"));
            _whitelistContext.SaveChanges();

            _zoekAdres = new AdresQuery("1111AB");

            var result = ExecuteHandleOnSut();

            Assert.False(result.InWhitelist);
        }

        [Fact]
        public void Should_Return_InWhitelist_True_When_Adress_Is_Found()
        {
            _whitelistContext.Add(new Adres("1111AA"));
            _whitelistContext.SaveChanges();

            _zoekAdres = new AdresQuery("1111AA");

            var result = ExecuteHandleOnSut();

            Assert.True(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new AdresInWhitelistHandler(_whitelistContext);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            return _sut
                .Handle(_zoekAdres, new System.Threading.CancellationToken())
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
        // ~AdresHandlerTests() {
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
