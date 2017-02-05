using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Shared.Commands;

namespace servicedesk.Services.Tickets.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAsync();
        Task<Ticket> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(CreateTicket create);
    }
}