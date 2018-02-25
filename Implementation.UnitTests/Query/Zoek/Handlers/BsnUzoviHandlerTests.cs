using System;
using Contracts;
using Implementation.Query.Service;
using Implementation.Query.Zoek;
using Implementation.Query.Zoek.Handlers;
using Implementation.UnitTests.Query.Service;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contracts;

namespace Implementation.UnitTests.Query.Zoek.Handlers
{
    [TestClass]
    public class BsnUzoviHandlerTests
    {
        BsnUzoviQuery _zoekBsnUzovi;
        ServiceResult _serviceResult;

        IQueryHandler<ServiceQuery, ServiceResult> _serviceQueryHandler;

        BsnUzoviHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _serviceQueryHandler = A.Fake<IQueryHandler<ServiceQuery, ServiceResult>>();
        }

        [TestMethod]
        public void Should_Return_InWhitelist_False_When_BsnUzovi_Is_Not_Found()
        {
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .With(1, 2)
                .Build();

            _zoekBsnUzovi = new BsnUzoviQuery(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.IsFalse(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_InWhitelist_True_When_BsnUzovi_Is_Found()
        {
            _serviceResult = ServiceQueryResultBuilder.Construct()
                .With(1, 1)
                .Build();

            _zoekBsnUzovi = new BsnUzoviQuery(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.IsTrue(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new BsnUzoviHandler(_serviceQueryHandler);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _serviceQueryHandler.Handle(A<ServiceQuery>.Ignored)).Returns(_serviceResult);

            return _sut.Handle(_zoekBsnUzovi);
        }
    }
}
