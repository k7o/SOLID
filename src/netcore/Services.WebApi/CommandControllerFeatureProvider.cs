using Contracts;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace Services.WebApi
{
    public class CommandControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var commandType in Bootstrapper.KnownCommandTypes)
            {
                var controllerType = typeof(CommandController<>).MakeGenericType(commandType).GetTypeInfo();
                feature.Controllers.Add(controllerType);
            }
        }
    }
}
