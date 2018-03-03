using System.Linq;
using Contracts;
using Entities;
using Crosscutting.Contracts;

namespace Implementation.Query.Zoek.Handlers
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
