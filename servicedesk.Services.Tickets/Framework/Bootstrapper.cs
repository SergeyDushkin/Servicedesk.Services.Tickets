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
using servicedesk.Services.Tickets.Repositories;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Dal;
using servicedesk.Services.Tickets.Domain;
using Microsoft.EntityFrameworkCore;
using Nancy.Configuration;
using Polly;
using System;
using System.IO;

using RabbitMQ.Client.Exceptions;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.vNext;

namespace servicedesk.Services.Tickets.Framework
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

                builder.RegisterType<TicketRepository>().As<ITicketRepository>();
                builder.RegisterType<TicketService>().As<ITicketService>();

                builder.RegisterType<BaseRepository<Address, TicketDbContext>>().As<IBaseRepository<Address>>();
                builder.RegisterType<BaseRepository<Client, TicketDbContext>>().As<IBaseRepository<Client>>();
                builder.RegisterType<BaseRepository<User, TicketDbContext>>().As<IBaseRepository<User>>();
                
                builder.RegisterType<BaseService<Address>>().As<IBaseService<Address>>();
                builder.RegisterType<BaseService<Client>>().As<IBaseService<Client>>();
                builder.RegisterType<BaseService<User>>().As<IBaseService<User>>();

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
            /// Create Database
            container.Resolve<TicketDbContext>().Database.EnsureCreated();

            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST,PUT,GET,OPTIONS,DELETE");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers",
                    "Authorization, Origin, X-Requested-With, Content-Type, Accept");
            };
            _exceptionHandler = container.Resolve<IExceptionHandler>();
            Logger.Info("servicedesk.Services.Tickets API has started.");
        }
    }
}