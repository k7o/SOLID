namespace Clients.Wcf.ConsoleApp.Controllers
{
    using Business.Contracts.Query.Zoek;
    using Contracts;

    public class QueryExampleController
    {
        private readonly IQueryProcessor _queryProcessor;

        public QueryExampleController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public bool IsAdresInWhitelist(string postcode)
        {
            return _queryProcessor.Execute(new AdresQuery(postcode)).InWhitelist;
        }

        public bool IsBsnUzoviInWhitelist(int bsnnummer, short uzovi)
        {
            return _queryProcessor.Execute(new BsnUzoviQuery(bsnnummer, uzovi)).InWhitelist;
        }
    }
}
