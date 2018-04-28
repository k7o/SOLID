using Business.Contexts;
using Business.Contracts.Query.WhitelistResult;
using Business.Entities;
using Contracts;
using Crosscutting.Contracts;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Implementation.Query.GetWhitelisted.Handlers
{
    public class GetWhitelistedAdressenHandler : IRequestHandler<GetAllAdressenQuery, IEnumerable<AdresResult>>
    {
        readonly IUnitOfWork _unitOfWork;

        public GetWhitelistedAdressenHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<AdresResult>> Handle(GetAllAdressenQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return new Task<IEnumerable<AdresResult>>(() => _unitOfWork
                .Repository<Adres>()
                .GetAll()
                .Select(d => new AdresResult(d.Postcode)));
        }
    }
}
