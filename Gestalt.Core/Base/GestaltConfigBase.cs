using Gestalt.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestalt.Abstractions.Messages.Base
{
    public abstract class GestaltConfigBase:IGestaltConfigurationSchema
    {
        public string Id { get; set; }
        public string Environment { get; set; }
        public string Version { get; set; }
        public string Application { get; set; }
    }
}
