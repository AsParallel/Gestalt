using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace Gestalt.Autofac
{
    /// <summary>
    /// This class is intended for use with module-only registration
    /// </summary>
    public static class AutofacModuleRegistrationHelper
    {
        private static List<Module> builders = new
            List<Module>();

        public static void RegisterModule(Module builder)
        {
            builders.Add(builder);
        }

        public static void RegisterModules(IEnumerable<Module> b)
        {
            builders.AddRange(b);
        }

        /// <summary>
        /// Returns the Container Builder
        /// </summary>
        /// <returns></returns>
        public static ContainerBuilder RegisterAll()
        {
            ContainerBuilder b = new ContainerBuilder();
            builders.ForEach(builder => b.RegisterModule(builder));
            return b;
        }

        public static ContainerBuilder RegisterAll(ContainerBuilder b)
        {
            builders.ForEach(builder => b.RegisterModule(builder));
            return b;
        }

        /// <summary>
        /// Returns the IContainer
        /// </summary>
        /// <returns></returns>
        public static IContainer Compile()
        {
            ContainerBuilder b = new ContainerBuilder();
            builders.ForEach(builder => b.RegisterModule(builder));
            return b.Build();
        }
    }
}
