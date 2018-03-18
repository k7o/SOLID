using Business.Contracts.Query.Zoek;
using Contracts;
using Crosscutting.Contracts;

namespace Business.Implementation.Query.Zoek.Handlers
{
    public class BsnUzoviStrategyHandler : IQueryStrategyHandler<BsnUzoviQuery, ZoekResult>
    {
        readonly IDataQueryHandler<BsnUzoviQuery, ZoekResult> _queryHandler;

        public BsnUzoviStrategyHandler(IDataQueryHandler<BsnUzoviQuery, ZoekResult> queryHandler)
        {
            Guard.IsNotNull(queryHandler, "queryHandler");

            _queryHandler = queryHandler;
        }

        public ZoekResult Handle(BsnUzoviQuery query)
        {
            return _queryHandler.Handle(query);
        }
    }
}
