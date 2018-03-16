namespace Clients.Wcf.ConsoleApp
{
    using Clients.Wcf.ConsoleApp.Code;
    using Clients.Wcf.ConsoleApp.Controllers;
    using Clients.Wcf.ConsoleApp.CrossCuttingConcerns;
    using Contracts;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;

    public static class Bootstrapper
    {
        private static Container container;

        public static void Bootstrap()
        {
            container = new Container();

            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();

            container.RegisterSingleton<IQueryProcessor>(new DynamicQueryProcessor(container));

            container.Register(typeof(ICommandStrategyHandler<>), typeof(WcfServiceCommandHandlerProxy<>));
            container.Register(typeof(IQueryStrategyHandler<,>), typeof(WcfServiceQueryHandlerProxy<,>));

            container.RegisterDecorator(typeof(ICommandStrategyHandler<>),
                typeof(FromWcfFaultTranslatorCommandHandlerDecorator<>));

            container.Register<CommandExampleController>();
            container.Register<QueryExampleController>();

            container.Verify();
        }

        public static TService GetInstance<TService>() where TService : class
        {
            return container.GetInstance<TService>();
        }
    }
}