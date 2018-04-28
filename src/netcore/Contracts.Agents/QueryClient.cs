using Contracts;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Agents
{
    public class QueryClient<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IRequest<TResult>
    {
        public async Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder
                .Append("http://localhost:51964/")
                .Append($"api/query/{request.GetType().Name.Replace("Query", string.Empty, StringComparison.InvariantCulture)}?")
                .Append(request.ToQueryString());

            var client = new HttpClient();

            using (var clientRequest = new HttpRequestMessage())
            {
                clientRequest.Method = new HttpMethod("GET");

                var url = urlBuilder.ToString();
                clientRequest.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                var response = await client.SendAsync(clientRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false); 

                var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return (TResult) JsonConvert.DeserializeObject(responseData, typeof(TResult));
            }
        }
    }
}
