using System.Threading.Tasks;
using RawRabbit;
using servicedesk.Common.Commands;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Commands;

namespace servicedesk.Services.Tickets.Handlers
{
    public class CreateAddressHandler : ICommandHandler<CreateAddress>
    {
        private readonly IHandler handler;
        private readonly IBusClient bus;
        private readonly IBaseDependentlyService<Address> service;

        public CreateAddressHandler(IHandler handler, IBusClient bus, IBaseDependentlyService<Address> service)
        {
            this.handler = handler;
            this.bus = bus;
            this.service = service;
        }

        public async Task HandleAsync(CreateAddress command)
        {
            var address = new Address {
                ReferenceId = command.ReferenceId,
                Name = command.Name,
                FullAddress = command.Address
            };

            await handler
                .Run(async () => await service.CreateAsync(address))
                .OnSuccess((logger) => logger.Info("New address created successfully"))
                .OnError((ex, logger) => logger.Error(ex, "Error when trying to create new address: " + ex.GetBaseException().Message))
                .ExecuteAsync();
        }
    }
}