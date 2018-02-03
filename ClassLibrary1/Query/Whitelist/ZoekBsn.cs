using System;
using System.Linq;
using ClassLibrary1.Infrastructure;

namespace ClassLibrary1
{
    public static partial class Query
    {
        public static Whitelist.ZoekQueryResult Handle(
                this IQueryHandler<Whitelist.ZoekBsn, Whitelist.ZoekQueryResult> handler,
                int bsnnummer)
        {
            return handler.Handle(new Whitelist.ZoekBsn(bsnnummer));
        }

        public static partial class Whitelist
        {
            public class ZoekBsn : IQuery<Query.Whitelist.ZoekQueryResult>
            {
                public int Bsnnummer { get; private set; }

                public ZoekBsn(int bsnnummer)
                {
                    Bsnnummer = bsnnummer;
                }
            }

            public static partial class Handlers
            {
                public class ZoekBsnHandler : IQueryHandler<Query.Whitelist.ZoekBsn, Query.Whitelist.ZoekQueryResult>
                {
                    IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult> _serviceQuery;

                    public ZoekBsnHandler(IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult> serviceQuery)
                    {
                        _serviceQuery = serviceQuery;
                    }

                    public ZoekQueryResult Handle(Query.Whitelist.ZoekBsn query)
                    {
                        var result = _serviceQuery.Handle()
                                .BsnUzovis
                                .Any(c => c.Key == query.Bsnnummer);
                        
                        return new ZoekQueryResult(query.Bsnnummer, result);
                    }
                }
            }
        }
    }
}
