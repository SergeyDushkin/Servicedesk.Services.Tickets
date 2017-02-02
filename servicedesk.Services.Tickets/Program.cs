using servicedesk.Common.Host;
using servicedesk.Services.Tickets.Framework;
using servicedesk.Services.Tickets.Shared.Commands;

namespace servicedesk.Services.Tickets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10020)
                .UseAutofac(Bootstrapper.LifeTimeScope)
                .UseRabbitMq()
                .SubscribeToCommand<CreateTicket>(exchangeName: "servicedesk.Services.Tickets", routingKey : "ticket.create")
                .Build()
                .Run();
        }
    }
}
