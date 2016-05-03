using Microsoft.AspNet.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Gestalt.Core.Infrastructure;
using Autofac;
using System.IO;
using System.Text;

namespace Gestalt.ControllerFactory
{
    public class AutofacGestaltControllerFactory : IControllerFactory
    {
        private ISchemaResolver resolver;
        private IContainer container;
        public AutofacGestaltControllerFactory(ISchemaResolver resolver,IContainer container)
        {
            this.resolver = resolver;
            this.container = container;
        }
        public object CreateController(ActionContext actionContext)
        {
            if (actionContext.RouteData.DataTokens.Keys.Contains("Application"))
            {
                Type controllerType = Type.GetType("ConfigurationService").MakeGenericType(resolver.GetSchema(actionContext.RouteData.DataTokens["Application"].ToString()));
                var args = controllerType.GetGenericArguments();
                return Activator.CreateInstance(controllerType,args.Select(x=>container.Resolve(x)));
            }
            else {
                //TODO gracefully handle this exit
                actionContext.HttpContext.Response.StatusCode = 400;
                return null;
            }
        }

        public void ReleaseController(object controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
