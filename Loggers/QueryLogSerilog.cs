using Infrastructure;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loggers
{
    public class QueryLogSerilog : IQueryLog
    {
        readonly ILogger _logger;

        public QueryLogSerilog(ILogger logger)
        {
            Guard.IsNotNull(logger, "logger");

            _logger = logger;
        }

        public void QueryExecute(string eventName)
        {
            _logger.Information($"execute {eventName}");
        }

        public void QueryExecuted(string eventName, string string1)
        {
            _logger.Information($"executed {eventName} with value {string1}");
        }

        public void QueryStart(string eventName)
        {
            _logger.Information($"start {eventName}");
        }

        public void QueryStop(string eventName, string string1)
        {
            _logger.Information($"stop {eventName} with value {string1}");
        }
    }
}
