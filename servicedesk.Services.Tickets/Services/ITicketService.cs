using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using servicedesk.Services.Tickets.Domain;

namespace servicedesk.Services.Tickets.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAsync();
        Task<Ticket> GetByIdAsync(Guid id);
        Task CreateAsync(string userId, Guid clientId, Guid addressId, DateTimeOffset requestDate, string description);
    }
}