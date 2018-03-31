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

        public void Exception(string exceptionMessage)
        {
            _logger.Error($"exception thrown ({exceptionMessage})");
        }

        public void Execute()
        {
            _logger.Information($"Execute {GetType().Name}");
        }

        public void Executed(string executedWith)
        {
            _logger.Information($"Executed");
        }

        public void Start()
        {
            _logger.Information($"Start");
        }

        public void Stop(string stopped)
        {
            _logger.Information($"Stop with value {stopped}");
        }
    }
}
