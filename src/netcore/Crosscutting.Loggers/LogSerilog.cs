using Crosscutting.Contracts;
using Serilog;
using System;

namespace Crosscutting.Loggers
{
    public class LogSerilog : ILog
    {
        readonly ILogger _logger;

        public LogSerilog(ILogger logger)
        {
            Guard.IsNotNull(logger, nameof(logger));

            _logger = logger;
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(Exception ex, string message)
        {
            _logger.Debug(ex, message);
        }

        public void Exception(string message)
        {
            _logger.Error(message);
        }
        
        public void Exception(Exception ex, string message)
        {
            _logger.Error(ex, message);
        }

        public void Informational(string message)
        {
            _logger.Information(message);
        }

        public void Informational(Exception ex, string message)
        {
            _logger.Information(ex, message);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
        }

        public void Warning(Exception ex, string message)
        {
            _logger.Warning(ex, message);
        }
    }
}
