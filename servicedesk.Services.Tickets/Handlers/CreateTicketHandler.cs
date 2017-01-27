using System.Threading.Tasks;
using RawRabbit;
using servicedesk.Common.Commands;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Commands;
using servicedesk.Services.Tickets.Shared.Events;

namespace servicedesk.Services.Tickets.Handlers
{
    public class CreateTicketHandler : ICommandHandler<CreateTicket>
    {
        private readonly IHandler _handler;
        private readonly IBusClient _bus;
        private readonly ITicketService _ticketService;

        public CreateTicketHandler(IHandler handler, IBusClient bus, ITicketService ticketService)
        {
            _handler = handler;
            _bus = bus;
            _ticketService = ticketService;
        }

        public async Task HandleAsync(CreateTicket command)
        {
            await _handler
                .Run(async () => await _ticketService.CreateAsync(command.UserId, command.ClientId, command.AddressId, command.RequestDate, command.Description))
                .OnSuccess(async () => await _bus.PublishAsync(
                    new TicketCreated(command.Request.Id, command),
                    command.Request.Id, 
                    cfg => cfg.WithExchange(e => e.WithName("servicedesk.Services.Tickets")).WithRoutingKey("ticket.created")))
                .OnError(async (ex, logger) => 
                {
                    logger.Error(ex, "Error when trying to create new ticket: " + ex.GetBaseException().Message);
                    await _bus.PublishAsync(
                        new CreateTicketRejected(command.Request.Id, command.UserId, "error", "Error when trying to create new ticket: " + ex.GetBaseException().Message), 
                        command.Request.Id, 
                        cfg => cfg.WithExchange(e => e.WithName("servicedesk.Services.Tickets")).WithRoutingKey("ticket.rejected"));
                })
                .ExecuteAsync();
        }
    }
}