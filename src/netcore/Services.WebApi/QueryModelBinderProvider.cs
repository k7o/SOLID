﻿using Contracts;
using Crosscutting.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.WebApi
{
    public class QueryModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            if (context.Metadata.ModelType.GetInterface("IQuery`1") != null)
            {
                return new BinderTypeModelBinder(typeof(QueryModelBinder));
            }

            return null;
        }
    }
}
