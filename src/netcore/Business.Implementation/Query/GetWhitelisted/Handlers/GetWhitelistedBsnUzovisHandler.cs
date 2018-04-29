using Business.Contracts.Query.WhitelistResult;
using Business.Implementation.Entities;
using Contexts.Contracts;
using Crosscutting.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Implementation.Query.GetWhitelisted.Handlers
{
    public class GetWhitelistedBsnUzovisHandler : MediatR.IRequestHandler<GetAllBsnUzovisQuery, IEnumerable<BsnUzoviResult>>
    {
        readonly IUnitOfWork _unitOfWork;

        public GetWhitelistedBsnUzovisHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<BsnUzoviResult>> Handle(GetAllBsnUzovisQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return Task.FromResult(_unitOfWork
                .Repository<BsnUzovi>()
                .GetAll()
                .Select(d => new BsnUzoviResult(d.Bsnnummer, d.Uzovi)));
        }
    }
}
