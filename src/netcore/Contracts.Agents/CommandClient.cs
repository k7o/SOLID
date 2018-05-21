using Crosscutting.Contracts;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Agents
{
    public class CommandClient<TCommand> : IRequestHandler<TCommand> where TCommand : class, IRequest
    {
        public async Task Handle(TCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            var urlBuilder = new StringBuilder();
            urlBuilder
                .Append("http://localhost:51964/")
                .Append($"api/command/{request.GetType().Name.Replace("Command", string.Empty, StringComparison.InvariantCulture)}");

            var client = new HttpClient();

            using (var clientRequest = new HttpRequestMessage())
            {
                clientRequest.Method = new HttpMethod("POST");

                var content = new StringContent(JsonConvert.SerializeObject(request, Formatting.None));
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                clientRequest.Content = content;

                var url = urlBuilder.ToString();
                clientRequest.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                await client.SendAsync(clientRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
