using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private readonly IServiceProvider services;
        private readonly IHandler handler;
        private readonly ILogger logger;
        private readonly IBusClient bus;

        public CreateTicketHandler(IHandler handler, IBusClient bus, ILogger<CreateTicketHandler> logger, IServiceProvider services)
        {
            this.handler = handler;
            this.bus = bus;
            this.logger = logger;
            this.services = services;
        }

        public async Task HandleAsync(CreateTicket command)
        {
            await handler
                .Run(async () => 
                {
                    var ticketService = this.services.GetService<ITicketService>();
                    var id = await ticketService.CreateAsync(command);
                    var @event = new TicketCreated(command.Request.Id, command.UserId, id);

                    logger.LogDebug("New ticket created successfully");
                    logger.LogDebug($"Publish event ticket_created: {Newtonsoft.Json.JsonConvert.SerializeObject(@event)}");
                    
                    await bus.PublishAsync(@event, command.Request.Id, cfg => 
                        cfg.WithExchange(e => 
                            e.WithName("servicedesk.Services.Tickets")).WithRoutingKey("ticket.created"));
                })
                //.OnSuccess(async (logger) => 
                //{
                //    var @event = new TicketCreated(command.Request.Id, command.UserId, command);
                //    logger.Debug("New ticket created successfully");
                //    logger.Debug($"Publish event ticket_created: {Newtonsoft.Json.JsonConvert.SerializeObject(@event)}");
                //    await bus.PublishAsync(@event, command.Request.Id, 
                //        cfg => cfg.WithExchange(e => e.WithName("servicedesk.Services.Tickets")).WithRoutingKey("ticket.created"));
                //})
                .OnError(async (ex, logger) => 
                {
                    logger.Error(ex, "Error when trying to create new ticket: " + ex.GetBaseException().Message);
                    await bus.PublishAsync(
                        new CreateTicketRejected(command.Request.Id, command.UserId, "error", "Error when trying to create new ticket: " + ex.GetBaseException().Message), 
                        command.Request.Id, 
                        cfg => cfg.WithExchange(e => e.WithName("servicedesk.Services.Tickets")).WithRoutingKey("ticket.rejected"));
                })
                .ExecuteAsync();
        }
    }
}

            /*
            try
            {
                await bus.RespondAsync<CreateTicket, TicketCreated>(async (msg, context) => {
                    var createId = await ticketService.CreateAsync(msg);
                    return new TicketCreated(context.GlobalRequestId, createId);
                });
                
                logger.LogDebug("New ticket created successfully");
                logger.LogDebug($"Publish event ticket_created: {Newtonsoft.Json.JsonConvert.SerializeObject(command)}");
                
                await bus.PublishAsync(@event, command.Request.Id,
                    cfg => cfg.WithExchange(e => e.WithName("servicedesk.Services.Tickets")).WithRoutingKey("ticket.created"));
            }
            catch(Exception ex)
            {
                logger.LogError(new EventId(10010, "create ticket error"), ex, "Error when trying to create new ticket: " + ex.GetBaseException().Message);
                await bus.PublishAsync(
                    new CreateTicketRejected(command.Request.Id, command.UserId, "error", "Error when trying to create new ticket: " + ex.GetBaseException().Message),
                    command.Request.Id, 
                    cfg => cfg.WithExchange(e => e.WithName("servicedesk.Services.Tickets")).WithRoutingKey("ticket.rejected"));
            }*/