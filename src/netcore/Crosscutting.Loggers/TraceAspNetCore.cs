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

        public void Exception(string exceptionMessage)
        {
            _logger.LogTrace(new EventId(1, "Exception"), exceptionMessage);
        }

        public void Execute()
        {
            _logger.LogTrace(new EventId(2, "Execute"), string.Empty);
        }

        public void Executed(string executedWith)
        {
            _logger.LogTrace(new EventId(3, "Executed"), executedWith);
        }

        public void Start()
        {
            _logger.LogTrace(new EventId(4, "Start"), string.Empty);
        }

        public void Stop(string stopped)
        {
            _logger.LogTrace(new EventId(5, "Stop"), stopped);
        }
    }
}
