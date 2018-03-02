using System;
using Implementation.Decorators;
using Contracts;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure;
using Implementation.Query.Zoek;

namespace Implementation.UnitTests.Decorators
{
    [TestClass]
    public class QueryCacheDecoratorTests
    {
        IQueryHandler<BsnQuery, ZoekResult> _decorated;

        ILog _log;
        ICache _cache;
        IUnitOfWork _unitOfWork;

        BsnQuery _bsnQuery;

        QueryCacheDecorator<BsnQuery, ZoekResult> _sut;

        [TestInitialize]
        public void Initialize()
        {
            _log = A.Fake<ILog>();
            _cache = A.Fake<ICache>();
            
            _unitOfWork = A.Fake<IUnitOfWork>();
            _decorated = A.Fake<IQueryHandler<BsnQuery, ZoekResult>>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_Should_Throw_ArgumentNullException_When_UnitOfWork_Is_Null()
        {
            _unitOfWork = null;

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
            _bsnQuery = new BsnQuery(1);

            ExecuteHandleOnSut();

            A.CallTo(() => _cache.GetOrAdd(A<string>.Ignored, A<Func<BsnQuery>>.Ignored))
                .MustHaveHappened();
        }

        private void CreateSut()
        {
            _sut = new QueryCacheDecorator<BsnQuery, ZoekResult>(_decorated, _cache, _log);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            return _sut.Handle(_bsnQuery);
        }
    }
}
