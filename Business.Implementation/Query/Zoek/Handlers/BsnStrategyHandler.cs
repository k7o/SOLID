using Business.Contracts.Query.Zoek;
using Contracts;
using Crosscutting.Contracts;

namespace Business.Implementation.Query.Zoek.Handlers
{
    public class BsnStrategyHandler : IQueryStrategyHandler<BsnQuery, ZoekResult>
    {
        readonly IDataQueryHandler<BsnQuery, ZoekResult> _queryHandler;

        public BsnStrategyHandler(IDataQueryHandler<BsnQuery, ZoekResult> queryHandler)
        {
            Guard.IsNotNull(queryHandler, "queryHandler");

            _queryHandler = queryHandler;
        }

        public ZoekResult Handle(BsnQuery query)
        {
            return _queryHandler.Handle(query);
        }
    }
}
