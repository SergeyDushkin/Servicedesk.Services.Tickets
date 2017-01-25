using servicedesk.Common.Commands;
using servicedesk.Common.Host;
using serviceDesk.Services.Tickets.Framework;

namespace serviceDesk.Services.Tickets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10020)
                .UseAutofac(Bootstrapper.LifeTimeScope)
                .UseRabbitMq()
                .SubscribeToCommand<SetStatus>(exchangeName: "serviceDesk.Services.Tickets.commands", routingKey : "ticket.create")
                .Build()
                .Run();
        }
    }
}
