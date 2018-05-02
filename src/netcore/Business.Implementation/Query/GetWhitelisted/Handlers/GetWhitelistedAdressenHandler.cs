using Business.Context;
using BusinessLogic.Entities;
using Contexts.Contracts;
using Crosscutting.Contracts;
using Dtos.Query.WhitelistResult;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic.Query.GetWhitelisted.Handlers
{
    public class GetWhitelistedAdressenHandler : MediatR.IRequestHandler<GetAllAdressenQuery, IEnumerable<AdresResult>>
    {
        readonly WhitelistContext _context;

        public GetWhitelistedAdressenHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public Task<IEnumerable<AdresResult>> Handle(GetAllAdressenQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return Task.FromResult(_context
                .Repository<Adres>()
                .GetAll()
                .Select(d => new AdresResult(d.Postcode)));
        }
    }
}
