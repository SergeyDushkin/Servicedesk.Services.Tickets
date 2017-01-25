using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using serviceDesk.Services.Tickets.Domain;

namespace serviceDesk.Services.Tickets.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAsync();
        Task<Ticket> GetByIdAsync(Guid id);
        Task CreateAsync(Guid userId, Guid clientId, Guid addressId, DateTimeOffset requestDate, string description);
    }
}