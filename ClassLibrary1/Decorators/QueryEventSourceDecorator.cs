using ClassLibrary1.Infrastructure;
using System;
using System.Diagnostics;

namespace ClassLibrary1.Decorators
{
    public class QueryEventSourceDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IAmTraceable, IQuery<TResult> 
    {
        readonly IQueryHandler<TQuery, TResult> _decorated;

        public QueryEventSourceDecorator(IQueryHandler<TQuery, TResult> decorated)
        {
            Guard.IsNotNull(decorated, nameof(decorated));

            _decorated = decorated;
        }

        public TResult Handle(TQuery query)
        {
            query.Start();

            TResult result;
            try
            {
                query.Excute();

                result = _decorated.Handle(query);

                query.Excuted();
            }
            catch (Exception ex)
            {
                query.Failure(ex);
                throw;
            }                
            finally
            {
                query.Stop();
            }

            return result;
        }
    }
}
