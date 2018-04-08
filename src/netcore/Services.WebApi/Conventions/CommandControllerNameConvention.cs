﻿using Contracts;
using Crosscutting.Contracts;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.WebApi.Controllers.Conventions
{
    public class CommandControllerNameConvention : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controllerModel)
        {
            Guard.IsNotNull(controllerModel, nameof(controllerModel));

            if (controllerModel.ControllerType.GetGenericTypeDefinition() !=
                typeof(CommandController<>))
            {
                // Not a CommandController, ignore.
                return;
            }

            var dtoType = controllerModel.ControllerType.GenericTypeArguments[0];
            controllerModel.ControllerName = dtoType.Name.RemoveFromEnd("Command");
        }
    }
}