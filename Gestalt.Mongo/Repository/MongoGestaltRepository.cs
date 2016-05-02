using Gestalt.Abstractions;
using Gestalt.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Gestalt.Mongo.Repository
{
    public class MongoGestaltRepository<T> : IGestaltConfigurationRepository<T> where T : IGestaltConfigurationSchema
    {
        private IMongoDBContext<T> context;
        public MongoGestaltRepository(IMongoDBContext<T> context)
        {
            this.context = context;
        }
        public async void AddConfiguration(T config)
        {
            await context.Collection.InsertOneAsync(config);
        }

        public async void GetConfiguration(string id)
        {
            await context.Collection.FindAsync(x =>
            x.Id == id);
        }

        public async void GetConfiguration(string id, string version)
        {
            await context.Collection.FindAsync(x =>
            x.Id == id
            && x.Version == version);
        }

        public async void RemoveAllSchemaVersions(T config)
        {
            //delete all in collection
            await context.Collection.DeleteManyAsync(x => true);
        }

        public async void RemoveConfiguration(T config)
        {
            await context.Collection.FindOneAndDeleteAsync(x =>
            x.Id == config.Id
            && x.Version == config.Version
            && x.Environment == config.Environment);
        }

        public async void RemoveConfiguration(string id)
        {
            await context.Collection.FindOneAndDeleteAsync(x =>
            x.Id == id);
        }

        public async void RemoveConfiguration(T config, string environment, string version)
        {
            await context.Collection.FindOneAndDeleteAsync(x =>
            x.Id == config.Id
            && x.Environment == environment
            && x.Version == version);
        }

        public async void UpdateConfiguration(T config, string id)
        {
            await context.Collection.FindOneAndUpdateAsync<T>(x => 
            x.Id == id, new ObjectUpdateDefinition<T>(config), null, default(System.Threading.CancellationToken));
        }

        public async void UpdateConfiguration(T config)
        {
            await context.Collection.FindOneAndUpdateAsync<T>(x => 
            x.Id == config.Id
            && x.Version == config.Version
            && x.Environment == config.Environment, new ObjectUpdateDefinition<T>(config), null, default(System.Threading.CancellationToken));
        }

        public async void UpdateConfiguration(T config, string environment, string version)
        {
            await context.Collection.FindOneAndUpdateAsync<T>(x => 
            x.Id == config.Id
            && x.Environment == environment
            && x.Version == version, new ObjectUpdateDefinition<T>(config), null, default(System.Threading.CancellationToken));
        }
    }
}
