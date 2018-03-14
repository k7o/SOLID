using Crosscutting.Contracts;
using Serilog;
using System;

namespace Crosscutting.Loggers
{
    public class TraceSerilog : ITrace
    {
        readonly ILogger _logger;

        public TraceSerilog(ILogger logger)
        {
            Guard.IsNotNull(logger, "logger");

            _logger = logger;
        }

        public void Exception(string eventName, Exception ex)
        {
            _logger.Error(ex, $"execute {eventName}");
        }

        public void Exception(string eventName, string exceptionMessage)
        {
            _logger.Error($"exception thrown on {eventName} ({exceptionMessage})");
        }

        public void Execute(string eventName)
        {
            _logger.Information($"execute {eventName}");
        }

        public void Executed(string eventName, string executedWith)
        {
            _logger.Information($"executed {eventName} with value {executedWith}");
        }

        public void Start(string eventName)
        {
            _logger.Information($"start {eventName}");
        }

        public void Stop(string eventName, string stopped)
        {
            _logger.Information($"stop {eventName} with value {stopped}");
        }
    }
}
