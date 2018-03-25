using Contracts;
using Crosscutting.Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.WebApi.Controllers.Conventions;
using System.ComponentModel.DataAnnotations;

namespace Services.WebApi.Controllers
{
    [Route("api/command/[controller]")]
    [CommandControllerNameConvention]
    public class CommandController<TCommand> : Controller where TCommand : ICommand
    {
        readonly ICommandStrategyHandler<TCommand> _handler;

        public CommandController(ICommandStrategyHandler<TCommand> handler)
        {
            Guard.IsNotNull(handler, nameof(handler));

            _handler = handler;
        }

        [HttpPost]
        public IActionResult Handle([FromBody] TCommand command)
        {
            Guard.IsNotNull(command, nameof(command));

            _handler.Handle(command);

            return Ok();
        }
    }
}
