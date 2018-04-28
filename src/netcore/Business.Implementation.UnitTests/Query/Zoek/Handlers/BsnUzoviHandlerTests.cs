using Contracts;
using FakeItEasy;
using Business.Entities;
using System.Collections.Generic;
using Business.Implementation.Query.InWhitelist.Handlers;
using Business.Contracts.Query.InWhitelist;
using Xunit;
using System.Threading.Tasks;

namespace Business.Implementation.UnitTests.Query.Zoek.Handlers
{
    public class BsnUzoviHandlerTests
    {
        readonly List<BsnUzovi> _bsnUzovis;
        readonly IUnitOfWork _unitOfWork;
        readonly IRepository<BsnUzovi> _bsnUzoviRepository;

        BsnUzoviQuery _zoekBsnUzovi;
        BsnUzoviInWhitelistHandler _sut;

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

            Assert.False(result.Result.InWhitelist);
        }

        [Fact]
        public void Should_Return_InWhitelist_True_When_BsnUzovi_Is_Found()
        {
            _bsnUzovis.Add(new BsnUzovi(1, 1));

            _zoekBsnUzovi = new BsnUzoviQuery(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.True(result.Result.InWhitelist);
        }

        private void CreateSut()
        {
            _sut = new BsnUzoviInWhitelistHandler(_unitOfWork);
        }

        private Task<ZoekResult> ExecuteHandleOnSut()
        {
            CreateSut();

            A.CallTo(() => _bsnUzoviRepository.GetAll())
                .Returns(_bsnUzovis);
            A.CallTo(() => _unitOfWork.Repository<BsnUzovi>())
                .Returns(_bsnUzoviRepository);
         
            return _sut.Handle(_zoekBsnUzovi, new System.Threading.CancellationToken());
        }
    }
}
