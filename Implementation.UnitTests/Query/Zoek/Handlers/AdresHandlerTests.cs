using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using Implementation.Query.Zoek;
using Implementation.Query.Zoek.Handlers;
using Contracts;
using Entities;

namespace Implementation.UnitTests.Query.Zoek.Handlers
{
    [TestClass]
    public class AdresHandlerTests
    {
        AdresQuery _zoekAdres;

        IUnitOfWork _unitOfWork;

        AdresDataHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
        }

        [TestMethod]
        public void Should_Return_InWhitelist_False_When_Adress_Is_Not_Found()
        {
            _unitOfWork.Repository<Adres>().Add(new Adres("1111AA"));

            _zoekAdres = new AdresQuery("1111AB");

            var result = ExecuteHandleOnSut();

            Assert.IsFalse(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_InWhitelist_True_When_Adress_Is_Found()
        {
            _unitOfWork.Repository<Adres>().Add(new Adres("1111AA"));

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

            A.CallTo(() => _unitOfWork.Repository<Adres>());

            return _sut.Handle(_zoekAdres);
        }
    }
}
