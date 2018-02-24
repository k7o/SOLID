using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSources
{
    [EventSource(Name = "ClassLibraryEventSource")]
    public sealed class ClassLibraryEventSource : EventSource
    {
        private static readonly Lazy<ClassLibraryEventSource> Instance =
            new Lazy<ClassLibraryEventSource>(() => new ClassLibraryEventSource());

        private ClassLibraryEventSource() { }

        public static ClassLibraryEventSource Log { get { return Instance.Value; } }

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