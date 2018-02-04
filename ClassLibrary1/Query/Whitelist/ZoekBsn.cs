using System;
using System.Linq;
using ClassLibrary1.Infrastructure;

namespace ClassLibrary1
{
    public static partial class Query
    {
        public static Whitelist.ZoekQueryResult<Whitelist.ZoekBsn> Handle(
                this IQueryHandler<Whitelist.ZoekBsn, Whitelist.ZoekQueryResult<Whitelist.ZoekBsn>> handler,
                int bsnnummer)
        {
            return handler.Handle(new Whitelist.ZoekBsn(bsnnummer));
        }

        public static partial class Whitelist
        {
            public class ZoekBsn : IQuery<ZoekQueryResult<ZoekBsn>>
            {
                public int Bsnnummer { get; private set; }

                public ZoekBsn(int bsnnummer)
                {
                    Bsnnummer = bsnnummer;
                }
            }

            public static partial class Handlers
            {
                public class ZoekBsnHandler : IQueryHandler<ZoekBsn, ZoekQueryResult<ZoekBsn>>
                {
                    IQueryHandler<ServiceQuery, ServiceResult> _serviceQuery;

                    public ZoekBsnHandler(IQueryHandler<ServiceQuery, Query.Whitelist.ServiceResult> serviceQuery)
                    {
                        _serviceQuery = serviceQuery;
                    }

                    public ZoekQueryResult<ZoekBsn> Handle(ZoekBsn query)
                    {
                        return new ZoekQueryResult<ZoekBsn>(query, 
                            _serviceQuery.Handle()
                                .BsnUzovis
                                .Any(c => c.Key == query.Bsnnummer));
                    }
                }
            }
        }
    }
}
