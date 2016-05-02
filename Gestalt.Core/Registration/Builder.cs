using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using System.Reflection;
using Gestalt.Abstractions;
using Microsoft.Framework.Runtime;
using Microsoft.AspNet.Mvc.Infrastructure;
using Gestalt.DataAccess;
using MongoDB.Driver;
using System.Configuration;
using Microsoft.Extensions.OptionsModel;
using Gestalt.AppSettings;
using Microsoft.Framework.ConfigurationModel.Json;
using Newtonsoft.Json.Linq;

namespace ZO.Gestalt.Core.Registration
{
    public static class Registrar
    {
        private static List<Assembly> assemblies = new List<Assembly>();
        private static ContainerBuilder builder;

        public static void RegisterConfigSchemasInAssembly(Assembly assem)
        {
            assemblies.Add(assem);
        }

        public static void RegisterConfigSchemasInAssemblies(IEnumerable<Assembly> assems)
        {
            assemblies.AddRange(assems);
        }

        private static void RegisterType(Type t)
        {
            builder.RegisterType(t).AsSelf();
        }

        public static IContainer CompileSchemas()
        {
            builder = new ContainerBuilder();

            typeof(Registrar).GetTypeInfo().Assembly.GetExportedTypes().Where(x => x.IsAssignableFrom(typeof(IGestaltConfigurationSchema)))
                .ToList()
                .ForEach(schema => builder.RegisterType(schema)
                .AsSelf());
            assemblies.ToList().ForEach(assem =>
            {
                assem.GetExportedTypes().Where(x => x.IsAssignableFrom(typeof(IGestaltConfigurationSchema)))
                .ToList()
                .ForEach(schema => builder.RegisterType(schema)
                .AsSelf());
            });

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

            return builder.Build();
        }
    }
}
