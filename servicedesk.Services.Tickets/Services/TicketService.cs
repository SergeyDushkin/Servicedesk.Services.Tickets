using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Repositories;
using servicedesk.Services.Tickets.Shared.Commands;

namespace servicedesk.Services.Tickets.Services
{
    public class TicketService : ITicketService
    {
        private readonly IBaseRepository repository;

        public TicketService(IBaseRepository repository)
        {
            this.repository = repository;
        }

        public Task<Guid> CreateAsync(CreateTicket create)
        {
            var ticket = new Ticket
            {
                ClientId = create.ClientId,
                AddressId = create.AddressId,
                ContractId = create.ContractId,
                Description = create.Description,
                BusinessUnitId = create.BusinessUnitId,
                OperatorId = create.OperatorId,
                PriorityId = create.PriorityId,
                ServiceId = create.ServiceId,
                StartDate = create.StartDate,
                StatusId = create.StatusId,
                UserId = create.UserId
            };

            repository.Add(ticket);
            repository.CommitAsync();

            return Task.FromResult(ticket.Id);
        }

        public Task<IEnumerable<Ticket>> GetAsync() => repository.AllIncludingAsync<Ticket>(f => f.Address, 
            f => f.Client,
            f => f.Contract,
            f => f.BusinessUnit,
            f => f.Operator,
            f => f.Priority,
            f => f.Service,
            f => f.Status);

        public Task<Ticket> GetByIdAsync(Guid id) => repository.GetSingleAsync<Ticket>(r => r.Id == id, 
            f => f.Address, 
            f => f.Client,
            f => f.Contract,
            f => f.BusinessUnit,
            f => f.Operator,
            f => f.Priority,
            f => f.Service,
            f => f.Status);
    }
}