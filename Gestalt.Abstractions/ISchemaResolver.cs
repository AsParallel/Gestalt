using System;

namespace Gestalt.Core.Infrastructure
{
    public interface ISchemaResolver
    {
        Type GetSchema(string Schema);
    }
}