using Business.Contracts.Query.Zoek;
using System;
using System.Threading.Tasks;

namespace Clients.WebApi.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new QueryClient<AdresQuery, ZoekResult>();

            var handleTask = client.HandleAsync(new AdresQuery("1111AA"), new System.Threading.CancellationToken());
            
            var result = handleTask.Result;


            handleTask.Wait();
        }
    }
}
