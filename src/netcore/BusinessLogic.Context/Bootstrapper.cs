using Contexts.Contracts;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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

        public static Container RegisterSqlContext(this Container container, string connectionstring)
        {
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
