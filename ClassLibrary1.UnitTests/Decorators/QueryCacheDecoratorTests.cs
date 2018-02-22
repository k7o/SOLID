using System;
using ClassLibrary1.Decorators;
using ClassLibrary1.Infrastructure;
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
        ILogger _logger;
        IAppCache _appCache;
        
        IQueryHandler<ServiceQuery, ServiceResult> _decorated;

        ServiceQuery _query;

        QueryCacheDecorator<ServiceQuery, ServiceResult> _sut;

        [TestInitialize]
        public void Initialize()
        {
            _logger = A.Fake<ILogger>();
            _appCache = A.Fake<IAppCache>();
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
            _appCache = null;

            ExecuteHandleOnSut();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_Should_Throw_ArgumentNullException_When_Logger_Handler_Is_Null()
        {
            _logger = null;

            ExecuteHandleOnSut();
        }

        [TestMethod]
        public void Handle_Should_Call_GetOrAdd_On_AppCache()
        {
            _query = new ServiceQuery();

            ExecuteHandleOnSut();

            A.CallTo(() => _appCache.GetOrAdd(A<string>.Ignored, A<Func<ZoekResult>>.Ignored))
                .MustHaveHappened();
        }

        private void CreateSut()
        {
            _sut = new QueryCacheDecorator<ServiceQuery, ServiceResult>(_appCache, _logger, _decorated);
        }

        private ServiceResult ExecuteHandleOnSut()
        {
            CreateSut();

            return _sut.Handle(_query);
        }
    }
}
