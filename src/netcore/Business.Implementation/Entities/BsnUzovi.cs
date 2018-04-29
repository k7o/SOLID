namespace Business.Implementation.Entities
{
    public class BsnUzovi
    {
        public int Bsnnummer { get; private set; }

        public short Uzovi { get; private set; }

        public BsnUzovi(int bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }

        // Default constructors are needed for certain ORM frameworks
        private BsnUzovi()
        {
        }
    }
}
