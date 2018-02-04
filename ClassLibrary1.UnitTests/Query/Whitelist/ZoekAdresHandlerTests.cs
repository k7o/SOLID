using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Infrastructure;
using FakeItEasy;
using ClassLibrary1.UnitTests.Agents;
using static ClassLibrary1.Query.Whitelist;

namespace ClassLibrary1.UnitTests.Query.Whitelist
{
    [TestClass]
    public class ZoekAdresHandlerTests
    {
        ZoekAdres _zoekAdres;
        ServiceResult _serviceResult;

        IQueryHandler<ServiceQuery, ServiceResult> _serviceQueryHandler;

        Handlers.ZoekAdresHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _serviceQueryHandler = A.Fake<IQueryHandler<ServiceQuery, ServiceResult>>();
        }

        [TestMethod]
        public void Should_Return_InWhitelist_False_When_Adress_Is_Not_Found()
        {
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .With("Hertogshoef")
                .Build();

            _zoekAdres = new ZoekAdres("Het Spant");

            var result = ExecuteHandleOnSut();

            Assert.IsFalse(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_InWhitelist_True_When_Adress_Is_Found()
        {
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .With("Het Spant")
                .Build();

            _zoekAdres = new ZoekAdres("Het Spant");

            var result = ExecuteHandleOnSut();

            Assert.IsTrue(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_Zoekadres_On_Query()
        {
            _zoekAdres = new ZoekAdres("Het Spant");
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .Build();

            var result = ExecuteHandleOnSut();

            Assert.AreEqual(_zoekAdres, result.Query);
        }

        private void CreateSut()
        {
            _sut = new Handlers.ZoekAdresHandler(_serviceQueryHandler);
        }

        private ZoekQueryResult<ZoekAdres> ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _serviceQueryHandler.Handle(A<ServiceQuery>.Ignored)).Returns(_serviceResult);

            return _sut.Handle(_zoekAdres);
        }
    }
}
