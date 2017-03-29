using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using servicedesk.Services.Tickets.Dal;
using servicedesk.Services.Tickets.Framework;
using servicedesk.Services.Tickets.Services;
using servicedesk.Common.Repositories;
using servicedesk.Common.Services;
using servicedesk.Common.Events;
using servicedesk.Common.Commands;

using RawRabbit.Configuration;


using RabbitMQ.Client.Exceptions;
using RawRabbit;
using RawRabbit.vNext;
using Polly;
using System.IO;
using System.Reflection;
using servicedesk.Common.Extensions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;

namespace servicedesk.Services.Tickets
{
    public class Startup
    {
        public string EnvironmentName {get;set;}
        public IConfiguration Configuration { get; set; }
        public IContainer ApplicationContainer { get; set; }
        public static ILifetimeScope LifetimeScope { get; private set; }
        public Microsoft.Extensions.Logging.ILogger Logger { get; set; }

        public Startup(IHostingEnvironment env)
        {
            EnvironmentName = env.EnvironmentName.ToLowerInvariant();
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .SetBasePath(env.ContentRootPath);

            Configuration = builder.Build();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            var serilogLogger = new LoggerConfiguration()
                .Enrich.WithProperty("Application","ServiceDesk.Services.Tickets")
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            loggerFactory.AddSerilog(serilogLogger);
            loggerFactory.AddConsole();

            Logger = loggerFactory.CreateLogger("ServiceDesk.Services.Tickets");

            
            app.UseCors("cors");
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServiceDesk.Services.Tickets API");
            });


            if (app.ApplicationServices.GetService<TicketDbContext>().Database.EnsureCreated())
            {
                app.ApplicationServices.GetService<IDatabaseSeeder>().SeedAsync();
            }

            // If you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TicketDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("TicketDatabase")));

            services.AddCors(x => x.AddPolicy("cors", policy => {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
                policy.AllowCredentials();
            }));
            
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ServiceDesk.Services.Tickets API", Version = "v1" });
            });

            // Create the Autofac container builder.
            var builder = new ContainerBuilder();

            // Add any Autofac modules or registrations.
            builder.RegisterModule(new AutofacModule(Configuration, Logger));

            // Populate the services.
            builder.Populate(services);

            // Build the container.
            ApplicationContainer = builder.Build();

            LifetimeScope = ApplicationContainer;

            // Create and return the service provider.
            return new AutofacServiceProvider(ApplicationContainer);
        }
    }

    public class AutofacModule : Autofac.Module
    {
        IConfiguration configuration { get; }
        Microsoft.Extensions.Logging.ILogger logger  { get; }

        public AutofacModule(IConfiguration configuration, Microsoft.Extensions.Logging.ILogger logger) 
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        protected override void Load(ContainerBuilder builder)
        {
            var rmqRetryPolicy = Policy
                .Handle<ConnectFailureException>()
                .Or<BrokerUnreachableException>()
                .Or<IOException>()
                .WaitAndRetry(5, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) => {
                        logger.LogError(new EventId(10001, "RabbitMQ Connect Error"), exception, $"Cannot connect to RabbitMQ. retryCount:{retryCount}, duration:{timeSpan}");
                    }
                );
                
            builder.RegisterInstance(AutoMapperConfig.InitializeMapper());

            builder.RegisterType<DatabaseInitializer>().As<IDatabaseSeeder>();
            builder.RegisterType<BaseRepository<TicketDbContext>>().As<IBaseRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BaseService>().As<IBaseService>().InstancePerLifetimeScope();
            builder.RegisterType<BaseDependentlyService>().As<IBaseDependentlyService>().InstancePerLifetimeScope();
            
            builder.RegisterType<TicketService>().As<ITicketService>().InstancePerLifetimeScope();

            builder.RegisterType<Handler>().As<IHandler>();

            var rawRabbitConfiguration = configuration.GetSettings<RawRabbitConfiguration>();
            builder.RegisterInstance(rawRabbitConfiguration).SingleInstance();
            rmqRetryPolicy.Execute(() => builder
                    .RegisterInstance(BusClientFactory.CreateDefault(rawRabbitConfiguration))
                    .As<IBusClient>()
            );

            var assembly = typeof(Startup).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEventHandler<>));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(ICommandHandler<>));
        }
    }
}