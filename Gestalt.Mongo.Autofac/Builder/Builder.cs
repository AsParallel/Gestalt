using Autofac;
using Gestalt.AppSettings;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace Gestalt.Mongo.Builder
{
    public class Builder: Module
    {
        private AppSettings.MongoConfiguration config;

        public Builder(AppSettings.MongoConfiguration config)
        {
            this.config = config;
        }
        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(x =>
            {
                return config;
            }).As<IMongoConfiguration>();
            builder.RegisterGeneric(typeof(MongoDBContext<>))
           .As(typeof(IMongoDBContext<>))
           .InstancePerLifetimeScope();

            builder.Register(x =>
            {
                return new MongoClient(config.ConnectionString);

            })
                 .As<IMongoClient>().SingleInstance();
        }
    }
}
