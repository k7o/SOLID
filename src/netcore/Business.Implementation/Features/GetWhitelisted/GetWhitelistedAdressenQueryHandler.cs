using Business.Context;
using BusinessLogic.Entities;
using Crosscutting.Contracts;
using Dtos.Features.GetWhitelisted;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic.Features.GetWhitelisted
{
    public class GetWhitelistedAdressenQueryHandler : MediatR.IRequestHandler<GetWhitelistedAdressenQuery, IEnumerable<AdresResult>>
    {
        readonly WhitelistContext _context;

        public GetWhitelistedAdressenQueryHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public Task<IEnumerable<AdresResult>> Handle(GetWhitelistedAdressenQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return Task.FromResult(_context
                .Repository<Adres>()
                .GetAll()
                .Select(d => new AdresResult(d.Postcode)));
        }
    }
}
