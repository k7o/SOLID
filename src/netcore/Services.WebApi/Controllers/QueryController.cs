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
        readonly ILogger _logger;

        public QueryController(IQueryStrategyHandler<TQuery, TResult> handler, ILogger logger)
        {
            Guard.IsNotNull(handler, nameof(handler));
            Guard.IsNotNull(logger, nameof(logger));

            _handler = handler;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Handle([FromQuery] TQuery query)
        {
            Guard.IsNotNull(query, nameof(query));

            _logger.LogInformation("Start");

            var result = _handler.Handle(query);

            _logger.LogInformation("Stop");

            return new JsonResult(result);
        }
    }
}
