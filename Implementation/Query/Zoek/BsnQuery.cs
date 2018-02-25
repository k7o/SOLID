using Contracts;

namespace Implementation.Query.Zoek
{
    public partial class BsnQuery : IQuery<ZoekResult>
    {
        public int Bsnnummer { get; private set; }

        public BsnQuery(int bsnnummer) 
        {
            Bsnnummer = bsnnummer;
        }
    }
}
