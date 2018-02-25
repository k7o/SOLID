using Contracts;
using Infrastructure;

namespace Implementation.Query.Zoek
{
    public partial class AdresQuery : IAmTraceable
    {
        private const string EventName = "AdresQuery";

        public void Start(IQueryTracer queryTracer)
        {
            queryTracer.Start(EventName);
        }

        public void Stop(IQueryTracer queryTracer)
        {
            queryTracer.Stop(EventName, Postcode);
        }

        public void Excute(IQueryTracer queryTracer)
        {
            queryTracer.Execute(EventName);
        }

        public void Excuted(IQueryTracer queryTracer)
        {
            queryTracer.Executed(EventName, Postcode);
        }

        public void Exception(IQueryTracer queryTracer)
        {
            queryTracer.Exception(EventName, Postcode);
        }
    }
}
