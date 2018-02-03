using ClassLibrary1.Infrastructure;
using System.Linq;

namespace ClassLibrary1
{
    public static partial class Query
    {
        public static Whitelist.ZoekQueryResult Handle(
               this IQueryHandler<Whitelist.ZoekAdres, Whitelist.ZoekQueryResult> handler,
               int bsnnummer, string adres)
        {
            return handler.Handle(new Whitelist.ZoekAdres(bsnnummer, adres));
        }

        public static partial class Whitelist
        {
            public class ZoekAdres : IQuery<Query.Whitelist.ZoekQueryResult>
            {
                public int Bsnnummer { get; private set; }
                public string Adres { get; private set; }

                public ZoekAdres(int bsnnummer, string adres)
                {
                    Bsnnummer = bsnnummer;
                    Adres = adres;
                }
            }

            public static partial class Handlers
            {
                public class ZoekAdresHandler : IQueryHandler<Query.Whitelist.ZoekAdres, Query.Whitelist.ZoekQueryResult>
                {
                    IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult> _serviceHandler;

                    public ZoekAdresHandler(IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult> serviceHandler)
                    {
                        _serviceHandler = serviceHandler;
                    }

                    public ZoekQueryResult Handle(Query.Whitelist.ZoekAdres query)
                    {
                        return new ZoekQueryResult(0, 
                            _serviceHandler.Handle().Adresses.Any(c => c == query.Adres));
                    }
                }
            }
        }
    }
}
