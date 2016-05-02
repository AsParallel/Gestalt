using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using System.Reflection;

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

        //public static IContainer Compile()
        //{
        //    builder = new ContainerBuilder();
        //    typeof(Registrar).GetTypeInfo().Assembly.GetTypes().Where(x => x.IsAssignableFrom(typeof(IGestaltConfigurationDefinition)));
        //    assemblies.AsParallel().ForAll(assem =>
        //    {
        //        builder
        //    });
        //}
    }
}
