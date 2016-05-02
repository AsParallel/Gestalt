using Gestalt.Abstractions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestalt.Core.Infrastructure
{
    public class SchemaResolver : ISchemaResolver
    {
        private ISchemaRegistry index;
        private IDictionary<string, Type> cache = new Dictionary<string, Type>();
        public SchemaResolver(ISchemaRegistry index)
        {
            this.index = index;
            cache = index.Index;
        }

        public Type GetSchema(string Schema)
        {
            if (cache.ContainsKey(Schema))
            {
                return cache[Schema];
            }
            throw new System.Exception(string.Format("Schema {0} is not registered with index, are you missing a registration?",Schema));
        }
    }
}
