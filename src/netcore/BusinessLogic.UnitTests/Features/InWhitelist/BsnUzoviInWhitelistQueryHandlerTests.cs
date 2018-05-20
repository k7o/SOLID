﻿using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using BusinessLogic.Features.InWhitelist;
using Dtos.Features.InWhitelist;
using BusinessLogic.Contexts.Entities;
using BusinessLogic.Contexts;

namespace BusinessLogic.UnitTests.Features.InWhitelist
{
    public class BsnUzoviInWhitelistQueryHandlerTests : IDisposable
    {
        readonly WhitelistContext _whitelistContext;

        BsnUzoviInWhitelistQuery _zoekBsnUzovi;
        BsnUzoviInWhitelistQueryHandler _sut;

        public BsnUzoviInWhitelistQueryHandlerTests()
        {
            _whitelistContext = new WhitelistContext(
                new DbContextOptionsBuilder()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options);
        }

        [Fact]
        public void Should_Return_InWhitelist_False_When_BsnUzovi_Is_Not_Found()
        {
            _whitelistContext.BsnUzovis.Add(new BsnUzovi(1, 2));
            _whitelistContext.SaveChanges();

            _zoekBsnUzovi = new BsnUzoviInWhitelistQuery(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.False(result.InWhitelist);
        }

        [Fact]
        public void Should_Return_InWhitelist_True_When_BsnUzovi_Is_Found()
        {
            _whitelistContext.BsnUzovis.Add(new BsnUzovi(1, 1));
            _whitelistContext.SaveChanges();

            _zoekBsnUzovi = new BsnUzoviInWhitelistQuery(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.True(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new BsnUzoviInWhitelistQueryHandler(_whitelistContext);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            return _sut
                .Handle(_zoekBsnUzovi, new System.Threading.CancellationToken())
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
        // ~BsnUzoviHandlerTests() {
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