using Gestalt.Abstractions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestalt.Core.Infrastructure
{
    public class SchemaRegistry : ISchemaRegistry
    {
        public SchemaRegistry(IEnumerable<KeyValuePair<string, Type>> registrationCollection)
        {
            Index = new Dictionary<string, Type>();
            registrationCollection.ToList().ForEach(reg => {
                Index.Add(reg.Key, reg.Value);
            });
        }
        public IDictionary<string, Type> Index { get; }
    };
}
