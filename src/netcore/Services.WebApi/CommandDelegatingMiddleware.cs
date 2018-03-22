using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.WebApi
{

    //public class CommandDelegatingMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly ILogger _logger;
    //    private IApiKeyService _service;

    //    public CommandDelegatingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IApiKeyService service)
    //    {
    //        _next = next;
    //        _logger = loggerFactory.CreateLogger<ApiKeyMiddleware>();
    //        _service = service1
    //    }

    //    public async Task Invoke(HttpContext context)
    //    {
    //        _logger.LogInformation("Handling API key for: " + context.Request.Path);

    //        // do custom stuff here with service      

    //        await _next.Invoke(context);

    //        _logger.LogInformation("Finished handling api key.");
    //    }
    //}
}
