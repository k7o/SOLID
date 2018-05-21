using Crosscutting.Contracts;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;

namespace Services.WebApi.Controllers.Conventions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate)]
    public sealed class QueryControllerNameConventionAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            Guard.IsNotNull(controller, nameof(controller));

            if (controller.ControllerType.GetGenericTypeDefinition() !=
                typeof(QueryController<,>))
            {
                // Not a QueryController, ignore.
                return;
            }

            var dtoType = controller.ControllerType.GenericTypeArguments[0];
            controller.ControllerName = dtoType.Name.RemoveFromEnd("Query");
        }
    }
}
