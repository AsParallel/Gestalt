using Gestalt.Abstractions;
using Gestalt.Abstractions.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestalt.Mongo.Messages.Base
{
    public class MongoGestaltConfigBase: IGestaltConfigurationSchema
    {
        public string Environment
        {
            get; set;
        }

        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public string Id { get; set; }

        public string Version { get; set; }
    }
}
