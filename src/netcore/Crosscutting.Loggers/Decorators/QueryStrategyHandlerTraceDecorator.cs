using Contracts;
using Crosscutting.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosscutting.Loggers.Decorators
{
    public class QueryStrategyHandlerTraceDecorator<TQuery, TResult> : IQueryStrategyHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        readonly IQueryStrategyHandler<TQuery, TResult> _decoratee;
        readonly ITrace _tracer;

        public QueryStrategyHandlerTraceDecorator(ITrace tracer, IQueryStrategyHandler<TQuery, TResult> decoratee)
        {
            Guard.IsNotNull(decoratee, nameof(decoratee));
            Guard.IsNotNull(tracer, nameof(tracer));

            _decoratee = decoratee;
            _tracer = tracer;
        }

        public TResult Handle(TQuery query)
        {
            TResult result;

            _tracer.Execute();
            try
            {
                result = _decoratee.Handle(query);
            }
            finally
            {
                _tracer.Executed(JsonConvert.SerializeObject(query));
            }

            return result;
        }
    }
}
