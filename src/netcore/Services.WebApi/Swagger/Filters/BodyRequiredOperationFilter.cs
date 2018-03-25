using Crosscutting.Contracts;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.WebApi.Swagger.Filters
{
    public class BodyRequiredOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            Guard.IsNotNull(operation, nameof(operation));
            Guard.IsNotNull(context, nameof(context));

            BodyParameter bodyParameter = operation
                .Parameters?
                .OfType<BodyParameter>()
                .SingleOrDefault();

            if (bodyParameter != null)
            {
                bodyParameter.Required = true;
            }
        }
    }
}
