using Contracts;

namespace Implementation.Query.Zoek
{
    public class BsnUzoviQuery : IQuery<ZoekResult>
    {
        public int Bsnnummer { get; private set; }
        public short Uzovi { get; private set; }

        public BsnUzoviQuery(int bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }
    }
}
