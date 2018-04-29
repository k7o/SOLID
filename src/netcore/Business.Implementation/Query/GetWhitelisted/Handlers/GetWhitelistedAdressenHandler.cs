using Business.Contracts.Query.WhitelistResult;
using Business.Entities;
using Contexts.Contracts;
using Crosscutting.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Implementation.Query.GetWhitelisted.Handlers
{
    public class GetWhitelistedAdressenHandler : MediatR.IRequestHandler<GetAllAdressenQuery, IEnumerable<AdresResult>>
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

            return Task.FromResult(_unitOfWork
                .Repository<Adres>()
                .GetAll()
                .Select(d => new AdresResult(d.Postcode)));
        }
    }
}
