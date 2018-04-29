using System.Linq;
using Business.Contracts.Query.InWhitelist;
using Crosscutting.Contracts;
using Business.Entities;
using System.Threading;
using System.Threading.Tasks;
using Contexts.Contracts;

namespace Business.Implementation.Query.InWhitelist.Handlers
{
    public class AdresInWhitelistHandler : MediatR.IRequestHandler<AdresQuery, ZoekResult>
    {
        readonly IUnitOfWork _unitOfWork;

        public AdresInWhitelistHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public Task<ZoekResult> Handle(AdresQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ZoekResult(_unitOfWork
                    .Repository<Adres>()
                    .GetAll()
                    .Any(c => c.Postcode == request.Postcode)));
        }
    }
    
}
