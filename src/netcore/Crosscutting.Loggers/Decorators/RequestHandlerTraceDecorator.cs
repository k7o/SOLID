using Contracts;
using Crosscutting.Contracts;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crosscutting.Loggers.Decorators
{
    public class RequestHandlerTraceDecorator<TCommand> : IRequestHandler<TCommand> 
        where TCommand : IRequest
    {
        readonly IRequestHandler<TCommand> _decoratee;
        readonly ITrace _tracer;

        public RequestHandlerTraceDecorator(ITrace tracer, IRequestHandler<TCommand> decoratee)
        {
            Guard.IsNotNull(decoratee, nameof(decoratee));
            Guard.IsNotNull(tracer, nameof(tracer));

            _decoratee = decoratee;
            _tracer = tracer;
        }

        public Task Handle(TCommand request, CancellationToken cancellationToken)
        {
            _tracer.Execute();

            try
            {
                _decoratee.Handle(request, cancellationToken);
            }
            finally
            {
                _tracer.Executed(JsonConvert.SerializeObject(request));
            }

            return Task.CompletedTask;
        }
    }
}
