using Crosscutting.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.WebApi.ModelBinders
{
    public class QueryModelBinder : IModelBinder
    {
        readonly IModelBinder _fallbackBinder;

        public QueryModelBinder(IModelBinder fallbackBinder)
        {
            Guard.IsNotNull(fallbackBinder, nameof(fallbackBinder));

            _fallbackBinder = fallbackBinder;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Guard.IsNotNull(bindingContext, nameof(bindingContext));

            // when not GET request, fallback
            if (bindingContext.HttpContext.Request.Method != "GET")
            {
                return _fallbackBinder.BindModelAsync(bindingContext);
            }

            var json = SerializationHelpers.ConvertQueryStringToJson(bindingContext.HttpContext.Request.QueryString.Value);
            var query = JsonConvert.DeserializeObject(json, bindingContext.ModelType);
            bindingContext.Result = ModelBindingResult.Success(query);

            return Task.CompletedTask;
        }
    }
}
