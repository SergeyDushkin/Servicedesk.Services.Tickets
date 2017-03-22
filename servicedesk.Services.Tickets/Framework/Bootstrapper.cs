using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Collectively.Common.Extensions;
using servicedesk.Common.Commands;
using servicedesk.Common.Events;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Dal;
using Polly;
using System;
using System.IO;

using RabbitMQ.Client.Exceptions;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using servicedesk.Common.Repositories;

namespace servicedesk.Services.Tickets.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private readonly ILogger logger;

        private readonly IConfiguration _configuration;

        public static ILifetimeScope LifeTimeScope { get; private set; }

        //private static IExceptionHandler _exceptionHandler;

        public Bootstrapper(IContainer container, IConfiguration configuration, ILogger logger) : base(container)
        {
            this._configuration = configuration;
            this.logger = logger;
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(enabled: true, displayErrorTraces: true);
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            logger.LogInformation("ServiceDesk.Services.Tickets Configuring application container");

            base.ConfigureApplicationContainer(container);

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

            container.Update(builder =>
            {
                builder.RegisterInstance(AutoMapperConfig.InitializeMapper());

                builder.RegisterType<CustomJsonSerializer>().As<JsonSerializer>().SingleInstance();
                
                builder.RegisterType<DatabaseInitializer>().As<IDatabaseSeeder>();
                builder.RegisterType<BaseRepository<TicketDbContext>>().As<IBaseRepository>();
                builder.RegisterType<BaseService>().As<IBaseService>();
                builder.RegisterType<BaseDependentlyService>().As<IBaseDependentlyService>();
                
                builder.RegisterType<TicketService>().As<ITicketService>();


                builder.RegisterType<Handler>().As<IHandler>();

                var rawRabbitConfiguration = _configuration.GetSettings<RawRabbitConfiguration>();
                builder.RegisterInstance(rawRabbitConfiguration).SingleInstance();
                rmqRetryPolicy.Execute(() => builder
                        .RegisterInstance(BusClientFactory.CreateDefault(rawRabbitConfiguration))
                        .As<IBusClient>()
                );

                var assembly = typeof(Startup).GetTypeInfo().Assembly;
                builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEventHandler<>));
                builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(ICommandHandler<>));
            });

            LifeTimeScope = container;
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                //_exceptionHandler.Handle(ex, ctx.ToExceptionData(),
                //    "Request details", "servicedesk", "Service", "Tickets");

                return ctx.Response;
            });
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            /// Create Database
            container.Resolve<TicketDbContext>().Database.EnsureCreated();

            /// Init Database
            var databaseSeeder = container.Resolve<IDatabaseSeeder>();
            databaseSeeder.SeedAsync();

            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST,PUT,GET,OPTIONS,DELETE");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers",
                    "Authorization, Origin, X-Requested-With, Content-Type, Accept");
            };

            logger.LogInformation("servicedesk.Services.Tickets API has started.");

            //_exceptionHandler = container.Resolve<IExceptionHandler>();
        }
    }

    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            this.ContractResolver = new CamelCasePropertyNamesContractResolver();
            this.Formatting = Formatting.Indented;
        }
    }
}