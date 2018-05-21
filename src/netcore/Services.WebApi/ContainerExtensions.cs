using Crosscutting.Contracts;
using MediatR;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Services.WebApi
{
    public static class ContainerExtensions
    {
        public static Container RegisterMediator(this Container container, params Assembly[] assemblies)
        {
            return BuildMediator(container, (IEnumerable<Assembly>)assemblies);
        }

        public static Container BuildMediator(this Container container, IEnumerable<Assembly> assemblies)
        {
            Guard.IsNotNull(container, nameof(container));

            var allAssemblies = new List<Assembly> { typeof(IMediator).GetTypeInfo().Assembly };
            allAssemblies.AddRange(assemblies);

            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), allAssemblies);
            container.Register(typeof(IRequestHandler<>), allAssemblies);
            // we have to do this because by default, generic type definitions (such as the Constrained Notification Handler) won't be registered
            var notificationHandlerTypes = container.GetTypesToRegister(
                typeof(INotificationHandler<>), 
                assemblies, 
                new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false,
            });
            container.Collection.Register(typeof(INotificationHandler<>), 
                notificationHandlerTypes);

            container.RegisterInstance(new SingleInstanceFactory(container.GetInstance));
            container.RegisterInstance(new MultiInstanceFactory(container.GetAllInstances));

            return container;
        }
    }
}
