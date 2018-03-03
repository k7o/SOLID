using Crosscutting.Contracts;

namespace Implementation.Query.Zoek
{
    public partial class AdresQuery : IAmTraceable
    {
        private const string EventName = "AdresQuery";

        public void Start(ITrace queryTracer)
        {
            queryTracer.Start(EventName);
        }

        public void Stop(ITrace queryTracer)
        {
            queryTracer.Stop(EventName, Postcode);
        }

        public void Excute(ITrace queryTracer)
        {
            queryTracer.Execute(EventName);
        }

        public void Excuted(ITrace queryTracer)
        {
            queryTracer.Executed(EventName, Postcode);
        }

        public void Exception(ITrace queryTracer)
        {
            queryTracer.Exception(EventName, Postcode);
        }
    }
}
