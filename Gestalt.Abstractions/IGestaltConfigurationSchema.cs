using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestalt.Abstractions
{
    //marker interface
    public interface IGestaltConfigurationSchema
    {
        string Id { get; set; }
        string Environment { get; set; }
        string Version { get; set; }
    }
}
