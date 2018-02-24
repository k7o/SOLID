﻿using Infrastructure;
using ClassLibrary1.Query.Service;
using System.Linq;

namespace ClassLibrary1.Query.Zoek.Handlers
{
    public class AdresHandler : IQueryHandler<AdresQuery, ZoekResult>
    {
        readonly IQueryHandler<ServiceQuery, ServiceResult> _serviceHandler;

        public AdresHandler(IQueryHandler<ServiceQuery, ServiceResult> serviceHandler)
        {
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
