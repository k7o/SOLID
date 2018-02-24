using System;
using Infrastructure;

namespace ClassLibrary1.Query.Zoek
{
    public partial class AdresQuery : IAmTraceable
    {
        private const string EventName = "Adres";

        public void Start(IQueryLog queryLog)
        {
            queryLog.QueryStart(EventName);
        }

        public void Stop(IQueryLog queryLog)
        {
            queryLog.QueryStop(EventName, Postcode);
        }

        public void Excute(IQueryLog queryLog)
        {
            queryLog.QueryExecute(EventName);
        }

        public void Excuted(IQueryLog queryLog)
        {
            queryLog.QueryExecuted(EventName, Postcode);
        }
    }
}
