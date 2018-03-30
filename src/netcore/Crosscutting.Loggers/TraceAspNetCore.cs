using Crosscutting.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosscutting.Loggers
{
    public class TraceAspNetCore : ITrace
    {
        readonly ILogger _logger;

        public TraceAspNetCore(ILogger logger)
        {
            Guard.IsNotNull(logger, nameof(logger));

            _logger = logger;
        }

        public void Exception(string eventName, string exceptionMessage)
        {
            _logger.LogTrace(new EventId(1, "Exception"), $"{eventName} {exceptionMessage}");
        }

        public void Execute(string eventName)
        {
            _logger.LogTrace(new EventId(2, "Execute"), eventName);
        }

        public void Executed(string eventName, string executedWith)
        {
            _logger.LogTrace(new EventId(3, "Executed"), $"{eventName} {executedWith}");
        }

        public void Start(string eventName)
        {
            _logger.LogTrace(new EventId(4, "Start"), eventName);
        }

        public void Stop(string eventName, string stopped)
        {
            _logger.LogTrace(new EventId(5, "Stop"), $"{eventName} {stopped}");
        }
    }
}
