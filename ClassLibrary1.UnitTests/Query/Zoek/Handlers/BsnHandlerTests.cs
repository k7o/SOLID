using System;
using ClassLibrary1.Infrastructure;
using ClassLibrary1.Query.Service;
using ClassLibrary1.Query.Zoek;
using ClassLibrary1.Query.Zoek.Handlers;
using ClassLibrary1.UnitTests.Query.Service;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassLibrary1.UnitTests.Query.Zoek.Handlers
{
    [TestClass]
    public class BsnHandlerTests
    {
        BsnQuery _zoekBsn;
        ServiceResult _serviceResult;

        IQueryHandler<ServiceQuery, ServiceResult> _serviceQueryHandler;

        BsnHandler _sut;

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

            _zoekBsn = new BsnQuery(1);

            var result = ExecuteHandleOnSut();

            Assert.IsFalse(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_InWhitelist_True_When_Bsn_Is_Found()
        {
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .With(1)
                .Build();

            _zoekBsn = new BsnQuery(1);

            var result = ExecuteHandleOnSut();

            Assert.IsTrue(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new BsnHandler(_serviceQueryHandler);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _serviceQueryHandler.Handle(A<ServiceQuery>.Ignored)).Returns(_serviceResult);

            return _sut.Handle(_zoekBsn);
        }
    }
}
