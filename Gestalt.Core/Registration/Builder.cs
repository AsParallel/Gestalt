using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using System.Reflection;
using Gestalt.Abstractions;
using Microsoft.Framework.Runtime;
using Microsoft.AspNet.Mvc.Infrastructure;

namespace ZO.Gestalt.Core.Registration
{

    public static class Registrar
    {
        private static List<Assembly> assemblies = new List<Assembly>();
        private static ContainerBuilder builder;

        public static void RegisterConfigSchemasInAssembly(Assembly assem)
        {
            assemblies.Add(assem);
        }

        public static void RegisterConfigSchemasInAssemblies(IEnumerable<Assembly> assems)
        {
            assemblies.AddRange(assems);
        }

        private static void RegisterType(Type t)
        {
            builder.RegisterType(t).AsSelf();
        }

        public static IContainer CompileSchemas()
        {
            builder = new ContainerBuilder();

            typeof(Registrar).GetTypeInfo().Assembly.GetExportedTypes().Where(x => x.IsAssignableFrom(typeof(IGestaltConfigurationSchema)))
                .ToList()
                .ForEach(schema => builder.RegisterType(schema)
                .AsSelf());
            assemblies.ToList().ForEach(assem =>
            {
                assem.GetExportedTypes().Where(x => x.IsAssignableFrom(typeof(IGestaltConfigurationSchema)))
                .ToList()
                .ForEach(schema => builder.RegisterType(schema)
                .AsSelf());
            });
            return builder.Build();
        }
    }
}
