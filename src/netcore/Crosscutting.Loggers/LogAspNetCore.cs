using Crosscutting.Contracts;
using Microsoft.Extensions.Logging;
using System;

namespace Crosscutting.Loggers
{
    public class LogAspNetCore : ILog
    {
        readonly ILogger _logger;

        public LogAspNetCore(ILogger logger)
        {
            Guard.IsNotNull(logger, nameof(logger));

            _logger = logger;
        }

        public void Debug(string message)
        {
            _logger.LogDebug(message);
        }

        public void Debug(Exception ex, string message)
        {
            _logger.LogDebug(ex, message);
        }

        public void Exception(string message)
        {
            _logger.LogError(message);
        }

        public void Exception(Exception ex, string message)
        {
            _logger.LogError(ex, message);
        }

        public void Informational(string message)
        {
            _logger.LogInformation(message);
        }

        public void Informational(Exception ex, string message)
        {
            _logger.LogInformation(ex, message);
        }

        public void Warning(string message)
        {
            _logger.LogWarning(message);
        }

        public void Warning(Exception ex, string message)
        {
            _logger.LogWarning(ex, message);
        }
    }
}
