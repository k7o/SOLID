using Contracts;
using Infrastructure;
using System;

namespace Implementation.Decorators
{
    public class QueryTracerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IAmTraceable, IQuery<TResult> 
    {
        readonly IQueryHandler<TQuery, TResult> _decorated;
        readonly IQueryTracer _queryTracer;

        public QueryTracerDecorator(IQueryHandler<TQuery, TResult> decorated, IQueryTracer queryTracer)
        {
            Guard.IsNotNull(decorated, nameof(decorated));
            Guard.IsNotNull(queryTracer, nameof(queryTracer));

            _decorated = decorated;
            _queryTracer = queryTracer;
        }

        public TResult Handle(TQuery query)
        {
            query.Start(_queryTracer);

            TResult result;
            try
            {
                query.Excute(_queryTracer);

                result = _decorated.Handle(query);

                query.Excuted(_queryTracer);
            }
            catch (Exception)
            {
                query.Exception(_queryTracer);
                throw;
            }
            finally
            {
                query.Stop(_queryTracer);
            }

            return result;
        }
    }
}
