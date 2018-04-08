using Business.Contexts;
using Business.Contracts.Query.WhitelistResult;
using Business.Entities;
using Contracts;
using Crosscutting.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Business.Implementation.Query.WhitelistResult.Handlers
{
    public class BsnResultStrategyHandler : IQueryStrategyHandler<GetAllBsnsQuery, IEnumerable<BsnResult>>
    {
        readonly IUnitOfWork _unitOfWork;

        public BsnResultStrategyHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BsnResult> Handle(GetAllBsnsQuery query)
        {
            Guard.IsNotNull(query, nameof(query));

            return _unitOfWork
                .Repository<Bsn>()
                .GetAll()
                .Select(d => new BsnResult(d.Bsnnummer));
        }
    }
}
