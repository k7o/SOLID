using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Features.InWhitelist;
using Dtos.Features.InWhitelist;
using BusinessLogic.Contexts;
using BusinessLogic.Contexts.Entities;

namespace BusinessLogic.UnitTests.Features.InWhitelist
{
    public class AdresInWhitelistQueryHandlerTests : IDisposable
    {
        WhitelistContext _whitelistContext;

        AdresInWhitelistQuery _zoekAdres;

        AdresInWhitelistQueryHandler _sut;

        public AdresInWhitelistQueryHandlerTests()
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

            _zoekAdres = new AdresInWhitelistQuery("1111AB");

            var result = ExecuteHandleOnSut();

            Assert.False(result.InWhitelist);
        }

        [Fact]
        public void Should_Return_InWhitelist_True_When_Adress_Is_Found()
        {
            _whitelistContext.Add(new Adres("1111AA"));
            _whitelistContext.SaveChanges();

            _zoekAdres = new AdresInWhitelistQuery("1111AA");

            var result = ExecuteHandleOnSut();

            Assert.True(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new AdresInWhitelistQueryHandler(_whitelistContext);
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
