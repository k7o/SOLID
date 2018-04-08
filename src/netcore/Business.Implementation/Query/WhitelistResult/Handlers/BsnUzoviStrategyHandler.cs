using Business.Contexts;
using Business.Contracts.Query.WhitelistResult;
using Business.Entities;
using Contracts;
using Crosscutting.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Business.Implementation.Query.WhitelistResult.Handlers
{
    public class BsnUzoviStrategyHandler : IQueryStrategyHandler<GetAllBsnUzovisQuery, IEnumerable<BsnUzoviResult>>
    {
        readonly IUnitOfWork _unitOfWork;

        public BsnUzoviStrategyHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BsnUzoviResult> Handle(GetAllBsnUzovisQuery query)
        {
            Guard.IsNotNull(query, nameof(query));

            return _unitOfWork
                .Repository<BsnUzovi>()
                .GetAll()
                .Select(d => new BsnUzoviResult(d.Bsnnummer, d.Uzovi));
        }
    }
}
