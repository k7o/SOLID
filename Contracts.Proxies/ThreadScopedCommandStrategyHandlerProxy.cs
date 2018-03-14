using Contracts;
using Crosscutting.Contracts;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;

namespace Contracts.Proxies
{
    public class ThreadScopedCommandStrategyHandlerProxy<TCommand> : ICommandStrategyHandler<TCommand> where TCommand : ICommand
    {
        readonly Container _container;
        readonly Func<ICommandStrategyHandler<TCommand>> _decorateeFactory;

        public ThreadScopedCommandStrategyHandlerProxy(Container container,
            Func<ICommandStrategyHandler<TCommand>> decorateeFactory)
        {
            Guard.IsNotNull(container, nameof(container));
            Guard.IsNotNull(decorateeFactory, nameof(decorateeFactory));

            _container = container;
            _decorateeFactory = decorateeFactory;
        }

        public void Handle(TCommand command)
        {
            using (ThreadScopedLifestyle.BeginScope(_container))
            {
                var handler = this._decorateeFactory.Invoke();
                handler.Handle(command);
            }
        }
    }
}
