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

            return new Task<IEnumerable<BsnResult>>(() =>_unitOfWork
                .Repository<Bsn>()
                .GetAll()
                .Select(d => new BsnResult(d.Bsnnummer)));
        }
    }
}
