using Contracts;
using FakeItEasy;
using Business.Entities;
using System.Collections.Generic;
using Business.Implementation.Query.Zoek.Handlers;
using Business.Contracts.Query.Zoek;
using Xunit;

namespace Business.Implementation.UnitTests.Query.Zoek.Handlers
{
    public class BsnUzoviHandlerTests
    {
        List<BsnUzovi> _bsnUzovis;
        IUnitOfWork _unitOfWork;
        IRepository<BsnUzovi> _bsnUzoviRepository;

        BsnUzoviQuery _zoekBsnUzovi;
        BsnUzoviDataHandler _sut;

        public BsnUzoviHandlerTests()
        {
            _bsnUzovis = new List<BsnUzovi>();
            _bsnUzoviRepository = A.Fake<IRepository<BsnUzovi>>();
            _unitOfWork = A.Fake<IUnitOfWork>();
        }

        [Fact]
        public void Should_Return_InWhitelist_False_When_BsnUzovi_Is_Not_Found()
        {
            _bsnUzovis.Add(new BsnUzovi(1, 2));

            _zoekBsnUzovi = new BsnUzoviQuery(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.False(result.InWhitelist);
        }

        [Fact]
        public void Should_Return_InWhitelist_True_When_BsnUzovi_Is_Found()
        {
            _bsnUzovis.Add(new BsnUzovi(1, 1));

            _zoekBsnUzovi = new BsnUzoviQuery(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.True(result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new BsnUzoviDataHandler(_unitOfWork);
        }

        private ZoekResult ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _bsnUzoviRepository.GetAll())
                .Returns(_bsnUzovis);
            A.CallTo(() => _unitOfWork.Repository<BsnUzovi>())
                .Returns(_bsnUzoviRepository);
         
            return _sut.Handle(_zoekBsnUzovi);
        }
    }
}
