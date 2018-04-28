using Contracts;
using Crosscutting.Contracts;
using MediatR;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Crosscutting.Contracts.Decorators
{
    public class ThreadScopedCommandHandlerProxy<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
    {
        readonly Container _container;
        readonly Func<IRequestHandler<TRequest>> _decorateeFactory;

        public ThreadScopedCommandHandlerProxy(Container container,
            Func<IRequestHandler<TRequest>> decorateeFactory)
        {
            Guard.IsNotNull(container, nameof(container));
            Guard.IsNotNull(decorateeFactory, nameof(decorateeFactory));

            _container = container;
            _decorateeFactory = decorateeFactory;
        }

        public Task Handle(TRequest request, CancellationToken cancellationToken)
        {
            using (ThreadScopedLifestyle.BeginScope(_container))
            {
                var handler = this._decorateeFactory.Invoke();
                handler.Handle(request, cancellationToken);
            }
            return Task.CompletedTask;
        }
    }
}
