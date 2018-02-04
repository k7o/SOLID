using ClassLibrary1.Infrastructure;
using System.Linq;

namespace ClassLibrary1
{
    public static partial class Query
    {
        public static Whitelist.ZoekResult<Whitelist.ZoekAdres> Handle(
               this IQueryHandler<Whitelist.ZoekAdres, Whitelist.ZoekResult<Whitelist.ZoekAdres>> handler,
               string adres)
        {
            return handler.Handle(new Whitelist.ZoekAdres(adres));
        }

        public static partial class Whitelist
        {
            public class ZoekAdres : IQuery<ZoekResult<ZoekAdres>>
            {
                public string Adres { get; private set; }

                public ZoekAdres(string adres)
                {
                    Adres = adres;
                }
            }

            public static partial class Handlers
            {
                public class ZoekAdresHandler : IQueryHandler<ZoekAdres, ZoekResult<ZoekAdres>>
                {
                    IQueryHandler<ServiceQuery, ServiceResult> _serviceHandler;

                    public ZoekAdresHandler(IQueryHandler<ServiceQuery, ServiceResult> serviceHandler)
                    {
                        _serviceHandler = serviceHandler;
                    }

                    public ZoekResult<ZoekAdres> Handle(ZoekAdres query)
                    {
                        return new ZoekResult<ZoekAdres>(query, 
                            _serviceHandler.Handle().Adresses.Any(c => c == query.Adres));
                    }
                }
            }
        }
    }
}
