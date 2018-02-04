using System;
using ClassLibrary1.Infrastructure;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ClassLibrary1.Query.Whitelist;

namespace ClassLibrary1.UnitTests.Query.Whitelist
{
    [TestClass]
    public class ZoekBsnUzoviTests
    {
        ZoekBsnUzovi _zoekBsnUzovi;
        ServiceResult _serviceResult;

        IQueryHandler<ServiceQuery, ServiceResult> _serviceQueryHandler;

        Handlers.ZoekBsnUzoviHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _serviceQueryHandler = A.Fake<IQueryHandler<ServiceQuery, ServiceResult>>();
        }

        [TestMethod]
        public void Should_Return_InWhitelist_False_When_Bsn_Is_Not_Found()
        {
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .With(1, 2)
                .Build();

            _zoekBsnUzovi = new ZoekBsnUzovi(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.IsFalse(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_InWhitelist_True_When_Bsn_Is_Found()
        {
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .With(1, 1)
                .Build();

            _zoekBsnUzovi = new ZoekBsnUzovi(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.IsTrue(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_ZoekBsn_On_Query()
        {
            _zoekBsnUzovi = new ZoekBsnUzovi(1, 1);
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .Build();

            var result = ExecuteHandleOnSut();

            Assert.AreEqual(_zoekBsnUzovi, result.Query);
        }

        private void CreateSut()
        {
            _sut = new Handlers.ZoekBsnUzoviHandler(_serviceQueryHandler);
        }

        private ZoekQueryResult<ZoekBsnUzovi> ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _serviceQueryHandler.Handle(A<ServiceQuery>.Ignored)).Returns(_serviceResult);

            return _sut.Handle(_zoekBsnUzovi);
        }
    }
}
