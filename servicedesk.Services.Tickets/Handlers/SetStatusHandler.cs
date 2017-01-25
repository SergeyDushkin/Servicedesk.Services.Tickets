using System.Threading.Tasks;
using Coolector.Common.Services;
using RawRabbit;
using servicedesk.Common.Commands;
using servicedesk.Common.Events;
using serviceDesk.Services.Tickets.Services;

namespace serviceDesk.Services.Tickets.Handlers
{
    public class SetStatusHandler : ICommandHandler<CreateTicket>
    {
        private readonly IHandler _handler;
        private readonly IBusClient _bus;
        private readonly ITicketService _ticketService;

        public SetStatusHandler(IHandler handler, 
            IBusClient bus, 
            ITicketService ticketService)
        {
            _handler = handler;
            _bus = bus;
            _ticketService = ticketService;
        }

        public async Task HandleAsync(CreateTicket command)
        {
            await _handler
                .Run(async () => await _ticketService.CreateAsync(command))
                .OnSuccess(async () => await _bus.PublishAsync(
                    new NextStatusSet(command.Request.Id, command.SourceId, command.ReferenceId, command.StatusId), 
                    command.Request.Id, 
                    cfg => cfg.WithExchange(e => e.WithName("serviceDesk.Services.Tickets.events")).WithRoutingKey("nextstatusset")))
                .OnCustomError(async (ex, logger) => await _bus.PublishAsync(
                    new SetNewStatusRejected(command.Request.Id, "error", "Error when trying to set new status."), 
                    command.Request.Id, 
                    cfg => cfg.WithExchange(e => e.WithName("serviceDesk.Services.Tickets.events")).WithRoutingKey("setnewstatusrejected")))
                .OnError(async (ex, logger) => 
                {
                    logger.Error(ex, "Error when trying to set new status.");
                    await _bus.PublishAsync(
                        new SetNewStatusRejected(command.Request.Id, "error", "Error when trying to set new status."), 
                        command.Request.Id, 
                        cfg => cfg.WithExchange(e => e.WithName("serviceDesk.Services.Tickets.events")).WithRoutingKey("setnewstatusrejected"));
                })
                .ExecuteAsync();
        }
    }
}