using Contracts.Crosscutting;
using Serilog;
using System;

namespace Loggers
{
    public class QueryLogSerilog : IQueryTracer
    {
        readonly ILogger _logger;

        public QueryLogSerilog(ILogger logger)
        {
            Guard.IsNotNull(logger, "logger");

            _logger = logger;
        }

        public void Exception(string eventName, Exception ex)
        {
            _logger.Error(ex, $"execute {eventName}");
        }

        public void Exception(string eventName, string exception)
        {
            _logger.Error($"exception thrown on {eventName} ({exception})");
        }

        public void Execute(string eventName)
        {
            _logger.Information($"execute {eventName}");
        }

        public void Executed(string eventName, string executed)
        {
            _logger.Information($"executed {eventName} with value {executed}");
        }

        public void Start(string eventName)
        {
            _logger.Information($"start {eventName}");
        }

        public void Stop(string eventName, string stop)
        {
            _logger.Information($"stop {eventName} with value {stop}");
        }
    }
}
