using Business.Contracts.Query.WhitelistResult;
using Business.Entities;
using Contexts.Contracts;
using Crosscutting.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Business.Implementation.Query.GetWhitelisted.Handlers
{
    public class GetWhitelistedBsnsHandler : MediatR.IRequestHandler<GetAllBsnsQuery, IEnumerable<BsnResult>>
    {
        readonly IUnitOfWork _unitOfWork;

        public GetWhitelistedBsnsHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<BsnResult>> Handle(GetAllBsnsQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return Task.FromResult(_unitOfWork
                .Repository<Bsn>()
                .GetAll()
                .Select(d => new BsnResult(d.Bsnnummer)));
        }
    }
}
