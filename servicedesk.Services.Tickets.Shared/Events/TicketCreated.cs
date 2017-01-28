using System;
using servicedesk.Common.Events;
using servicedesk.Services.Tickets.Shared.Commands;

namespace servicedesk.Services.Tickets.Shared.Events
{
    public class TicketCreated : IAuthenticatedEvent
    {
        public Guid RequestId { get; }
        public string UserId { get; }
        public Guid ClientId { get; }
        public Guid AddressId { get; }
        public DateTimeOffset RequestDate { get; }
        public string Description { get; }

        public TicketCreated() 
        {
        }

        public TicketCreated(Guid requestId, string userId, Guid clientId, Guid addressId, DateTimeOffset requestDate, string description) 
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
