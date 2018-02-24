using Infrastructure;
using System;
using System.Diagnostics;

namespace ClassLibrary1.Decorators
{
    public class QueryEventSourceDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IAmTraceable, IQuery<TResult> 
    {
        readonly IQueryHandler<TQuery, TResult> _decorated;
        readonly IQueryLog _queryLog;

        public QueryEventSourceDecorator(IQueryHandler<TQuery, TResult> decorated, IQueryLog eventSource)
        {
            Guard.IsNotNull(decorated, nameof(decorated));
            Guard.IsNotNull(eventSource, nameof(eventSource));

            _decorated = decorated;
            _queryLog = eventSource;
        }

        public TResult Handle(TQuery query)
        {
            query.Start(_queryLog);

            TResult result;
            try
            {
                query.Excute(_queryLog);

                result = _decorated.Handle(query);

                query.Excuted(_queryLog);
            }
            finally
            {
                query.Stop(_queryLog);
            }

            return result;
        }
    }
}
