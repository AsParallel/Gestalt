using Gestalt.Autofac;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac.Framework.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System;

namespace Gestalt
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build().ReloadOnChanged("appsettings.json");
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            /////Register all the services
            services.AddMvc();
            IContainer container = null;
            //Mongo settings registration
            services.Configure<Gestalt.AppSettings.MongoConfiguration>(Configuration.GetSection("Mongo"));
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            //add autofac registrations
            //This has a pretty low manageability score, because of exhaustive strong coupling, but it is what it is
            //TODO define less aggressive module registration practices
            AutofacModuleRegistrationHelper.RegisterModule(new Autofac.Builder.Builder(container));
            AutofacModuleRegistrationHelper.RegisterModule(new Mongo.Builder.Builder());
            var containerBuilder = AutofacModuleRegistrationHelper.RegisterAll();

            //compile services registrations as autofac registrations
            //this functionality provided by Autofac.Extensions.DependencyInjection
            containerBuilder.Populate(services);

            container = containerBuilder.Build();

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseApplicationInsightsRequestTelemetry();
            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();
            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}