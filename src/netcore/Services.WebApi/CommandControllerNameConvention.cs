using Contracts;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.WebApi
{
    public class CommandControllerNameConvention : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.GetGenericTypeDefinition() !=
                typeof(CommandController<>))
            {
                // Not a CommandController, ignore.
                return;
            }

            var dtoType = controller.ControllerType.GenericTypeArguments[0];
            controller.ControllerName = dtoType.Name.RemoveFromEnd("Command");
        }
    }
}
