using Contracts;
using Implementation.Query.Zoek;
using Implementation.Query.Zoek.Handlers;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;

namespace Implementation.UnitTests.Query.Zoek.Handlers
{
    [TestClass]
    public class BsnUzoviHandlerTests
    {
        BsnUzoviQuery _zoekBsnUzovi;

        IUnitOfWork _unitOfWork;

        BsnUzoviDataHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
        }

        [TestMethod]
        public void Should_Return_InWhitelist_False_When_BsnUzovi_Is_Not_Found()
        {
            _unitOfWork.Repository<BsnUzovi>()
                .Add(new BsnUzovi(1, 2));

            _zoekBsnUzovi = new BsnUzoviQuery(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.IsFalse(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_InWhitelist_True_When_BsnUzovi_Is_Found()
        {
            _unitOfWork.Repository<BsnUzovi>()
                .Add(new BsnUzovi(1, 1));

            _zoekBsnUzovi = new BsnUzoviQuery(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.IsTrue(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new BsnUzoviDataHandler(_unitOfWork);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _unitOfWork.Repository<BsnUzovi>());

            return _sut.Handle(_zoekBsnUzovi);
        }
    }
}
