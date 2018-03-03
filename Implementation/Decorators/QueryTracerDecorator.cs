using Contracts;
using Contracts.Crosscutting;
using System;

namespace Implementation.Decorators
{
    public class QueryTracerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IAmTraceable, IQuery<TResult> 
    {
        readonly IQueryHandler<TQuery, TResult> _decoratee;
        readonly IQueryTracer _queryTracer;

        public QueryTracerDecorator(IQueryTracer queryTracer, IQueryHandler<TQuery, TResult> decoratee)
        {
            Guard.IsNotNull(decoratee, nameof(decoratee));
            Guard.IsNotNull(queryTracer, nameof(queryTracer));

            _decoratee = decoratee;
            _queryTracer = queryTracer;
        }

        public TResult Handle(TQuery query)
        {
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
