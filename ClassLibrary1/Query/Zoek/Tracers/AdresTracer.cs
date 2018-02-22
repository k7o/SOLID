using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Query.Zoek.Tracers
{
    [EventSource(Name = "ClassLibrary1.Query.Zoek.Tracers.AdresTracer")]
    public class AdresTracer : EventSource
    {
        private static readonly Lazy<AdresTracer> Instance =
            new Lazy<AdresTracer>(() => new AdresTracer());

        private AdresTracer() { }

        public static AdresTracer Log { get { return Instance.Value; } }
        

        public class Keywords
        {
            public const EventKeywords Diagnostic = (EventKeywords)1;
            public const EventKeywords Perf = (EventKeywords)2;
        }

        public class Tasks
        {
            public const EventTask Service = (EventTask)1;
            public const EventTask Zoek = (EventTask)2;
        }

        [Event(1, Message = "Start Adres",
            Opcode = EventOpcode.Start,
            Task = Tasks.Zoek, Keywords = Keywords.Diagnostic,
            Level = EventLevel.Informational)]
        internal void Start(string adres)
        {
            if (this.IsEnabled())
                this.WriteEvent(1, adres);
        }

        [Event(1, Message = "Einde Adres",
            Opcode = EventOpcode.Stop,
            Task = Tasks.Zoek, Keywords = Keywords.Diagnostic,
            Level = EventLevel.Informational)]
        internal void Stop()
        {
            if (this.IsEnabled())
                this.WriteEvent(1);
        }
    }
}

/*
[EventSource(Name = "MyCompany")]
public class MyCompanyEventSource : EventSource
{
    public class Keywords
    {
        public const EventKeywords Page = (EventKeywords)1;
        public const EventKeywords DataBase = (EventKeywords)2;
        public const EventKeywords Diagnostic = (EventKeywords)4;
        public const EventKeywords Perf = (EventKeywords)8;
    }

    public class Tasks
    {
        public const EventTask Page = (EventTask)1;
        public const EventTask DBQuery = (EventTask)2;
    }

    private static MyCompanyEventSource _log = new MyCompanyEventSource();
    private MyCompanyEventSource() { }
    public static MyCompanyEventSource Log { get { return _log; } }

    [Event(1, Message = "Application Failure: {0}",
    Level = EventLevel.Critical, Keywords = Keywords.Diagnostic)]
    internal void Failure(string message)
    {
        this.WriteEvent(1, message);
    } */