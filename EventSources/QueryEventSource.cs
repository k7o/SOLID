using Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSources
{
    public class QueryEventSource : IQueryEventSource
    {
        private static readonly Lazy<NestedQueryEventSource> Instance =
              new Lazy<NestedQueryEventSource>(() => new NestedQueryEventSource());
        private static NestedQueryEventSource innerLog { get { return Instance.Value; } }

        public bool IsEnabled()
        {
            return innerLog.IsEnabled();
        }

        public void Failure(string eventName, string message)
        {
            innerLog.Failure(eventName, message);
        }

        public void QueryExecute(string eventName)
        {
            innerLog.QueryExecute(eventName);
        }

        public void QueryExecuted(string eventName, string string1)
        {
            innerLog.QueryExecuted(eventName, string1);
        }

        public void QueryStart(string eventName)
        {
            innerLog.QueryStart(eventName);
        }

        public void QueryStop(string eventName, string string1)
        {
            innerLog.QueryStop(eventName, string1);
        }

        [EventSource(Name = "QueryEventSource")]
        private sealed class NestedQueryEventSource : EventSource, IQueryEventSource
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

            [Event(1, Message = "Failure: {0}")]
            public void Failure(string eventName, string message)
            {
                if (this.IsEnabled())
                    this.WriteEvent(1, eventName, message);
            }

            [Event(2, Message = "Start Query: {0}")]
            public void QueryStart(string eventName)
            {
                if (this.IsEnabled())
                    this.WriteEvent(2, eventName);
            }

            [Event(3, Message = "Stop Query: {0}")]
            public void QueryStop(string eventName, string string1)
            {
                if (this.IsEnabled())
                    this.WriteEvent(3, eventName, string1);
            }

            [Event(4, Message = "Execute Query: {0}")]
            public void QueryExecute(string eventName)
            {
                if (this.IsEnabled())
                    this.WriteEvent(4, eventName);
            }

            [Event(5, Message = "Executed Query: {0}")]
            public void QueryExecuted(string eventName, string string1)
            {
                if (this.IsEnabled())
                    this.WriteEvent(5, eventName, string1);
            }
        }
    }
}