- DecoratorChainBuilder: simplifies the creation of a decorator chain, also adds the possiblity to omit a decorator

- MefContainer: is a MEF DI wrapper class

- QueryHandlerFactory: exposes MEF exports for (decorated) query handlers

- MefQueryProcessor: handler to execute queryhandler for given query using the MEF (https://docs.microsoft.com/en-us/dotnet/framework/mef/) DI framework (taken from https://cuttingedge.it/blogs/steven/pivot/entry.php?id=92)

- SIQueryProcessor: handler to execute queryhandler for given query using the SimpleInjector (https://simpleinjector.readthedocs.io/en/latest/index.html) DI framework (taken from https://cuttingedge.it/blogs/steven/pivot/entry.php?id=92)
