using Autofac;
using Gestalt.Core.Infrastructure;
using System;
using Gestalt.Abstractions;
using Gestalt.Autofac.Infrastructure;
using Gestalt.Abstractions.Infrastructure;
using Gestalt.ControllerFactory;
using Microsoft.AspNet.Mvc.Controllers;
using Autofac.Core;

namespace Gestalt.Autofac.Builder
{
    public class Builder: Module
    {
        private IContainer container;
        public Builder(IContainer container)
        {
            this.container = container;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder = new ContainerBuilder();

            builder.RegisterType<SchemaResolver>().As<ISchemaResolver>().SingleInstance();

            builder.Register<SchemaRegistry>(i => { return RegistrationHelpers.GenerateSchemaIndexFromImplementingConfigurationSchemas(RegistrationHelpers.ResolveSchemas()); })
                .As<ISchemaRegistry>()
                .SingleInstance();

            builder.RegisterType<AutofacGestaltControllerFactory>().As<IControllerFactory>().WithParameter(new ResolvedParameter((p,ctx)=>p.Name == "container", (p,ctx)=> container));

            builder.RegisterType<AutofacRepositoryFactory>().As<IRepositoryFactory>().WithParameter(new ResolvedParameter((x,y) => x.Name == "container", (x,y)=> container));
           
        }
    }
}
