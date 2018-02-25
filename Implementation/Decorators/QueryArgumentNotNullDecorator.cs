using Contracts;

namespace Implementation.Decorators
{
    public class QueryArgumentNotNullDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        readonly IQueryHandler<TQuery, TResult> _decorated;

        public QueryArgumentNotNullDecorator(IQueryHandler<TQuery, TResult> decorated)
        {
            Guard.IsNotNull(decorated, nameof(decorated));

            _decorated = decorated;
        }

        public TResult Handle(TQuery query)
        {
            Guard.IsNotNull(query, nameof(query));

            return _decorated.Handle(query);
        }
    }
}
