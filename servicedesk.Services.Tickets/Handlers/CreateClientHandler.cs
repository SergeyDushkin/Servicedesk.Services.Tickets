using System.Threading.Tasks;
using Coolector.Common.Services;
using RawRabbit;
using servicedesk.Common.Commands;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Commands;

namespace servicedesk.Services.Tickets.Handlers
{
    public class CreateClientHandler : ICommandHandler<CreateClient>
    {
        private readonly IHandler handler;
        private readonly IBusClient bus;
        private readonly IBaseService<Client> service;

        public CreateClientHandler(IHandler handler, IBusClient bus, IBaseService<Client> service)
        {
            this.handler = handler;
            this.bus = bus;
            this.service = service;
        }

        public async Task HandleAsync(CreateClient command)
        {
            var client = new Client {
                Name = command.Name
            };

            await handler
                .Run(async () => await service.CreateAsync(client))
                //.OnSuccess((logger) => logger.Error(ex, "New address created successfully"))
                .OnError((ex, logger) => logger.Error(ex, "Error when trying to create new client: " + ex.GetBaseException().Message))
                .ExecuteAsync();
        }
    }
}