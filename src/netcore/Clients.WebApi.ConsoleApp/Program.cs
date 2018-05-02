using Contracts.Agents;
using Dtos.Features.AddToWhitelist;
using Dtos.Features.InWhitelist;
using System.Threading;

namespace Clients.WebApi.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancellationToken = new CancellationToken();

            var commandClient = new CommandClient<AddAdresToWhitelistCommand>();
            var queryClient = new QueryClient<AdresInWhitelistQuery, ZoekResult>();

            var commandHandleTask = commandClient.Handle(new AddAdresToWhitelistCommand("1111AA"), cancellationToken);

            var queryHandleTask = queryClient.Handle(new AdresInWhitelistQuery("1111AA"), cancellationToken);
            
            var result = queryHandleTask.Result;
            //// what todo;)

            commandHandleTask.Wait();
            queryHandleTask.Wait();
        }
    }
}
