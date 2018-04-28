using Contracts;
using Crosscutting.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.WebApi.Controllers.Conventions;

namespace Services.WebApi.Controllers
{
    [Route("api/query/[controller]")]
    [QueryControllerNameConvention]
    public class QueryController<TQuery, TResult> : Controller where TQuery : IRequest<TResult>
    {
        readonly IMediator _mediator;

        public QueryController(IMediator mediator)
        {
            Guard.IsNotNull(mediator, nameof(mediator));

            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Handle([FromQuery] TQuery query)
        {
            Guard.IsNotNull(query, nameof(query));

            var result = _mediator.Send(query);
            result.Start();
            return new JsonResult(result.Result);
        }
    }
}
