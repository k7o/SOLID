using Business.Contracts.Query.Zoek;
using Contracts;
using Crosscutting.Contracts;

namespace Business.Implementation.Query.Zoek.Handlers
{
    public class AdresStrategyHandler : IQueryStrategyHandler<AdresQuery, ZoekResult>
    {
        readonly IDataQueryHandler<AdresQuery, ZoekResult> _queryHandler;

        public AdresStrategyHandler(IDataQueryHandler<AdresQuery, ZoekResult> queryHandler)
        {
            Guard.IsNotNull(queryHandler, nameof(queryHandler));

            _queryHandler = queryHandler;
        }

        public ZoekResult Handle(AdresQuery query)
        {
            return _queryHandler.Handle(query);
        }
    }
}
