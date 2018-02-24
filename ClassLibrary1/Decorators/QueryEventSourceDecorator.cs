using Infrastructure;
using System;
using System.Diagnostics;

namespace ClassLibrary1.Decorators
{
    public class QueryEventSourceDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IAmTraceable, IQuery<TResult> 
    {
        readonly IQueryHandler<TQuery, TResult> _decorated;
        readonly IQueryEventSource _eventSource;

        public QueryEventSourceDecorator(IQueryHandler<TQuery, TResult> decorated, IQueryEventSource eventSource)
        {
            Guard.IsNotNull(decorated, nameof(decorated));
            Guard.IsNotNull(eventSource, nameof(eventSource));

            _decorated = decorated;
            _eventSource = eventSource;
        }

        public TResult Handle(TQuery query)
        {
            query.Start(_eventSource);

            TResult result;
            try
            {
                query.Excute(_eventSource);

                result = _decorated.Handle(query);

                query.Excuted(_eventSource);
            }
            catch (Exception ex)
            {
                query.Failure(_eventSource, ex);
                throw;
            }                
            finally
            {
                query.Stop(_eventSource);
            }

            return result;
        }
    }
}
