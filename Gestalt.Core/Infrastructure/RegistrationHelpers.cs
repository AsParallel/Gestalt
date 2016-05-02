using Gestalt.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Gestalt.Core.Infrastructure
{
    public class RegistrationHelpers
    {
        private static IEnumerable<Assembly> assemblies = new List<Assembly>();

        public static void RegisterAssembly(Assembly assem)
        {
            assemblies.ToList().Add(assem);
        }

        public static void RegisterAssemblies(IEnumerable<Assembly> assems)
        {
            assemblies.ToList().AddRange(assems);
        }

        public static IEnumerable<Type> ResolveSchemas()
        {
            return assemblies.Select(t => t.GetExportedTypes().Where(x =>
                   x.IsAssignableFrom(typeof(IGestaltConfigurationSchema)))).SelectMany(t => t);
        }

        ///The schema index is effectively a sideline lookup table to cross correlate Application configuration objects with their originating objects
        public static SchemaRegistry GenerateSchemaIndexFromImplementingConfigurationSchemas(IEnumerable<Type> types)
        {
            return new SchemaRegistry(types.Select(t =>
                new List<KeyValuePair<string, Type>>() {
                new KeyValuePair<string, Type>(t.Name,t),
               new KeyValuePair<string, Type>(t.FullName,t)
                }
             ).SelectMany(x => x));
        }
    }
}
