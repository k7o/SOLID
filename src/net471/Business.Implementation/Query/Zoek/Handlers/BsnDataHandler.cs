using System.Linq;
using Contracts;
using Crosscutting.Contracts;
using Business.Contracts.Query.Zoek;
using Business.Entities;

namespace Business.Implementation.Query.Zoek.Handlers
{
    public class BsnDataHandler : IDataQueryHandler<BsnQuery, ZoekResult>
    {
        readonly IUnitOfWork _unitOfWork;

        public BsnDataHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork  = unitOfWork;
        }

        public ZoekResult Handle(BsnQuery query)
        {
            return new ZoekResult(
                _unitOfWork.Repository<Bsn>().GetAll()
                    .Any(c => c.Bsnnummer == query.Bsnnummer));
        }
    }
}
