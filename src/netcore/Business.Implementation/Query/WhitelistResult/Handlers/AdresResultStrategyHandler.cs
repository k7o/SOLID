using Business.Contexts;
using Business.Contracts.Query.WhitelistResult;
using Business.Entities;
using Contracts;
using Crosscutting.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Business.Implementation.Query.WhitelistResult.Handlers
{
    public class AdresResultStrategyHandler : IQueryStrategyHandler<GetAllAdressenQuery, IEnumerable<AdresResult>>
    {
        readonly IUnitOfWork _unitOfWork;

        public AdresResultStrategyHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AdresResult> Handle(GetAllAdressenQuery query)
        {
            Guard.IsNotNull(query, nameof(query));

            return _unitOfWork
                .Repository<Adres>()
                .GetAll()
                .Select(d => new AdresResult(d.Postcode));
        }
    }
}
