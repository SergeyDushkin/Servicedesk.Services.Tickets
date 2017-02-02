using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using Serilog;
using servicedesk.Services.Tickets.Dal;
using servicedesk.Services.Tickets.Framework;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace servicedesk.Services.Tickets
{
    public class Startup
    {
        public string EnvironmentName {get;set;}
        public IConfiguration Configuration { get; set; }
        public IContainer ApplicationContainer { get; set; }

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

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TicketDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("TicketDatabase")));

            ApplicationContainer = GetServiceContainer(services);

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var serilogLogger = new LoggerConfiguration()
                .Enrich.WithProperty("Application","ServiceDesk.Services.Tickets")
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            loggerFactory.AddSerilog(serilogLogger);
            loggerFactory.AddConsole();

            var logger = loggerFactory.CreateLogger("Bootstrapper");

            app.UseOwin().UseNancy(x => x.Bootstrapper = new Bootstrapper(ApplicationContainer, Configuration, logger));
        }

        protected static IContainer GetServiceContainer(IEnumerable<ServiceDescriptor> services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            return builder.Build();
        }
    }
}
