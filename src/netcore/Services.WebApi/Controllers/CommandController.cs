using Crosscutting.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.WebApi.Controllers.Conventions;
using System.Threading.Tasks;

namespace Services.WebApi.Controllers
{
    [Route("api/command/[controller]")]
    [CommandControllerNameConvention]
    public class CommandController<TCommand> : Controller where TCommand : IRequest
    {
        readonly IMediator _mediator;

        public CommandController(IMediator mediator)
        {
            Guard.IsNotNull(mediator, nameof(mediator));

            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> HandleAsync([FromBody] TCommand command)
        {
            Guard.IsNotNull(command, nameof(command));

            await _mediator.Send(command);

            return Ok();
        }
    }
}
