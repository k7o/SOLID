using System;
using ClassLibrary1.Decorators;
using Infrastructure;
using ClassLibrary1.Query.Service;
using ClassLibrary1.Query.Zoek;
using FakeItEasy;
using LazyCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;

namespace ClassLibrary1.UnitTests.Decorators
{
    [TestClass]
    public class QueryCacheDecoratorTests
    {
        ILog _log;
        ICache _cache;
        
        IQueryHandler<ServiceQuery, ServiceResult> _decorated;

        ServiceQuery _query;

        QueryCacheDecorator<ServiceQuery, ServiceResult> _sut;

        [TestInitialize]
        public void Initialize()
        {
            _log = A.Fake<ILog>();
            _cache = A.Fake<ICache>();
            _decorated = A.Fake<IQueryHandler<ServiceQuery, ServiceResult>>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_Should_Throw_ArgumentNullException_When_Decorated_Handler_Is_Null()
        {
            _decorated = null;

            ExecuteHandleOnSut();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_Should_Throw_ArgumentNullException_When_AppCache_Is_Null()
        {
            _cache = null;

            ExecuteHandleOnSut();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_Should_Throw_ArgumentNullException_When_Logger_Handler_Is_Null()
        {
            _log = null;

            ExecuteHandleOnSut();
        }

        [TestMethod]
        public void Handle_Should_Call_GetOrAdd_On_AppCache()
        {
            _query = new ServiceQuery();

            ExecuteHandleOnSut();

            A.CallTo(() => _cache.GetOrAdd(A<string>.Ignored, A<Func<ServiceResult>>.Ignored))
                .MustHaveHappened();
        }

        private void CreateSut()
        {
            _sut = new QueryCacheDecorator<ServiceQuery, ServiceResult>(_cache, _log, _decorated);
        }

        private ServiceResult ExecuteHandleOnSut()
        {
            CreateSut();

            return _sut.Handle(_query);
        }
    }
}
