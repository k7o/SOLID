using System;
using Infrastructure;

namespace ClassLibrary1.Query.Zoek
{
    public partial class AdresQuery : IAmTraceable
    {
        private const string EventName = "Adres";

        public void Start(IQueryEventSource queryEventSource)
        {
            queryEventSource.QueryStart(EventName);
        }

        public void Stop(IQueryEventSource queryEventSource)
        {
            queryEventSource.QueryStop(EventName, Postcode);
        }

        public void Excute(IQueryEventSource queryEventSource)
        {
            queryEventSource.QueryExecute(EventName);
        }

        public void Excuted(IQueryEventSource queryEventSource)
        {
            queryEventSource.QueryExecuted(EventName, Postcode);
        }

        public void Failure(IQueryEventSource queryEventSource, Exception ex)
        {
            queryEventSource.Failure(EventName, Postcode);
        }
    }
}
