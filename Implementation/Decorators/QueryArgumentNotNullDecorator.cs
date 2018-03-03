using Contracts;
using Crosscutting.Contracts;

namespace Implementation.Decorators
{
    public class QueryArgumentNotNullDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        readonly IQueryHandler<TQuery, TResult> _decoratee;

        public QueryArgumentNotNullDecorator(IQueryHandler<TQuery, TResult> decoratee)
        {
            Guard.IsNotNull(decoratee, nameof(decoratee));

            _decoratee = decoratee;
        }

        public TResult Handle(TQuery query)
        {
            Guard.IsNotNull(query, nameof(query));

            return _decoratee.Handle(query);
        }
    }
}
