using Contracts;
using Implementation.Query.Zoek;
using Implementation.Query.Zoek.Handlers;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using System.Collections.Generic;

namespace Implementation.UnitTests.Query.Zoek.Handlers
{
    [TestClass]
    public class BsnUzoviHandlerTests
    {
        List<BsnUzovi> _bsnUzovis;
        IUnitOfWork _unitOfWork;
        IRepository<BsnUzovi> _bsnUzoviRepository;

        BsnUzoviQuery _zoekBsnUzovi;
        BsnUzoviDataHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _bsnUzovis = new List<BsnUzovi>();
            _bsnUzoviRepository = A.Fake<IRepository<BsnUzovi>>();
            _unitOfWork = A.Fake<IUnitOfWork>();
        }

        [TestMethod]
        public void Should_Return_InWhitelist_False_When_BsnUzovi_Is_Not_Found()
        {
            _bsnUzovis.Add(new BsnUzovi(1, 2));

            _zoekBsnUzovi = new BsnUzoviQuery(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.IsFalse(result.InWhitelist);
        }

        [TestMethod]
        public void Should_Return_InWhitelist_True_When_BsnUzovi_Is_Found()
        {
            _bsnUzovis.Add(new BsnUzovi(1, 1));

            _zoekBsnUzovi = new BsnUzoviQuery(1, 1);

            var result = ExecuteHandleOnSut();

            Assert.IsTrue(result.InWhitelist);
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
