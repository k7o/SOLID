using Crosscutting.Contracts;
using System;
using System.Diagnostics.Tracing;

namespace Crosscutting.Loggers
{
    public class TraceEventSource : ITrace
    {
        private static readonly Lazy<NestedTraceEventSource> Instance =
              new Lazy<NestedTraceEventSource>(() => new NestedTraceEventSource());
        private static NestedTraceEventSource InnerLog { get { return Instance.Value; } }

        public void Execute()
        {
            InnerLog.TraceExecute("Execute");
        }

        public void Executed(string executedWith)
        {
            InnerLog.TraceExecuted("Executed", executedWith);
        }

        public void Start()
        {
            InnerLog.TraceStart("Start");
        }

        public void Stop(string stopped)
        {
            InnerLog.TraceStop("Stop", stopped);
        }

        public void Exception(string exceptionMessage)
        {
            InnerLog.TraceException("Exception", exceptionMessage);
        }

        // eventsource name should be unique
        [EventSource(Name = "k7o.TraceEventSource")]
        private sealed class NestedTraceEventSource : EventSource
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

            [Event(1, Message = "Exception thrown")]
            public void TraceException(string eventName, string exceptionValue)
            {
                if (IsEnabled())
                {
                    WriteEvent(1, eventName, exceptionValue);
                }
            }

            [Event(2, Message = "{0}")]
            public void TraceStart(string eventName)
            {
                if (IsEnabled())
                {
                    WriteEvent(2, eventName);
                }
            }

            [Event(3, Message = "{0}")]
            public void TraceStop(string eventName, string queryStopValue)
            {
                if (IsEnabled())
                {
                    WriteEvent(3, eventName, queryStopValue);
                }
            }

            [Event(4, Message = "{0}")]
            public void TraceExecute(string eventName)
            {
                if (IsEnabled())
                {
                    WriteEvent(4, eventName);
                }
            }

            [Event(5, Message = "{0}")]
            public void TraceExecuted(string eventName, string executedValue)
            {
                if (IsEnabled())
                {
                    WriteEvent(5, eventName, executedValue);
                }
            }
        }
    }
}
