using System.Linq;
using Business.Entities;
using Crosscutting.Contracts;
using Business.Contracts.Query.InWhitelist;
using System.Threading;
using System.Threading.Tasks;
using Contexts.Contracts;

namespace Business.Implementation.Query.InWhitelist.Handlers
{
    public class BsnUzoviInWhitelistHandler : MediatR.IRequestHandler<BsnUzoviQuery, ZoekResult>
    {
        readonly IUnitOfWork _unitOfWork;

        public BsnUzoviInWhitelistHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public Task<ZoekResult> Handle(BsnUzoviQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ZoekResult(_unitOfWork
                    .Repository<BsnUzovi>()
                    .GetAll()
                    .Any(c => c.Bsnnummer == request.Bsnnummer && c.Uzovi == request.Uzovi)));
        }
    }
}
