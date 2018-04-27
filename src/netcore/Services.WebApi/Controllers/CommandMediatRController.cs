using Crosscutting.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.WebApi.Controllers.Conventions;

namespace Services.WebApi.Controllers
{
    [Route("api/commandmediatr/[controller]")]
    [CommandMediatRControllerNameConvention]
    public class CommandMediatRController<TCommand> : Controller where TCommand : IRequest
    {
        readonly IRequestHandler<TCommand> _handler;

        public CommandMediatRController(IRequestHandler<TCommand> handler)
        {
            Guard.IsNotNull(handler, nameof(handler));

            _handler = handler;
        }

        [HttpPost]
        public IActionResult Handle([FromBody] TCommand command)
        {
            Guard.IsNotNull(command, nameof(command));

            _handler.Handle(command, new System.Threading.CancellationToken());

            return Ok();
        }
    }
}
