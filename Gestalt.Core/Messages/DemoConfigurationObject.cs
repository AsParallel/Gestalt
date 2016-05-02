using Gestalt.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestalt.Core.Messages
{
    public class DemoDataBaseConfigurationObject: Gestalt.Mongo.Messages.Base.MongoGestaltConfigBase
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
