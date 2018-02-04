using System;
using System.Linq;
using ClassLibrary1.Infrastructure;

namespace ClassLibrary1
{
    public static partial class Query
    {
        public static Whitelist.ZoekResult<Whitelist.ZoekBsn> Handle(
                this IQueryHandler<Whitelist.ZoekBsn, Whitelist.ZoekResult<Whitelist.ZoekBsn>> handler,
                int bsnnummer)
        {
            return handler.Handle(new Whitelist.ZoekBsn(bsnnummer));
        }

        public static partial class Whitelist
        {
            public class ZoekBsn : IQuery<ZoekResult<ZoekBsn>>
            {
                public int Bsnnummer { get; private set; }

                public ZoekBsn(int bsnnummer)
                {
                    Bsnnummer = bsnnummer;
                }
            }

            public static partial class Handlers
            {
                public class ZoekBsnHandler : IQueryHandler<ZoekBsn, ZoekResult<ZoekBsn>>
                {
                    IQueryHandler<ServiceQuery, ServiceResult> _serviceQuery;

                    public ZoekBsnHandler(IQueryHandler<ServiceQuery, Query.Whitelist.ServiceResult> serviceQuery)
                    {
                        _serviceQuery = serviceQuery;
                    }

                    public ZoekResult<ZoekBsn> Handle(ZoekBsn query)
                    {
                        return new ZoekResult<ZoekBsn>(query, 
                            _serviceQuery.Handle()
                                .BsnUzovis
                                .Any(c => c.Key == query.Bsnnummer));
                    }
                }
            }
        }
    }
}
