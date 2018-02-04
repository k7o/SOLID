using System;
using ClassLibrary1.Infrastructure;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ClassLibrary1.Query.Whitelist;

namespace ClassLibrary1.UnitTests.Query.Whitelist
{
    [TestClass]
    public class ZoekBsnTests
    {
        ZoekBsn _zoekBsn;
        ServiceResult _serviceResult;

        IQueryHandler<ServiceQuery, ServiceResult> _serviceQueryHandler;

        Handlers.ZoekBsnHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _serviceQueryHandler = A.Fake<IQueryHandler<ServiceQuery, ServiceResult>>();
        }

        [TestMethod]
        public void Should_Return_InWhitelist_False_When_Bsn_Is_Not_Found()
        {
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .With(2)
                .Build();

            _zoekBsn = new ZoekBsn(1);

            var result = ExecuteHandleOnSut();

            Assert.IsFalse(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_InWhitelist_True_When_Bsn_Is_Found()
        {
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .With(1)
                .Build();

            _zoekBsn = new ZoekBsn(1);

            var result = ExecuteHandleOnSut();

            Assert.IsTrue(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_ZoekBsn_On_Query()
        {
            _zoekBsn = new ZoekBsn(1);
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .Build();

            var result = ExecuteHandleOnSut();

            Assert.AreEqual(_zoekBsn, result.Query);
        }

        private void CreateSut()
        {
            _sut = new Handlers.ZoekBsnHandler(_serviceQueryHandler);
        }

        private ZoekQueryResult<ZoekBsn> ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _serviceQueryHandler.Handle(A<ServiceQuery>.Ignored)).Returns(_serviceResult);

            return _sut.Handle(_zoekBsn);
        }
    }
}
