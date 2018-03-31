using Business.Contracts.Command;
using Business.Contracts.Query.Zoek;
using Contracts.Agents;
using System.Threading;

namespace Clients.WebApi.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancellationToken = new CancellationToken();

            var commandClient = new CommandClient<AddAdresCommand>();
            var queryClient = new QueryClient<AdresQuery, ZoekResult>();

            var commandHandleTask = commandClient.HandleAsync(new AddAdresCommand("1111AA"), cancellationToken);

            var queryHandleTask = queryClient.HandleAsync(new AdresQuery("1111AA"), cancellationToken);
            
            var result = queryHandleTask.Result;
            // what todo;)

            commandHandleTask.Wait();
            queryHandleTask.Wait();
        }
    }
}
