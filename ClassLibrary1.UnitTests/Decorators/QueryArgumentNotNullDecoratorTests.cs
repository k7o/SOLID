using System;
using ClassLibrary1.Decorators;
using ClassLibrary1.Infrastructure;
using ClassLibrary1.Query.Zoek;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassLibrary1.UnitTests.Decorators
{
    [TestClass]
    public class QueryArgumentNotNullDecoratorTests
    {
        IQueryHandler<BsnQuery, ZoekResult> _decorated;
        
        BsnQuery _query;

        QueryArgumentNotNullDecorator<BsnQuery, ZoekResult> _sut;
        
        [TestInitialize]
        public void Initialize()
        {
            _decorated = A.Fake<IQueryHandler<BsnQuery, ZoekResult>>();
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
            _query = new BsnQuery(1);

            ExecuteHandleOnSut();

            A.CallTo(() => 
                _decorated.Handle(A<BsnQuery>.That.IsEqualTo(_query)))
                .MustHaveHappened();
        }

        private void CreateSut()
        {
            _sut = new QueryArgumentNotNullDecorator<BsnQuery, ZoekResult>(_decorated);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _decorated.Handle(A<BsnQuery>.Ignored)).Returns(new ZoekResult(true));

            return _sut.Handle(_query);
        }

    }
}
