using System.Linq;
using Business.Contracts.Query.InWhitelist;
using Contracts;
using Crosscutting.Contracts;
using Business.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Implementation.Query.InWhitelist.Handlers
{
    public class AdresInWhitelistHandler : IRequestHandler<AdresQuery, ZoekResult>
    {
        readonly IUnitOfWork _unitOfWork;

        public AdresInWhitelistHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public Task<ZoekResult> Handle(AdresQuery request, CancellationToken cancellationToken)
        {
            return new Task<ZoekResult>(() => new ZoekResult(_unitOfWork
                    .Repository<Adres>()
                    .GetAll()
                    .Any(c => c.Postcode == request.Postcode)));
        }
    }
    
}
