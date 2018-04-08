using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;
using System.Reflection;
using Services.WebApi.Controllers;
using System.Linq;
using Contracts;

namespace Services.WebApi.FeatureProviders
{
    public class QueryControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var queryType in Bootstrapper.KnownQueryTypes)
            {
                var controllerType = typeof(QueryController<,>).MakeGenericType(queryType.QueryType, queryType.ResultType).GetTypeInfo();
                feature.Controllers.Add(controllerType);
            }
        }
    }
}
