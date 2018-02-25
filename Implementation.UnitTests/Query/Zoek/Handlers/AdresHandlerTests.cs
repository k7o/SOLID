using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure;
using FakeItEasy;
using Implementation.UnitTests.Agents;
using Implementation.Query.Zoek;
using Implementation.Query.Service;
using Implementation.Query.Zoek.Handlers;
using Implementation.UnitTests.Query.Service;
using Contracts;

namespace Implementation.UnitTests.Query.Zoek.Handlers
{
    [TestClass]
    public class AdresHandlerTests
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
