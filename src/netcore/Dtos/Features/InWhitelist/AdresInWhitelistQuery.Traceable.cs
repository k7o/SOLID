using Crosscutting.Contracts;

namespace Dtos.Features.InWhitelist
{
    public partial class AdresInWhitelistQuery : IAmTraceable
    {
        public void Start(ITrace trace)
        {
            Guard.IsNotNull(trace, nameof(trace));

            trace.Start();
        }

        public void Stop(ITrace trace)
        {
            Guard.IsNotNull(trace, nameof(trace));

            trace.Stop(Postcode);
        }

        public void Excute(ITrace trace)
        {
            Guard.IsNotNull(trace, nameof(trace));

            trace.Execute();
        }

        public void Excuted(ITrace trace)
        {
            Guard.IsNotNull(trace, nameof(trace));

            trace.Executed(Postcode);
        }

        public void Exception(ITrace trace)
        {
            Guard.IsNotNull(trace, nameof(trace));

            trace.Exception(Postcode);
        }
    }
}
