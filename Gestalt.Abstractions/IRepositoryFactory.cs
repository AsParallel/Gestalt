using Gestalt.Abstractions;
using System;

namespace Gestalt.Abstractions.Infrastructure
{
    public interface IRepositoryFactory
    {
        IGestaltConfigurationRepository<T> GetRepository<T>() where T : IGestaltConfigurationSchema;

        //object GetRepository(Type t);
    }
}