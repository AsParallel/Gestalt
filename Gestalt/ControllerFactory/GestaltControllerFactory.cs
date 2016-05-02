using Microsoft.AspNet.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Gestalt.Core.Infrastructure;
using Autofac;

namespace Gestalt.ControllerFactory
{
    public class GestaltControllerFactory : IControllerFactory
    {
        private ISchemaResolver resolver;
        private IContainer container;
        public GestaltControllerFactory(ISchemaResolver resolver,IContainer container)
        {
            this.resolver = resolver;
            this.container = container;
        }
        public object CreateController(ActionContext actionContext)
        {
            if (actionContext.RouteData.DataTokens.Keys.Contains("Application"))
            {
                Type controllerType = Type.GetType("ConfigurationService").MakeGenericType(resolver.GetSchema(actionContext.RouteData.DataTokens["Application"].ToString()));
                return container.Resolve(controllerType);
            }
            else {
                actionContext.HttpContext.Response.StatusCode = 400;
                throw new Exception("The configuration partition key must be specified");
            }

        }

        public void ReleaseController(object controller)
        {
            //no functionality
        }
    }
}
