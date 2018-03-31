using Contracts;
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
    public class QueryClient<TQuery, TResult> : IAsyncQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public async Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder
                .Append("http://localhost:51964/")
                .Append($"api/query/{query.GetType().Name.Replace("Query", string.Empty, StringComparison.InvariantCulture)}?")
                .Append(query.ToQueryString());

            var client = new HttpClient();
            try
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = new HttpMethod("GET");

                    var url = urlBuilder.ToString();
                    request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false); 

                    var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    return (TResult) JsonConvert.DeserializeObject(responseData, typeof(TResult));
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
