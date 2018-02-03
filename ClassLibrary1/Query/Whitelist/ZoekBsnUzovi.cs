using System.Linq;
using ClassLibrary1.Infrastructure;

namespace ClassLibrary1
{
    public static partial class Query
    {
        public static Whitelist.ZoekQueryResult Handle(
               this IQueryHandler<Whitelist.ZoekBsnUzovi, Whitelist.ZoekQueryResult> handler,
               int bsnnummer, short uzovi)
        {
            return handler.Handle(new Whitelist.ZoekBsnUzovi(bsnnummer, uzovi));
        }

        public static partial class Whitelist
        {
            public class ZoekBsnUzovi : IQuery<Query.Whitelist.ZoekQueryResult>
            {
                public int Bsnnummer { get; private set; }
                public short Uzovi { get; private set; }

                public ZoekBsnUzovi(int bsnnummer, short uzovi)
                {
                    Bsnnummer = bsnnummer;
                    Uzovi = uzovi;
                }
            }
            public static partial class Handlers
            {
                public class ZoekBsnUzoviHandler : IQueryHandler<Query.Whitelist.ZoekBsnUzovi, Query.Whitelist.ZoekQueryResult>
                {
                    IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult> _serviceQuery;

                    public ZoekBsnUzoviHandler(IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult> serviceQuery)
                    {
                        _serviceQuery = serviceQuery;
                    }

                    public ZoekQueryResult Handle(Query.Whitelist.ZoekBsnUzovi query)
                    {
                        return new ZoekQueryResult(query.Bsnnummer, 
                            _serviceQuery.Handle().BsnUzovis.Any(c => c.Key == query.Bsnnummer && c.Value == query.Uzovi));
                    }
                }
            }
        }
    }
}
