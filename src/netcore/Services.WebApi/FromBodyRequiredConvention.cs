using Crosscutting.Contracts;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.WebApi
{
    public class FromBodyRequiredConvention : Attribute, IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            Guard.IsNotNull(action, nameof(action));

            var parameterName = action.Parameters
                .Where(p => p.BindingInfo?.BindingSource.CanAcceptDataFrom(BindingSource.Body) ?? false)
                .Select(p => p.ParameterName)
                .SingleOrDefault();

            if (parameterName != null)
            {
                action.Filters.Add(new FromBodyRequiredActionFilter(parameterName));
            }
        }

        private class FromBodyRequiredActionFilter : IActionFilter
        {
            private readonly string _parameterName;

            public FromBodyRequiredActionFilter(string parameterName)
            {
                _parameterName = parameterName;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                object value;
                context.ActionArguments.TryGetValue(_parameterName, out value);

                if (value == null)
                {
                    // Now uncomment one of these depending on what you want to do.
                    //
                    // This will return a 400 with an error in json.
                    //context.Result = new BadRequestObjectResult(new { message = "The body is required" });

                    // This will add a validation error and let the action decide what to do
                    //context.ModelState.AddModelError(_parameterName, "The body is required");
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                // not needed
            }
        }
    }
}
