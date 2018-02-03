
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading;

namespace ClassLibrary1.Infrastructure
{
    //
    // GENERATED CODE!
    //

    /// <summary>
    /// MEF container
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class MefContainer
    {
        private static Lazy<CompositionContainer> Container =
            new Lazy<CompositionContainer>(BuildContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        internal static void RegisterInstance<T>(object mockRepository)
        {
            throw new NotImplementedException();
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The container is a singleton and will be destroyed when the application is closed.")]
        private static CompositionContainer BuildContainer()
        {
            var aggregate = new AggregateCatalog();

            var container = new CompositionContainer(aggregate, true);
            container.Compose(new CompositionBatch());

            return container;
        }

        /// <summary>
        /// Add extra types from the given <paramref name="assembly"/> to the container.
        /// </summary>
        public static void AddTypesFromAssembly(Assembly assembly)
        {
            ((AggregateCatalog)Container.Value.Catalog).Catalogs.Add(new AssemblyCatalog(assembly));
            Container.Value.Compose(new CompositionBatch());
        }

        /// <summary>
        /// Register a specific instance 
        /// </summary>
        public static void RegisterInstance<T>(T instance)
        {
            AttributedModelServices.ComposeExportedValue(Container.Value, instance);
        }

        /// <summary>
        /// Fetch an instance of type <typeparam name="T" /> from the container.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This is a valid method signature.")]
        public static Lazy<T> Resolve<T>()
        {
            return Container.Value.GetExport<T>();
        }

        /// <summary>
        /// Fetch an instance of type <typeparam name="type" /> from the container.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Resolve(Type type)
        {
            return Container.Value.GetExportedValue(type);
        }

        /// <summary>
        /// Fetch all instances of type <typeparam name="T" /> from the container.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is a valid method signature.")]
        public static IEnumerable<Lazy<T>> ResolveAll<T>()
        {
            return Container.Value.GetExports<T>();
        }

        private static object GetExportedValue(this ExportProvider container, Type type)
        {
            // get a reference to the GetExportedValue<T> method
            MethodInfo methodInfo = container.GetType().GetMethods()
                                      .First(d => d.Name == "GetExportedValue"
                                                  && d.GetParameters().Length == 0);

            // create an array of the generic types that the GetExportedValue<T> method expects
            Type[] genericTypeArray = new Type[] { type };

            // add the generic types to the method
            methodInfo = methodInfo.MakeGenericMethod(genericTypeArray);

            // invoke GetExportedValue<type>()
            return methodInfo.Invoke(container, null);
        }
    }
}
