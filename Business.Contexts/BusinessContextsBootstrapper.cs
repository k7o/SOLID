using Contracts;
using Crosscutting.Contracts;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using System.Reflection;

namespace Business.Contexts
{
    public static class BusinessContextsBootstrapper
    {
        private static Assembly[] businessContextsAssemblies = new[] { typeof(WhitelistUnitOfWork).Assembly };

        public static void Bootstrap(Container container)
        {
            Guard.IsNotNull(container, nameof(container));

            // db
            container.Register<IUnitOfWork>(() =>
                    new WhitelistUnitOfWork(
                            new DbContextOptionsBuilder()
                                .UseInMemoryDatabase("Whitelist")
                                .Options));
        }
    }
}
