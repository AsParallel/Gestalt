using Autofac;
using Gestalt.AppSettings;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace Gestalt.Mongo.Builder
{
    public class Builder: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ///Mongo
            /////TODO When the 5.0 assemblies drop for this and autofac, move this builder registration portion to the Mongo assembly
            var mongoSettings = JObject.Parse(ConfigurationManager.AppSettings.Get("Mongo")).ToObject<MongoConfiguration>();

            builder.Register(x =>
            {
                return mongoSettings;
            }).As<IMongoConfiguration>();
            builder.RegisterGeneric(typeof(MongoDBContext<>))
           .As(typeof(IMongoDBContext<>))
           .InstancePerLifetimeScope();

            builder.Register(x =>
            {
                return new MongoClient(mongoSettings.ConnectionString);

            })
                 .As<IMongoClient>().SingleInstance();
        }
    }
}
