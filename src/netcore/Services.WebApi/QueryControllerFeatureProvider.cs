using Contracts;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using Business.Contracts.Query.Zoek;

namespace Services.WebApi
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
