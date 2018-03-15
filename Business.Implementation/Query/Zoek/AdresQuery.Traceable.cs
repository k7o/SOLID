using Crosscutting.Contracts;

namespace Business.Implementation.Query.Zoek
{
    public partial class AdresQuery : IAmTraceable
    {
        private const string EventName = "AdresQuery";

        public void Start(ITrace trace)
        {
            Guard.IsNotNull(trace, nameof(trace));

            trace.Start(EventName);
        }

        public void Stop(ITrace trace)
        {
            Guard.IsNotNull(trace, nameof(trace));

            trace.Stop(EventName, Postcode);
        }

        public void Excute(ITrace trace)
        {
            Guard.IsNotNull(trace, nameof(trace));

            trace.Execute(EventName);
        }

        public void Excuted(ITrace trace)
        {
            Guard.IsNotNull(trace, nameof(trace));

            trace.Executed(EventName, Postcode);
        }

        public void Exception(ITrace trace)
        {
            Guard.IsNotNull(trace, nameof(trace));

            trace.Exception(EventName, Postcode);
        }
    }
}
