﻿using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;
using System.Reflection;
using Services.WebApi.Controllers;

using System.Linq;
using Business.Contracts.Command;

namespace Services.WebApi.FeatureProviders
{
    public class CommandMediatRControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var controllerType = typeof(CommandMediatRController<>).MakeGenericType(typeof(AddAdresCommand)).GetTypeInfo();
            feature.Controllers.Add(controllerType);
        }
    }
}
