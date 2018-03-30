using Contracts;
using Crosscutting.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.WebApi.Controllers.Conventions;

namespace Services.WebApi.Controllers
{
    [Route("api/query/[controller]")]
    [QueryControllerNameConvention]
    public class QueryController<TQuery, TResult> : Controller where TQuery : IQuery<TResult>
    {
        readonly IQueryStrategyHandler<TQuery, TResult> _handler;

        public QueryController(IQueryStrategyHandler<TQuery, TResult> handler)
        {
            Guard.IsNotNull(handler, nameof(handler));

            _handler = handler;
        }

        [HttpGet]
        public IActionResult Handle([FromQuery] TQuery query)
        {
            Guard.IsNotNull(query, nameof(query));

            var result = _handler.Handle(query);

            return new JsonResult(result);
        }
    }
}
