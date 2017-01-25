using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using NLog;
using Coolector.Common.Services;
using Coolector.Common.Nancy;
using Coolector.Common.Extensions;
using Coolector.Common.Exceptionless;
using servicedesk.Common.Commands;
using servicedesk.Common.Events;
using serviceDesk.Services.Tickets.Repositories;
using serviceDesk.Services.Tickets.Services;
using serviceDesk.Services.Tickets.Dal;
using Microsoft.EntityFrameworkCore;
using Nancy.Configuration;
using Polly;
using System;
using System.Collections.Generic;
using System.IO;

using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.vNext;

namespace serviceDesk.Services.Tickets.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static IExceptionHandler _exceptionHandler;
        private readonly IConfiguration _configuration;

        public static ILifetimeScope LifeTimeScope { get; private set; }

        public Bootstrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(enabled: true, displayErrorTraces: true);
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            Logger.Info("ServiceDesk.Services.Tickets Configuring application container");

            base.ConfigureApplicationContainer(container);

            var rmqRetryPolicy = Policy
                .Handle<ConnectFailureException>()
                .Or<BrokerUnreachableException>()
                .Or<IOException>()
                .WaitAndRetry(5, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) => {
                        Logger.Error(exception, $"Cannot connect to RabbitMQ. retryCount:{retryCount}, duration:{timeSpan}");
                    }
                );

            container.Update(builder =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TicketDbContext>();
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("TicketDatabase"));
                
                builder.RegisterType<TicketDbContext>().WithParameter("options", optionsBuilder.Options).AsSelf();

                builder.RegisterInstance(AutoMapperConfig.InitializeMapper());
                builder.RegisterInstance(_configuration.GetSettings<ExceptionlessSettings>()).SingleInstance();
                builder.RegisterType<ExceptionlessExceptionHandler>().As<IExceptionHandler>().SingleInstance();

                builder.RegisterType<StatusEventRepository>().As<IStatusEventRepository>();
                builder.RegisterType<StatusEventService>().As<IStatusEventService>();

                builder.RegisterType<StatusSourceRepository>().As<IStatusSourceRepository>();
                builder.RegisterType<StatusSourceService>().As<IStatusSourceService>();

                builder.RegisterType<StatusRepository>().As<IStatusRepository>();
                builder.RegisterType<StatusService>().As<IStatusService>();

                builder.RegisterType<StatusManager>().As<IStatusManager>();
                
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
                _exceptionHandler.Handle(ex, ctx.ToExceptionData(),
                    "Request details", "servicedesk", "Service", "Tickets");

                return ctx.Response;
            });
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            //var databaseSettings = container.Resolve<MongoDbSettings>();
            //var databaseInitializer = container.Resolve<IDatabaseInitializer>();
            //databaseInitializer.InitializeAsync();

            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST,PUT,GET,OPTIONS,DELETE");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers",
                    "Authorization, Origin, X-Requested-With, Content-Type, Accept");
            };
            _exceptionHandler = container.Resolve<IExceptionHandler>();
            Logger.Info("serviceDesk.Services.Tickets API has started.");
        }
    }
}