using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Repositories;

namespace servicedesk.Services.Tickets.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public Task CreateAsync(string userId, Guid clientId, Guid addressId, DateTimeOffset requestDate, string description)
        {
            var ticket = new Ticket
            {
                Client = new Customer(clientId),
                Address = new Address(addressId),
                UserId = userId,
                Description = description,
                StartDate = requestDate,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            _ticketRepository.Add(ticket);

            return _ticketRepository.CommitAsync();
        }

        public Task<IEnumerable<Ticket>> GetAsync() => _ticketRepository.AllIncludingAsync(r => r.Address, f => f.Client);

        public Task<Ticket> GetByIdAsync(Guid id) => _ticketRepository.GetSingleAsync(r => r.Id == id, f1 => f1.Address, f2 => f2.Client);
    }
}