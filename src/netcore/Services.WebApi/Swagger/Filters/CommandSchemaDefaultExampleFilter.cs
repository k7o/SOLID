using Contracts;
using Crosscutting.Contracts;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Runtime.Serialization;

namespace Services.WebApi.Swagger.Filters
{
    public class CommandSchemaDefaultExampleFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            Guard.IsNotNull(model, nameof(model));
            Guard.IsNotNull(context, nameof(context));

            if (context.SystemType.GetInterfaces().Any(@interface => @interface == typeof(ICommand)))
            {
                model.Example = FormatterServices.GetUninitializedObject(context.SystemType);
            }
        }
    }
}
