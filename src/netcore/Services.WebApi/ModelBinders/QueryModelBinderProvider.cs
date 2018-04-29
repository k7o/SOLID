using Crosscutting.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;

namespace Services.WebApi.ModelBinders
{
    public class QueryModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            Guard.IsNotNull(context, nameof(context));
            
            if (context.Metadata.ModelType.GetInterface("IRequest`1") != null &&
                context.Metadata.ModelType.Name.EndsWith("Query", StringComparison.OrdinalIgnoreCase))
            {
                var fallbackBinder = new BinderTypeModelBinder(typeof(ComplexTypeModelBinder));
                return new QueryModelBinder(fallbackBinder);
            }

            return null;
        }
    }
}
