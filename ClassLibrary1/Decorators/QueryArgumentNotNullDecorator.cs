using System;
using System.Runtime.Caching;
using ClassLibrary1.Infrastructure;
using LazyCache;
using Serilog;

namespace ClassLibrary1.Decorators
{
    public class QueryArgumentNotNullDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        IQueryHandler<TQuery, TResult> _decorated;

        public QueryArgumentNotNullDecorator(IQueryHandler<TQuery, TResult> decorated)
        {
            _decorated = decorated;
        }

        public TResult Handle(TQuery query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            return _decorated.Handle(query);
            
        }
    }
}
