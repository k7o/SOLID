using Contracts;
using System.Linq;
using Business.Entities;
using Crosscutting.Contracts;
using Business.Contracts.Query.Zoek;

namespace Business.Implementation.Query.Zoek.Handlers
{
    public class BsnUzoviDataHandler : IDataQueryHandler<BsnUzoviQuery, ZoekResult>
    {
        readonly IUnitOfWork _unitOfWork;

        public BsnUzoviDataHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public ZoekResult Handle(BsnUzoviQuery query)
        {
            return new ZoekResult(
                _unitOfWork.Repository<BsnUzovi>()
                    .GetAll()
                    .Any(c => c.Bsnnummer == query.Bsnnummer && c.Uzovi == query.Uzovi));
        }
    }
}
