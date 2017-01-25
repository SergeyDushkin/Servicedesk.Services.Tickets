using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using serviceDesk.Services.Tickets.Domain;
using serviceDesk.Services.Tickets.Repositories;

namespace serviceDesk.Services.Tickets.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public Task CreateAsync(Guid userId, Guid clientId, Guid addressId, DateTimeOffset requestDate, string description)
        {
            var ticket = new Ticket
            {
                ClientId = clientId,
                AddressId = addressId,
                UserId = userId,
                Description = description,
                RequestDate = requestDate,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            _ticketRepository.Add(ticket);

            return _ticketRepository.CommitAsync();
        }

        public Task<IEnumerable<Ticket>> GetAsync() => _ticketRepository.GetAllAsync();

        public Task<Ticket> GetByIdAsync(Guid id) => _ticketRepository.GetSingleAsync(id);
    }
}