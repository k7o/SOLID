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

        public void Execute(string eventName)
        {
            InnerLog.TraceExecute(eventName);
        }

        public void Executed(string eventName, string executedWith)
        {
            InnerLog.TraceExecuted(eventName, executedWith);
        }

        public void Start(string eventName)
        {
            InnerLog.TraceStart(eventName);
        }

        public void Stop(string eventName, string stop)
        {
            InnerLog.TraceStop(eventName, stop);
        }

        public void Exception(string eventName, string exceptionMessage)
        {
            InnerLog.TraceException(eventName, exceptionMessage);
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
                if (this.IsEnabled())
                    this.WriteEvent(1, eventName, exceptionValue);
            }

            [Event(2, Message = "Start {0}")]
            public void TraceStart(string eventName)
            {
                if (this.IsEnabled())
                    this.WriteEvent(2, eventName);
            }

            [Event(3, Message = "Stop {0}")]
            public void TraceStop(string eventName, string queryStopValue)
            {
                if (this.IsEnabled())
                    this.WriteEvent(3, eventName, queryStopValue);
            }

            [Event(4, Message = "Execute {0}")]
            public void TraceExecute(string eventName)
            {
                if (this.IsEnabled())
                    this.WriteEvent(4, eventName);
            }

            [Event(5, Message = "Executed {0}")]
            public void TraceExecuted(string eventName, string executedValue)
            {
                if (this.IsEnabled())
                    this.WriteEvent(5, eventName, executedValue);
            }
        }
    }
}