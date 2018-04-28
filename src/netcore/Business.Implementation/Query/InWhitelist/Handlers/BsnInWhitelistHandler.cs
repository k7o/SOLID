using System.Linq;
using Contracts;
using Business.Entities;
using Crosscutting.Contracts;
using Business.Contracts.Query.InWhitelist;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Implementation.Query.InWhitelist.Handlers
{
    public class BsnInWhitelistHandler : MediatR.IRequestHandler<BsnQuery, ZoekResult>
    {
        readonly IUnitOfWork _unitOfWork;

        public BsnInWhitelistHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork  = unitOfWork;
        }

        public Task<ZoekResult> Handle(BsnQuery request, CancellationToken cancellationToken)
        {
            return new Task<ZoekResult>(() =>
                new ZoekResult(_unitOfWork
                    .Repository<Bsn>()
                    .GetAll()
                    .Any(c => c.Bsnnummer == request.Bsnnummer)));
        }
    }
}
