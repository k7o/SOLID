using ClassLibrary1.Infrastructure;

namespace ClassLibrary1.Decorators
{
    public class QueryTracerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IAmTraceable, IQuery<TResult> 
    {
        readonly IQueryHandler<TQuery, TResult> _decorated;

        public QueryTracerDecorator(IQueryHandler<TQuery, TResult> decorated)
        {
            _decorated = decorated;
        }

        public TResult Handle(TQuery query)
        {
            query.Start();

            var result = _decorated.Handle(query);

            query.Stop();

            return result;
        }
    }
}
