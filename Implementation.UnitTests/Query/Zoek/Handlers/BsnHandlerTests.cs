using Contracts;
using Implementation.Query.Zoek;
using Implementation.Query.Zoek.Handlers;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Implementation.UnitTests.Query.Zoek.Handlers
{
    [TestClass]
    public class BsnHandlerTests
    {
        BsnQuery _zoekBsn;

        IUnitOfWork _unitOfWork;

        BsnDataHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
        }

        [TestMethod]
        public void Should_Return_InWhitelist_False_When_Bsn_Is_Not_Found()
        {
            _unitOfWork.Repository<BsnQuery>().Add(new BsnQuery(1));

            _zoekBsn = new BsnQuery(1);

            var result = ExecuteHandleOnSut();

            Assert.IsFalse(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_InWhitelist_True_When_Bsn_Is_Found()
        {
            _unitOfWork.Repository<BsnQuery>().Add(new BsnQuery(1));


            _zoekBsn = new BsnQuery(1);

            var result = ExecuteHandleOnSut();

            Assert.IsTrue(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new BsnDataHandler(_unitOfWork);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

           return _sut.Handle(_zoekBsn);
        }
    }
}
