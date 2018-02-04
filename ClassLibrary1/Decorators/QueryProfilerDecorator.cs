using ClassLibrary1.Infrastructure;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Decorators
{
    public class QueryProfilerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        ILogger _logger;
        IQueryHandler<TQuery, TResult> _decorated;

        public QueryProfilerDecorator(ILogger logger, IQueryHandler<TQuery, TResult> decorated)
        {
            _logger = logger;
            _decorated = decorated;
        }

        public TResult Handle(TQuery query)
        {
            var key = $"{query.GetType().Name}";

            _logger.Information($"{key} start");

            var sw = Stopwatch.StartNew();

            var result = _decorated.Handle(query);

            _logger.Information($"{key} ran for {sw.ElapsedMilliseconds} milliseconds");

            return result;
        }
    }
}
