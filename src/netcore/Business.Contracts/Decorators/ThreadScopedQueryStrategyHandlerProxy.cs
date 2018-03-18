using Contracts;
using Crosscutting.Contracts;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;

namespace Business.Contracts.Decorators
{
    public class ThreadScopedQueryStrategyHandlerProxy<TQuery, TResult> : IQueryStrategyHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        readonly Container _container;
        readonly Func<IQueryStrategyHandler<TQuery, TResult>> _decorateeFactory;

        public ThreadScopedQueryStrategyHandlerProxy(Container container,
            Func<IQueryStrategyHandler<TQuery, TResult>> decorateeFactory)
        {
            Guard.IsNotNull(container, nameof(container));
            Guard.IsNotNull(decorateeFactory, nameof(decorateeFactory));

            _container = container;
            _decorateeFactory = decorateeFactory;
        }

        public TResult Handle(TQuery query)
        {
            // Start a new scope.
            using (ThreadScopedLifestyle.BeginScope(_container))
            {
                // Create the decorateeFactory within the scope.
                IQueryStrategyHandler<TQuery, TResult> handler = this._decorateeFactory.Invoke();
                return handler.Handle(query);
            }
        }
    }
}
