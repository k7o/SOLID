using System;
using System.Collections.Generic;

namespace Contracts.Crosscutting
{
    public class CompositeLog : ILog
    {
        readonly IEnumerable<ILog> _logs;

        public CompositeLog(IEnumerable<ILog> logs)
        {
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
        }

        public void Debug(string message)
        {
            foreach (var log in _logs)
            {
                log.Debug(message);
            }
        }

        public void Debug(Exception ex, string message)
        {
            foreach (var log in _logs)
            {
                log.Debug(ex, message);
            }
        }

        public void Exception(string message)
        {
            foreach (var log in _logs)
            {
                log.Exception(message);
            }
        }

        public void Exception(Exception ex, string message)
        {
            foreach (var log in _logs)
            {
                log.Exception(ex, message);
            }
        }

        public void Informational(string message)
        {
            foreach (var log in _logs)
            {
                log.Informational(message);
            }
        }

        public void Informational(Exception ex, string message)
        {
            foreach (var log in _logs)
            {
                log.Informational(ex, message);
            }
        }

        public void Warning(string message)
        {
            foreach (var log in _logs)
            {
                log.Warning(message);
            }
        }

        public void Warning(Exception ex, string message)
        {
            foreach (var log in _logs)
            {
                log.Warning(ex, message);
            }
        }
    }
}
