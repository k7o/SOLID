using Implementation.Query.Service;
using Contracts;
using Infrastructure;
using System.Linq;

namespace Implementation.Query.Zoek.Handlers
{
    public class BsnUzoviHandler : IQueryHandler<BsnUzoviQuery, ZoekResult>
    {
        readonly IQueryHandler<ServiceQuery, ServiceResult> _serviceHandler;

        public BsnUzoviHandler(IQueryHandler<ServiceQuery, ServiceResult> serviceHandler)
        {
            Guard.IsNotNull(serviceHandler, nameof(serviceHandler));

            _serviceHandler = serviceHandler;
        }

        public ZoekResult Handle(BsnUzoviQuery query)
        {
            return new ZoekResult(_serviceHandler.Handle(new ServiceQuery())
                .BsnUzovis.Any(c => c.Key == query.Bsnnummer && c.Value == query.Uzovi));
        }
    }
}
