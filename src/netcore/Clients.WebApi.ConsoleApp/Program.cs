using Business.Contracts.Command;
using Business.Contracts.Query.InWhitelist;
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

            var commandHandleTask = commandClient.Handle(new AddAdresCommand("1111AA"), cancellationToken);

            var queryHandleTask = queryClient.Handle(new AdresQuery("1111AA"), cancellationToken);
            
            var result = queryHandleTask.Result;
            //// what todo;)

            commandHandleTask.Wait();
            queryHandleTask.Wait();
        }
    }
}
