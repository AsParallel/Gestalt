using System;
using System.Collections.Generic;

namespace Gestalt.Abstractions.Infrastructure
{
    public interface ISchemaRegistry
    {
        IDictionary<string, Type> Index { get; }
    }
}