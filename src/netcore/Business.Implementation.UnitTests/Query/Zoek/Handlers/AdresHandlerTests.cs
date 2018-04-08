using FakeItEasy;
using Contracts;
using System.Collections.Generic;
using Business.Implementation.Query.InWhitelist.Handlers;
using Business.Contracts.Query.InWhitelist;
using Business.Entities;
using Xunit;

namespace Business.Implementation.UnitTests.Query.Zoek.Handlers
{
    public class AdresHandlerTests
    {
        IRepository<Adres> _adresRepository;
        IUnitOfWork _unitOfWork;
        List<Adres> _adressen;

        AdresQuery _zoekAdres;

        AdresDataHandler _sut;

        public AdresHandlerTests()
        {
            _adressen = new List<Adres>();
            _unitOfWork = A.Fake<IUnitOfWork>();
            _adresRepository = A.Fake<IRepository<Adres>>();
        }

        [Fact]
        public void Should_Return_InWhitelist_False_When_Adress_Is_Not_Found()
        {
            _adressen.Add(new Adres("1111AA"));

            _zoekAdres = new AdresQuery("1111AB");

            var result = ExecuteHandleOnSut();

            Assert.False(result.InWhitelist);
        }

        [Fact]
        public void Should_Return_InWhitelist_True_When_Adress_Is_Found()
        {
            _adressen.Add(new Adres("1111AA"));

            _zoekAdres = new AdresQuery("1111AA");

            var result = ExecuteHandleOnSut();

            Assert.True(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new AdresDataHandler(_unitOfWork);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _adresRepository.GetAll())
                .Returns(_adressen);
            A.CallTo(() => _unitOfWork.Repository<Adres>())
                .Returns(_adresRepository);

            return _sut.Handle(_zoekAdres);
        }
    }
}
