using ClassLibrary1.Infrastructure;
using ClassLibrary1.Query.Zoek.Tracers;

namespace ClassLibrary1.Query.Zoek
{
    public partial class AdresQuery : IAmTraceable
    {
        public void Start()
        {
            AdresTracer.Log.Start(Adres);

        }

        public void Stop()
        {
            AdresTracer.Log.Stop();
        }
    }
}
