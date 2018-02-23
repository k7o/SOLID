using System;
using ClassLibrary1.Infrastructure;

namespace ClassLibrary1.Query.Zoek
{
    public partial class AdresQuery : IAmTraceable
    {
        public void Start()
        {
            ClassLibraryEventSource.Log.QueryStart("Adres");
        }

        public void Stop()
        {
            ClassLibraryEventSource.Log.QueryStop("Adres", Adres);
        }

        public void Excute()
        {
            ClassLibraryEventSource.Log.QueryExecute("Adres");
        }

        public void Excuted()
        {
            ClassLibraryEventSource.Log.QueryExecuted("Adres", Adres);
        }

        public void Failure(Exception ex)
        {
            ClassLibraryEventSource.Log.Failure("Adres", ex.Message);
        }
    }
}
