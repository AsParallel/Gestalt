using System.Configuration;
using System.Collections;
using System.Threading;
using System;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.OptionsModel;
using Gestalt.AppSettings;

namespace Gestalt.Mongo
{
    /// <summary>
    /// Provides the ORM access for MongoDB. Maintains clientless
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoDBContext<T> : IMongoDBContext<T>
    {
        private IMongoDatabase database;
        private IMongoClient client;
        private IDataAccessBase<T> resolver;
        private IMongoConfiguration mongoSettings;
        public IDataAccessBase<T> Resolver
        {
            get
            {
                return resolver;
            }
        }

        public IMongoClient Client
        {
            get
            {
                return client;
            }
        }

        public IMongoDatabase Database
        {
            get
            {
                return database;
            }
        }

        public MongoDBContext(IMongoClient client, IDataAccessBase<T> resolver, IMongoConfiguration config)
        {
            this.client = client;
            this.resolver = resolver;
            this.mongoSettings = config;
            database = client.GetDatabase(mongoSettings.DatabaseName);
        }

        public IMongoCollection<T> Collection
        {
            get
            {
                return Database.GetCollection<T>(resolver.GetCollectionName());
            }
        }
    }

    public class MongoContextDatabaseInitializationError : System.Exception
    {
        public MongoContextDatabaseInitializationError(System.Exception ex) : base("Database Could not be initialized", ex) { }
        public MongoContextDatabaseInitializationError(System.Exception ex, string msg) : base(string.IsNullOrEmpty(msg) ? msg : "Database Could not be initialized", ex) { }
    }

    public class MongoCollectionNameRegistrationError<T> : System.Exception
    {
        public MongoCollectionNameRegistrationError(System.Exception ex)
            : base("No type registration found for" + typeof(T).Name, ex)
        {
            ComputeError();
        }
        public MongoCollectionNameRegistrationError(string msg, System.Exception ex)
            : base(msg == string.Empty ? msg + ", see Data for details" : "No type registration found", ex)
        {
            ComputeError();
        }

        public void ComputeError()
        {
            Data.Add("Type", typeof(T));
            Data.Add("TypeError", "No type registration found for type" + typeof(T).Name);
        }
    }

 
}