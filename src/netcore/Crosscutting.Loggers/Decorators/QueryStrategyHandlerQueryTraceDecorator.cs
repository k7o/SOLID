using Contracts;
using Crosscutting.Contracts;
using System;

namespace Crosscutting.Loggers.Decorators
{
    public class QueryStrategyHandlerQueryTraceDecorator<TQuery, TResult> : IQueryStrategyHandler<TQuery, TResult> where TQuery : IAmTraceable, IQuery<TResult>
    {
        readonly IQueryStrategyHandler<TQuery, TResult> _decoratee;
        readonly ITrace _queryTracer;

        public QueryStrategyHandlerQueryTraceDecorator(ITrace queryTracer, IQueryStrategyHandler<TQuery, TResult> decoratee)
        {
            Guard.IsNotNull(decoratee, nameof(decoratee));
            Guard.IsNotNull(queryTracer, nameof(queryTracer));

            _decoratee = decoratee;
            _queryTracer = queryTracer;
        }

        public TResult Handle(TQuery query)
        {
            if (Equals(query, default(TQuery)))
            {
                throw new ArgumentNullException(nameof(query));
            }

            query.Start(_queryTracer);

            TResult result;
            try
            {
                query.Excute(_queryTracer);

                result = _decoratee.Handle(query);

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
