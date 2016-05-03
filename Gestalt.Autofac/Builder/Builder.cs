using Autofac;
using Gestalt.Core.Infrastructure;
using System;
using Gestalt.Abstractions;
using Gestalt.Autofac.Infrastructure;
using Gestalt.Abstractions.Infrastructure;
using Gestalt.ControllerFactory;
using Microsoft.AspNet.Mvc.Controllers;
using Autofac.Core;
using Gestalt.Controllers;

namespace Gestalt.Autofac.Builder
{
    public class Builder: Module
    {
        private IContainer container;
        public Builder(IContainer container)
        {
            //TODO This is kinda lazy. Write a lazy evaluator to replace this strategy.
            this.container = container;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder = new ContainerBuilder();

            builder.RegisterType<SchemaResolver>().As<ISchemaResolver>().SingleInstance();

            builder.Register<SchemaRegistry>(i => { return RegistrationHelpers.GenerateSchemaIndexFromImplementingConfigurationSchemas(RegistrationHelpers.ResolveSchemas()); })
                .As<ISchemaRegistry>()
                .SingleInstance();

            builder.RegisterType<AutofacGestaltControllerFactory>().As<IControllerFactory>().WithParameter(new ResolvedParameter((p,c)=>p.Name == "container", (p,c)=> container));

            //TODO This is ugly, write a core controller class with custom implementing interface
            builder.RegisterGeneric(typeof(GestaltConfigurationService<>)).AsSelf();

            builder.RegisterType<AutofacRepositoryFactory>().As<IRepositoryFactory>().WithParameter(new ResolvedParameter((p,c) => p.Name == "container", (p,c)=> container));
           
        }
    }
}
