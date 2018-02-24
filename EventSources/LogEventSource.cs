using Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSources
{
    public class LogEventSource : ILog
    {
        private static readonly Lazy<NestedLogEventSource> Instance =
              new Lazy<NestedLogEventSource>(() => new NestedLogEventSource());
        private static NestedLogEventSource innerLog { get { return Instance.Value; } }

        public void Debug(string message)
        {
            innerLog.Debug("debug", message);
        }

        public void Debug(Exception ex, string message)
        {
            innerLog.Debug("debug", message);
        }

        public void Exception(string message)
        {
            innerLog.Exception("exception", message);
        }

        public void Exception(Exception ex, string message)
        {
            innerLog.Exception("exception", message);
        }

        public void Informational(string message)
        {
            innerLog.Exception("information", message);
        }

        public void Informational(Exception ex, string message)
        {
            innerLog.Exception("information", message);
        }

        public void Warning(string message)
        {
            innerLog.Exception("warning", message);
        }

        public void Warning(Exception ex, string message)
        {
            innerLog.Exception("warning", message);
        }

        [EventSource(Name = "LogEventSource")]
        private sealed class NestedLogEventSource : EventSource
        {
            private static class Keywords
            {
                public const EventKeywords Diagnostic = (EventKeywords)1;
                public const EventKeywords Perf = (EventKeywords)2;
            }

            private static class Tasks
            {
                public const EventTask Service = (EventTask)1;
                public const EventTask Query = (EventTask)2;
            }

            [Event(1, Message = "Debug: {0}")]
            public void Debug(string eventName, string message)
            {
                if (this.IsEnabled())
                    this.WriteEvent(1, eventName, message);
            }

            [Event(2, Message = "Exception: {0}")]
            public void Exception(string eventName, string message)
            {
                if (this.IsEnabled())
                    this.WriteEvent(2, eventName, message);
            }

            [Event(3, Message = "Informational: {0}")]
            public void Informational(string eventName, string message)
            {
                if (this.IsEnabled())
                    this.WriteEvent(3, eventName, message);
            }

            [Event(4, Message = "Warning: {0}")]
            public void Warning(string eventName, string message)
            {
                if (this.IsEnabled())
                    this.WriteEvent(4, eventName, message);
            }
        }
    }
}
