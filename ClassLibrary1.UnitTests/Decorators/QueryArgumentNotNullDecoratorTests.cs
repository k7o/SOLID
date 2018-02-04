using System;
using ClassLibrary1.Decorators;
using ClassLibrary1.Infrastructure;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ClassLibrary1.Query.Whitelist;

namespace ClassLibrary1.UnitTests.Decorators
{
    [TestClass]
    public class QueryArgumentNotNullDecoratorTests
    {
        IQueryHandler<ZoekBsn, ZoekResult<ZoekBsn>> _decorated;
        
        ZoekBsn _query;

        QueryArgumentNotNullDecorator<ZoekBsn, ZoekResult<ZoekBsn>> _sut;
        
        [TestInitialize]
        public void Initialize()
        {
            _decorated = A.Fake<IQueryHandler<ZoekBsn, ZoekResult<ZoekBsn>>>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Çtr_Should_Throw_ArgumentNullException_When_Decorated_Handler_Is_Null()
        {
            _decorated = null;

            ExecuteHandleOnSut();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Handle_Should_Throw_ArgumentNullException_When_Query_Is_Null()
        {
            _query = null;

            ExecuteHandleOnSut();
        }

        [TestMethod]
        public void Handle_Should_Call_Handle_With_Query_On_Decorated_Handler_When_Argument_Is_Not_Null()
        {
            _query = new ZoekBsn(1);

            ExecuteHandleOnSut();

            A.CallTo(() => 
                _decorated.Handle(A<ZoekBsn>.That.IsEqualTo(_query)))
                .MustHaveHappened();
        }

        private void CreateSut()
        {
            _sut = new QueryArgumentNotNullDecorator<ZoekBsn, ZoekResult<ZoekBsn>>(_decorated);
        }

        private ZoekResult<ZoekBsn> ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _decorated.Handle(A<ZoekBsn>.Ignored)).Returns(new ZoekResult<ZoekBsn>(_query, true));

            return _sut.Handle(_query);
        }

    }
}
