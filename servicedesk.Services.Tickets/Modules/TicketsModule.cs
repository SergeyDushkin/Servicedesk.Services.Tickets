using AutoMapper;
using Coolector.Common.Extensions;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Modules
{
    public class TicketsModule : ModuleBase
    {
        public TicketsModule(ITicketService ticketService, IMapper mapper) 
            : base(mapper, "tickets")
        {
            Get("", args => FetchCollection<BrowseTickets, Ticket>
                (async x => (await ticketService.GetAsync()).PaginateWithoutLimit())
                .MapTo<TicketDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetTicket, Ticket>
                (async x => await ticketService.GetByIdAsync(x.Id))
                .MapTo<TicketDto>()
                .HandleAsync());
        }
    }
}