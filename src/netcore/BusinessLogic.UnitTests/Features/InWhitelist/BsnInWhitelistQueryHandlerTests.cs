using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using BusinessLogic.Features.InWhitelist;
using Dtos.Features.InWhitelist;
using BusinessLogic.Contexts;
using BusinessLogic.Contexts.Entities;

namespace BusinessLogic.UnitTests.Features.InWhitelist
{
    public class BsnInWhitelistQueryHandlerTests : IDisposable
    {
        BsnInWhitelistQuery _zoekBsn;

        readonly WhitelistContext _whitelistContext;

        BsnInWhitelistQueryHandler _sut;

        public BsnInWhitelistQueryHandlerTests()
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

            _zoekBsn = new BsnInWhitelistQuery(1);

            var result = ExecuteHandleOnSut();

            Assert.False(result.InWhitelist);
        }

        [Fact]
        public void Should_Return_InWhitelist_True_When_Bsn_Is_Found()
        {
            _whitelistContext.Bsns.Add(new Bsn(1));
            _whitelistContext.SaveChanges();
            
            _zoekBsn = new BsnInWhitelistQuery(1);

            var result = ExecuteHandleOnSut();

            Assert.True(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new BsnInWhitelistQueryHandler(_whitelistContext);
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
