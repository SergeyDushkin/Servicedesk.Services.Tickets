using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Repositories;

namespace servicedesk.Services.Tickets.Services
{
    public class TicketService : ITicketService
    {
        private readonly IBaseRepository repository;

        public TicketService(IBaseRepository repository)
        {
            this.repository = repository;
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

            repository.Add(ticket);

            return repository.CommitAsync();
        }

        public Task<IEnumerable<Ticket>> GetAsync() => repository.AllIncludingAsync<Ticket>(r => r.Address, f => f.Client);

        public Task<Ticket> GetByIdAsync(Guid id) => repository.GetSingleAsync<Ticket>(r => r.Id == id, f1 => f1.Address, f2 => f2.Client);
    }
}