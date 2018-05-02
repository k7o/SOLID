//using System;
//using Contracts;
//using FakeItEasy;
//using Crosscutting.Contracts;
//using Business.Contracts.Query.InWhitelist;
//using Xunit;
//using Crosscutting.Caches.Decorators;

//namespace BusinessLogic.UnitTests.Decorators
//{
//    public class QueryCacheDecoratorTests
//    {
//        IQueryHandler<AdresQuery, ZoekResult> _decoratee;

//        ILog _log;
//        ICache _cache;

//        AdresQuery _adresQuery;

//        QueryCacheDecorator<AdresQuery, ZoekResult> _sut;

//        public QueryCacheDecoratorTests()
//        {
//            _log = A.Fake<ILog>();
//            _cache = A.Fake<ICache>();
//            _decoratee = A.Fake<IQueryHandler<AdresQuery, ZoekResult>>();
//        }

//        [Fact]
//        public void Ctr_Should_Throw_ArgumentNullException_When_Decorated_Is_Null()
//        {
//            _decoratee = null;

//            Assert.Throws<ArgumentNullException>(() => ExecuteHandleOnSut());
//        }

//        [Fact]
//        public void Ctr_Should_Throw_ArgumentNullException_When_AppCache_Is_Null()
//        {
//            _cache = null;

//            Assert.Throws<ArgumentNullException>(() => ExecuteHandleOnSut());
//        }

//        [Fact]
//        public void Ctr_Should_Throw_ArgumentNullException_When_Logger_Handler_Is_Null()
//        {
//            _log = null;

//            Assert.Throws<ArgumentNullException>(() => ExecuteHandleOnSut());
//        }

//        [Fact]
//        public void Handle_Should_Call_GetOrAdd_On_AppCache()
//        {
//            _adresQuery = new AdresQuery("1111AA");

//            ExecuteHandleOnSut();

//            A.CallTo(() => _cache.GetOrAdd(A<string>.Ignored, A<Func<ZoekResult>>.Ignored))
//                .MustHaveHappened();
//        }

//        private void CreateSut()
//        {
//            _sut = new QueryCacheDecorator<AdresQuery, ZoekResult>(_cache, _log, _decoratee);
//        }

//        private ZoekResult ExecuteHandleOnSut()
//        {
//            CreateSut();

//            return _sut.Handle(_adresQuery);
//        }
//    }
//}
