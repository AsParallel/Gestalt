using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestalt.Abstractions
{
    public interface IGestaltConfigurationRepository<T>
    {
        void GetConfiguration(string id);
        void GetConfiguration(string id, string version);
        void AddConfiguration(T config);
        void UpdateConfiguration(T config);
        void UpdateConfiguration(T config, string version);
        void UpdateConfiguration(T config, string environment, string version);
        void RemoveConfiguration(string id);
        void RemoveConfiguration(T config);
        void RemoveConfiguration(T config, string environment, string version);
        void RemoveAllSchemaVersions(T config);

    }
}
