using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using Implementation.Query.Zoek;
using Implementation.Query.Zoek.Handlers;
using Contracts;
using Entities;
using System.Collections.Generic;

namespace Implementation.UnitTests.Query.Zoek.Handlers
{
    [TestClass]
    public class AdresHandlerTests
    {
        IRepository<Adres> _adresRepository;
        IUnitOfWork _unitOfWork;
        List<Adres> _adressen;

        AdresQuery _zoekAdres;

        AdresDataHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _adressen = new List<Adres>();
            _unitOfWork = A.Fake<IUnitOfWork>();
            _adresRepository = A.Fake<IRepository<Adres>>();
        }

        [TestMethod]
        public void Should_Return_InWhitelist_False_When_Adress_Is_Not_Found()
        {
            _adressen.Add(new Adres("1111AA"));

            _zoekAdres = new AdresQuery("1111AB");

            var result = ExecuteHandleOnSut();

            Assert.IsFalse(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_InWhitelist_True_When_Adress_Is_Found()
        {
            _adressen.Add(new Adres("1111AA"));

            _zoekAdres = new AdresQuery("1111AA");

            var result = ExecuteHandleOnSut();

            Assert.IsTrue(result.InWhitelist);
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
