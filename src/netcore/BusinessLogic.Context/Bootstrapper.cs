using Contexts.Contracts;
using Crosscutting.Contracts;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;

namespace BusinessLogic.Contexts
{
    public static class Bootstrapper
    {
        public static Container RegisterInMemoryContext(this Container container)
        {
            // datasource
            // register WhitelistContext (scoped)
            container.Register(() => new WhitelistContext(
                                        new DbContextOptionsBuilder()
                                            .UseInMemoryDatabase("Whitelist")
                                            .Options), Lifestyle.Scoped);
            // also register as IContext (scoped)
            container.Register<IContext>(() => container.GetInstance<WhitelistContext>(), Lifestyle.Scoped);

            return container;
        }

        public static Container RegisterSqlContext(this Container container, [IsNullOrWhiteSpace] string connectionstring)
        {
            Guard.IsNullOrWhiteSpace(connectionstring, nameof(connectionstring));

            // datasource
            // register WhitelistContext (scoped)
            container.Register(() => new WhitelistContext(
                                        new DbContextOptionsBuilder()
                                            .UseSqlServer(connectionstring)
                                            .Options), Lifestyle.Scoped);
            // also register as IContext (scoped)
            container.Register<IContext>(() => container.GetInstance<WhitelistContext>(), Lifestyle.Scoped);

            return container;
        }
    }
}
