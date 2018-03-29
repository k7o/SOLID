using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clients.WebApi.ConsoleApp
{
    public static class QueryExtensions
    {
        public static string ToQueryString<TQuery>(this TQuery query)
        {
            var jsonQuery = JsonConvert.SerializeObject(query, Formatting.None);

            var builder = new StringBuilder(jsonQuery);
            
            builder.Replace(":", "=");
            builder.Replace("{", "");
            builder.Replace("}", "");
            builder.Replace(",", "&");
            builder.Replace("\"", "");

            return builder.ToString();
        }
    }
}
