using Contracts;
using System.Linq;
using Business.Entities;
using Crosscutting.Contracts;
using Business.Contracts.Query.InWhitelist;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Implementation.Query.InWhitelist.Handlers
{
    public class BsnUzoviInWhitelistHandler : IRequestHandler<BsnUzoviQuery, ZoekResult>
    {
        readonly IUnitOfWork _unitOfWork;

        public BsnUzoviInWhitelistHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public Task<ZoekResult> Handle(BsnUzoviQuery request, CancellationToken cancellationToken)
        {
            return new Task<ZoekResult>(() =>
                new ZoekResult(_unitOfWork
                    .Repository<BsnUzovi>()
                    .GetAll()
                    .Any(c => c.Bsnnummer == request.Bsnnummer && c.Uzovi == request.Uzovi)));
        }
    }
}
