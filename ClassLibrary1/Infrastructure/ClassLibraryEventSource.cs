using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Infrastructure
{
    [EventSource(Name = "ClassLibraryEventSource")]
    public class ClassLibraryEventSource : EventSource
    {
        private static readonly Lazy<ClassLibraryEventSource> Instance =
            new Lazy<ClassLibraryEventSource>(() => new ClassLibraryEventSource());

        private ClassLibraryEventSource() { }

        public static ClassLibraryEventSource Log { get { return Instance.Value; } }

        protected static class Keywords
        {
            public const EventKeywords Diagnostic = (EventKeywords)1;
            public const EventKeywords Perf = (EventKeywords)2;
        }

        protected static class Tasks
        {
            public const EventTask Service = (EventTask)1;
            public const EventTask Query = (EventTask)2;
        }

        [Event(1, Message = "Failure: {0}",
             Level = EventLevel.Critical, 
             Keywords = Keywords.Diagnostic)]
        internal void Failure(string eventName, string message)
        {
            this.WriteEvent(1, eventName, message);
        }

        [Event(2, Message = "Start Query: {0}",
            Opcode = EventOpcode.Start,
            Task = Tasks.Query,
            Level = EventLevel.Informational,
            Keywords = Keywords.Diagnostic)]
        internal void QueryStart(string eventName)
        {
            if (this.IsEnabled())
                this.WriteEvent(2, eventName);
        }

        [Event(3, Message = "Stop Query: {0}",
            Opcode = EventOpcode.Stop,
            Task = Tasks.Query,
            Level = EventLevel.Informational,
            Keywords = Keywords.Diagnostic)]
        internal void QueryStop(string eventName, string string1)
        {
            if (this.IsEnabled())
                this.WriteEvent(3, eventName, string1);
        }

        [Event(4, Message = "Execute Query: {0}",
            Opcode = EventOpcode.Info,
            Task = Tasks.Query,
            Level = EventLevel.Informational,
            Keywords = Keywords.Diagnostic)]
        internal void QueryExecute(string eventName)
        {
            if (this.IsEnabled())
                this.WriteEvent(4, eventName);
        }

        [Event(5, Message = "Executed Query: {0}",
            Opcode = EventOpcode.Info,
            Task = Tasks.Query,
            Level = EventLevel.Informational,
            Keywords = Keywords.Diagnostic)]
        internal void QueryExecuted(string eventName, string string1)
        {
            if (this.IsEnabled())
                this.WriteEvent(5, eventName, string1);
        }
    }
}