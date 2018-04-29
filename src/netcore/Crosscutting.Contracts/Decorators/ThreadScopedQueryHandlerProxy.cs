using MediatR;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Crosscutting.Contracts.Decorators
{
    public class ThreadScopedQueryHandlerProxy<TQuery, TResult> : MediatR.IRequestHandler<TQuery, TResult> 
        where TQuery : IRequest<TResult>
    {
        readonly Container _container;
        readonly Func<MediatR.IRequestHandler<TQuery, TResult>> _decorateeFactory;

        public ThreadScopedQueryHandlerProxy(Container container,
            Func<MediatR.IRequestHandler<TQuery, TResult>> decorateeFactory)
        {
            Guard.IsNotNull(container, nameof(container));
            Guard.IsNotNull(decorateeFactory, nameof(decorateeFactory));

            _container = container;
            _decorateeFactory = decorateeFactory;
        }

        public Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            // Start a new scope.
            using (ThreadScopedLifestyle.BeginScope(_container))
            {
                // Create the decorateeFactory within the scope.
                MediatR.IRequestHandler<TQuery, TResult> handler = this._decorateeFactory.Invoke();
                return handler.Handle(request, cancellationToken);
            }
        }
    }
}
