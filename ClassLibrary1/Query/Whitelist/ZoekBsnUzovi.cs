using System.Linq;
using ClassLibrary1.Infrastructure;

namespace ClassLibrary1
{
    public static partial class Query
    {
        public static Whitelist.ZoekQueryResult<Whitelist.ZoekBsnUzovi> Handle(
               this IQueryHandler<Whitelist.ZoekBsnUzovi, Whitelist.ZoekQueryResult<Whitelist.ZoekBsnUzovi>> handler,
               int bsnnummer, short uzovi)
        {
            return handler.Handle(new Whitelist.ZoekBsnUzovi(bsnnummer, uzovi));
        }

        public static partial class Whitelist
        {
            public class ZoekBsnUzovi : IQuery<ZoekQueryResult<ZoekBsnUzovi>>
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
                public class ZoekBsnUzoviHandler : IQueryHandler<ZoekBsnUzovi, ZoekQueryResult<ZoekBsnUzovi>>
                {
                    IQueryHandler<ServiceQuery, ServiceResult> _serviceQuery;

                    public ZoekBsnUzoviHandler(IQueryHandler<ServiceQuery, ServiceResult> serviceQuery)
                    {
                        _serviceQuery = serviceQuery;
                    }

                    public ZoekQueryResult<ZoekBsnUzovi> Handle(ZoekBsnUzovi query)
                    {
                        return new ZoekQueryResult<ZoekBsnUzovi>(query, 
                            _serviceQuery.Handle().BsnUzovis.Any(c => c.Key == query.Bsnnummer && c.Value == query.Uzovi));
                    }
                }
            }
        }
    }
}
