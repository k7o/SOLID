﻿using ClassLibrary1.Query.Service;
using Infrastructure;
using System.Linq;

namespace ClassLibrary1.Query.Zoek.Handlers
{
    public class BsnHandler : IQueryHandler<BsnQuery, ZoekResult>
    {
        readonly IQueryHandler<ServiceQuery, ServiceResult> _serviceQueryHandler;

        public BsnHandler(IQueryHandler<ServiceQuery, ServiceResult> serviceQueryHandler)
        {
            _serviceQueryHandler = serviceQueryHandler;
        }

        public ZoekResult Handle(BsnQuery query)
        {
            return new ZoekResult(
                _serviceQueryHandler.Handle(new ServiceQuery())
                    .BsnUzovis
                    .Any(c => c.Key == query.Bsnnummer));
        }
    }
}
