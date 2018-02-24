﻿using Infrastructure;
using ClassLibrary1.Query.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Query.Zoek.Handlers
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
