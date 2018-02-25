using Infrastructure;
using Implementation.Query.Service;
using System.Linq;
using Contracts;

namespace Implementation.Query.Zoek.Handlers
{
    public class AdresHandler : IQueryHandler<AdresQuery, ZoekResult>
    {
        readonly IQueryHandler<ServiceQuery, ServiceResult> _serviceHandler;

        public AdresHandler(IQueryHandler<ServiceQuery, ServiceResult> serviceHandler)
        {
            Guard.IsNotNull(serviceHandler, nameof(serviceHandler));

            _serviceHandler = serviceHandler;
        }

        public ZoekResult Handle(AdresQuery query)
        {
            return new ZoekResult(_serviceHandler
                .Handle(new ServiceQuery())
                .Adresses.Any(c => c == query.Postcode));
        }
    }
}
