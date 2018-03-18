using Contracts;
using FakeItEasy;
using Business.Entities;
using System.Collections.Generic;
using Business.Implementation.Query.Zoek.Handlers;
using Business.Contracts.Query.Zoek;
using Xunit;

namespace Business.Implementation.UnitTests.Query.Zoek.Handlers
{
    public class BsnHandlerTests
    {
        BsnQuery _zoekBsn;

        List<Bsn> _bsns;
        IRepository<Bsn> _bsnRepository;
        IUnitOfWork _unitOfWork;

        BsnDataHandler _sut;

        public BsnHandlerTests()
        {
            _bsns = new List<Bsn>();
             _bsnRepository = A.Fake<IRepository<Bsn>>();

            _unitOfWork = A.Fake<IUnitOfWork>();
        }

        [Fact]
        public void Should_Return_InWhitelist_False_When_Bsn_Is_Not_Found()
        {
            _bsns.Add(new Bsn(2));

            _zoekBsn = new BsnQuery(1);

            var result = ExecuteHandleOnSut();

            Assert.False(result.InWhitelist);
        }

        [Fact]
        public void Should_Return_InWhitelist_True_When_Bsn_Is_Found()
        {
            _bsns.Add(new Bsn(1));

            _zoekBsn = new BsnQuery(1);

            var result = ExecuteHandleOnSut();

            Assert.True(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new BsnDataHandler(_unitOfWork);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _bsnRepository.GetAll())
               .Returns(_bsns);
            A.CallTo(() => _unitOfWork.Repository<Bsn>())
                .Returns(_bsnRepository);

            return _sut.Handle(_zoekBsn);
        }
    }
}
