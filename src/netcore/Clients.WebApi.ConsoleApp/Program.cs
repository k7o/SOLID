using Business.Contracts.Query.Zoek;
using System;
using System.Threading.Tasks;

namespace Clients.WebApi.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancellationToken = new System.Threading.CancellationToken();

            var commandClient = new CommandClient<Business.Contracts.Command.AddAdresCommand>();

            var queryClient = new QueryClient<AdresQuery, ZoekResult>();

            var commandTask = commandClient.HandleAsync(new Business.Contracts.Command.AddAdresCommand("1111AA"), cancellationToken);

            var queryTask = queryClient.HandleAsync(new AdresQuery("1111AA"), cancellationToken);
            
            var result = queryTask.Result;
            
            commandTask.Wait();
        }
    }
}
