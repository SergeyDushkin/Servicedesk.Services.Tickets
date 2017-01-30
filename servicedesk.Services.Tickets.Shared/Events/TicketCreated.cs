using System;
using servicedesk.Common.Events;
using servicedesk.Services.Tickets.Shared.Commands;

namespace servicedesk.Services.Tickets.Shared.Events
{
    public class TicketCreated : IAuthenticatedEvent
    {
        public Guid RequestId { get; set; }
        public string UserId { get; set; }
        public Guid ClientId { get; set; }
        public Guid Id { get; set; }
        public Guid AddressId { get; set; }
        public DateTimeOffset RequestDate { get; set; }
        public string Description { get; set; }

        public TicketCreated() 
        {
        }

        public TicketCreated(Guid requestId, Guid id, string userId, Guid clientId, Guid addressId, DateTimeOffset requestDate, string description) 
        {
            RequestId = requestId;
            UserId = userId;
            ClientId = clientId;
            AddressId = addressId;
            RequestDate = requestDate;
            Description = description;
        }
        public TicketCreated(Guid requestId, CreateTicket ticket) 
        {
            RequestId = requestId;
            UserId = ticket.UserId;
            ClientId = ticket.ClientId;
            AddressId = ticket.AddressId;
            RequestDate = ticket.RequestDate;
            Description = ticket.Description;
        }
    }
}
