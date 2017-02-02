using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Coolector.Common.Extensions;
using servicedesk.Common.Commands;
using servicedesk.Common.Events;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Repositories;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Dal;
using servicedesk.Services.Tickets.Domain;
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

                builder.RegisterType<BaseRepository<TicketDbContext>>().As<IBaseRepository>();
                builder.RegisterType<BaseService>().As<IBaseService>();
                builder.RegisterType<BaseDependentlyService>().As<IBaseDependentlyService>();
                
                builder.RegisterType<TicketService>().As<ITicketService>();

                /*
                builder.RegisterType<BaseRepository<Address, TicketDbContext>>().As<IBaseRepository<Address>>();
                builder.RegisterType<BaseRepository<BusinessUnit, TicketDbContext>>().As<IBaseRepository<BusinessUnit>>();
                builder.RegisterType<BaseRepository<Contract, TicketDbContext>>().As<IBaseRepository<Contract>>();
                builder.RegisterType<BaseRepository<Customer, TicketDbContext>>().As<IBaseRepository<Customer>>();
                builder.RegisterType<BaseRepository<Service, TicketDbContext>>().As<IBaseRepository<Service>>();
                builder.RegisterType<BaseRepository<Supplier, TicketDbContext>>().As<IBaseRepository<Supplier>>();
                builder.RegisterType<BaseRepository<TicketPriority, TicketDbContext>>().As<IBaseRepository<TicketPriority>>();
                builder.RegisterType<BaseRepository<TicketStatus, TicketDbContext>>().As<IBaseRepository<TicketStatus>>();
                builder.RegisterType<BaseRepository<User, TicketDbContext>>().As<IBaseRepository<User>>();
                builder.RegisterType<BaseRepository<Work, TicketDbContext>>().As<IBaseRepository<Work>>();
                builder.RegisterType<BaseRepository<WorkStatus, TicketDbContext>>().As<IBaseRepository<WorkStatus>>();

                builder.RegisterType<BaseDependentlyService<Address>>().As<IBaseDependentlyService<Address>>();
                builder.RegisterType<BaseDependentlyService<BusinessUnit>>().As<IBaseDependentlyService<BusinessUnit>>();
                builder.RegisterType<BaseDependentlyService<Contract>>().As<IBaseDependentlyService<Contract>>();
                builder.RegisterType<BaseService<Customer>>().As<IBaseService<Customer>>();
                builder.RegisterType<BaseDependentlyService<Service>>().As<IBaseDependentlyService<Service>>();
                builder.RegisterType<BaseService<Supplier>>().As<IBaseService<Supplier>>();
                builder.RegisterType<BaseService<TicketPriority>>().As<IBaseService<TicketPriority>>();
                builder.RegisterType<BaseService<TicketStatus>>().As<IBaseService<TicketStatus>>();
                builder.RegisterType<BaseDependentlyService<User>>().As<IBaseDependentlyService<User>>();
                builder.RegisterType<BaseDependentlyService<Work>>().As<IBaseDependentlyService<Work>>();
                builder.RegisterType<BaseService<WorkStatus>>().As<IBaseService<WorkStatus>>();
                */

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

                //builder.RegisterInstance(_configuration.GetSettings<ExceptionlessSettings>()).SingleInstance();
                //builder.RegisterType<ExceptionlessExceptionHandler>().As<IExceptionHandler>().SingleInstance();

                //builder.RegisterGeneric(typeof(BaseRepository<,>)).As(typeof(IBaseRepository<>));
                //builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IBaseRepository<>)).AsImplementedInterfaces();
                //builder
                //    .RegisterAssemblyTypes(assembly)
                //    .Where(t => t.GetTypeInfo().ImplementedInterfaces.Any(i => i.IsGenericParameter && i.GetGenericTypeDefinition() == typeof(IBaseRepository<>)))
                //    .AsImplementedInterfaces()
                //    .InstancePerLifetimeScope();
                //var i = assembly.GetTypes().Where(r => r.IsClosedTypeOf(typeof(IBaseRepository<>))).ToList();
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
}