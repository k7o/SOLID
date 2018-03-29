using Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clients.WebApi.ConsoleApp
{
    class CommandClient<TCommand> : IAsyncCommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder
                .Append("http://localhost:51964/")
                .Append($"api/command/{command.GetType().Name.Replace("Command", string.Empty, StringComparison.InvariantCulture)}");

            var client = new HttpClient();
            try
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = new HttpMethod("POST");

                    var content = new StringContent(JsonConvert.SerializeObject(command, Formatting.None));
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request.Content = content;

                    var url = urlBuilder.ToString();
                    request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                    await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                var message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                throw;
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }
        }
    }
}
