using Contracts;
using Crosscutting.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosscutting.Loggers.Decorators
{
    public class CommandStrategyHandlerTraceDecorator<TCommand> : ICommandStrategyHandler<TCommand> where TCommand : ICommand
    {
        readonly ICommandStrategyHandler<TCommand> _decoratee;
        readonly ITrace _tracer;

        public CommandStrategyHandlerTraceDecorator(ITrace tracer, ICommandStrategyHandler<TCommand> decoratee)
        {
            Guard.IsNotNull(decoratee, nameof(decoratee));
            Guard.IsNotNull(tracer, nameof(tracer));

            _decoratee = decoratee;
            _tracer = tracer;
        }

        public void Handle(TCommand command)
        {
            _tracer.Execute();

            try
            {
                _decoratee.Handle(command);
            } 
            finally
            { 
                _tracer.Executed(JsonConvert.SerializeObject(command));
            }
        }
    }
}
