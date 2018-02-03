﻿using ClassLibrary1.Agents;
using ClassLibrary1.Infrastructure;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public static partial class Query
    {
        public static Whitelist.ServiceResult Handle(
                this IQueryHandler<Whitelist.ServiceQuery, Whitelist.ServiceResult> handler)
        {
            return handler.Handle(new Query.Whitelist.ServiceQuery());
        }

        public static partial class Whitelist
        {

            public class ServiceQuery : IQuery<Query.Whitelist.ServiceResult>
            {
            }

            public static partial class Handlers
            {
                public class ServiceQueryHandler : IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>
                {
                    IServiceAgent _serviceAgent;

                    public ServiceQueryHandler(IServiceAgent serviceAgent)
                    {
                        _serviceAgent = serviceAgent;
                    }

                    public ServiceResult Handle(ServiceQuery query)
                    {
                        var response = _serviceAgent.Get();

                        var bsnUzovis = new Dictionary<int, short>();
                        foreach (var bsnUzovi in response.BsnUzovis)
                        {
                            bsnUzovis.Add(bsnUzovi.Bsnnummer, bsnUzovi.Uzovi);
                        }

                        return new ServiceResult(bsnUzovis, response.Adresses);
                    }
                }
            }
        }
    }
}
