using Contracts;
using Crosscutting.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.WebApi.Controllers.Conventions;
using System.ComponentModel.DataAnnotations;

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
        public IActionResult Handle([FromBody] TCommand command)
        {
            Guard.IsNotNull(command, nameof(command));

            _mediator.Send(command);

            return Ok();
        }
    }
}
