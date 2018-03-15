using System;
using Contracts;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crosscutting.Contracts;
using Business.Implementation.Query.Zoek;
using Business.Implementation.Decorators;

namespace Business.Implementation.UnitTests.Decorators
{
    [TestClass]
    public class QueryCacheDecoratorTests
    {
        IQueryHandler<AdresQuery, ZoekResult> _decoratee;

        ILog _log;
        ICache _cache;

        AdresQuery _adresQuery;

        QueryCacheDecorator<AdresQuery, ZoekResult> _sut;

        [TestInitialize]
        public void Initialize()
        {
            _log = A.Fake<ILog>();
            _cache = A.Fake<ICache>();
            _decoratee = A.Fake<IQueryHandler<AdresQuery, ZoekResult>>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_Should_Throw_ArgumentNullException_When_Decorated_Is_Null()
        {
            _decoratee = null;

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
            _adresQuery = new AdresQuery("1111AA");

            ExecuteHandleOnSut();

            A.CallTo(() => _cache.GetOrAdd(A<string>.Ignored, A<Func<ZoekResult>>.Ignored))
                .MustHaveHappened();
        }

        private void CreateSut()
        {
            _sut = new QueryCacheDecorator<AdresQuery, ZoekResult>(_cache, _log, _decoratee);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            return _sut.Handle(_adresQuery);
        }
    }
}
