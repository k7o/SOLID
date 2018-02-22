using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Infrastructure;
using FakeItEasy;
using ClassLibrary1.UnitTests.Agents;
using ClassLibrary1.Query.Zoek;
using ClassLibrary1.Query.Service;
using ClassLibrary1.Query.Zoek.Handlers;

namespace ClassLibrary1.UnitTests.Query.Whitelist
{
    [TestClass]
    public class ZoekAdresHandlerTests
    {
        AdresQuery _zoekAdres;
        ServiceResult _serviceResult;

        IQueryHandler<ServiceQuery, ServiceResult> _serviceQueryHandler;

        AdresHandler _sut;

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

            _zoekAdres = new AdresQuery("Het Spant");

            var result = ExecuteHandleOnSut();

            Assert.IsFalse(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_InWhitelist_True_When_Adress_Is_Found()
        {
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .With("Het Spant")
                .Build();

            _zoekAdres = new AdresQuery("Het Spant");

            var result = ExecuteHandleOnSut();

            Assert.IsTrue(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new AdresHandler(_serviceQueryHandler);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _serviceQueryHandler.Handle(A<ServiceQuery>.Ignored)).Returns(_serviceResult);

            return _sut.Handle(_zoekAdres);
        }
    }
}
