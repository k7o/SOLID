using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.WebApi
{
    public class QueryControllerNameConvention : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
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
