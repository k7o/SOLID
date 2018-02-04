using System.ComponentModel.Composition;

namespace ClassLibrary1.Infrastructure
{
    [Export(typeof(IQueryProcessor))]
    public class MefQueryProcessor : IQueryProcessor
    {
        public TResult Process<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = MefContainer.Resolve(handlerType);

            return handler.Handle((dynamic)query);
        }
    }
}
