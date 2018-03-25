using Crosscutting.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.WebApi
{
    public class QueryModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Guard.IsNotNull(bindingContext, nameof(bindingContext));

            var json = SerializationHelpers.ConvertQueryStringToJson(
                bindingContext.HttpContext.Request.QueryString.Value);
            
            // ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }
    }
}
