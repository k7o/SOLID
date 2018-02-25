using Implementation.Query.Service;
using Infrastructure;
using System.Linq;
using Contracts;

namespace Implementation.Query.Zoek.Handlers
{
    public class BsnHandler : IQueryHandler<BsnQuery, ZoekResult>
    {
        readonly IQueryHandler<ServiceQuery, ServiceResult> _serviceHandler;

        public BsnHandler(IQueryHandler<ServiceQuery, ServiceResult> serviceHandler)
        {
            Guard.IsNotNull(serviceHandler, nameof(serviceHandler));

            _serviceHandler = serviceHandler;
        }

        public ZoekResult Handle(BsnQuery query)
        {
            return new ZoekResult(
                _serviceHandler.Handle(new ServiceQuery())
                    .BsnUzovis
                    .Any(c => c.Key == query.Bsnnummer));
        }
    }
}
