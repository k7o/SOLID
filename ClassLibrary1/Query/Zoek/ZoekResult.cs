namespace ClassLibrary1.Query.Zoek
{
    
    public class ZoekResult
    {
        public ZoekResult(bool inWhitelist) 
        {
            InWhitelist = inWhitelist;
        }
                
        public bool InWhitelist { get; private set; }
    }
}
