using Crosscutting.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.WebApi.Controllers.Conventions;
using System.Threading.Tasks;

namespace Services.WebApi.Controllers
{
    [Route("api/query/[controller]")]
    [QueryControllerNameConvention]
    public class QueryController<TQuery, TResult> : Controller where TQuery : class, IRequest<TResult>
    {
        readonly IMediator _mediator;

        public QueryController(IMediator mediator)
        {
            Guard.IsNotNull(mediator, nameof(mediator));

            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> HandleAsync([FromQuery] TQuery query)
        {
            Guard.IsNotNull(query, nameof(query));

            var queryDelegate = await _mediator.Send(query);

            return new JsonResult(queryDelegate);
        }
    }
}
