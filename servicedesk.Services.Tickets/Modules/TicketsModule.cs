using System.Threading.Tasks;
using AutoMapper;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Extensions;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Modules
{
    public class TicketsModule : ModuleBase
    {
        public TicketsModule(ITicketService ticketService, IBaseService service, IMapper mapper) 
            : base(mapper, "tickets")
        {
            Get("", args => FetchCollection<BrowseTickets, Ticket>
                (async query => await Task.FromResult(service.Query<Ticket>(x => true, 
                            f => f.Address, 
                            f => f.Applicant,
                            f => f.Client,
                            f => f.Contract,
                            f => f.BusinessUnit,
                            f => f.Operator,
                            f => f.Priority,
                            f => f.Service,
                            f => f.Status).Paginate(query)))
                .MapTo<TicketDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetTicket, Ticket>
                (async x => await ticketService.GetByIdAsync(x.Id))
                .MapTo<TicketDto>()
                .HandleAsync());
        }
    }
}