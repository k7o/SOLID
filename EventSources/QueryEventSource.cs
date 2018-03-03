using Contracts.Crosscutting;
using System;
using System.Diagnostics.Tracing;

namespace EventSources
{
    public class QueryEventSource : IQueryTracer
    {
        private static readonly Lazy<NestedQueryEventSource> Instance =
              new Lazy<NestedQueryEventSource>(() => new NestedQueryEventSource());
        private static NestedQueryEventSource InnerLog { get { return Instance.Value; } }

        public void Execute(string eventName)
        {
            InnerLog.QueryExecute(eventName);
        }

        public void Executed(string eventName, string executed)
        {
            InnerLog.QueryExecuted(eventName, executed);
        }

        public void Start(string eventName)
        {
            InnerLog.QueryStart(eventName);
        }

        public void Stop(string eventName, string stop)
        {
            InnerLog.QueryStop(eventName, stop);
        }

        public void Exception(string eventName, string exception)
        {
            InnerLog.QueryException(eventName, exception);
        }

        [EventSource(Name = "QueryEventSource")]
        private sealed class NestedQueryEventSource : EventSource
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
            public void QueryException(string eventName, string exceptionValue)
            {
                if (this.IsEnabled())
                    this.WriteEvent(1, eventName, exceptionValue);
            }

            [Event(2, Message = "Start {0}")]
            public void QueryStart(string eventName)
            {
                if (this.IsEnabled())
                    this.WriteEvent(2, eventName);
            }

            [Event(3, Message = "Stop {0}")]
            public void QueryStop(string eventName, string queryStopValue)
            {
                if (this.IsEnabled())
                    this.WriteEvent(3, eventName, queryStopValue);
            }

            [Event(4, Message = "Execute {0}")]
            public void QueryExecute(string eventName)
            {
                if (this.IsEnabled())
                    this.WriteEvent(4, eventName);
            }

            [Event(5, Message = "Executed {0}")]
            public void QueryExecuted(string eventName, string executedValue)
            {
                if (this.IsEnabled())
                    this.WriteEvent(5, eventName, executedValue);
            }
        }
    }
}