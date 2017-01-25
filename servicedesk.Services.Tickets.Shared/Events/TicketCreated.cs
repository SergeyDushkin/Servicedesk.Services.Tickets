using System;
using servicedesk.Common.Events;

namespace serviceDesk.Services.Tickets.Shared.Commands
{
    public class TicketCreated : IAuthenticatedEvent
    {
        public Guid RequestId { get; }
        public string UserId { get; }
        public Guid ClientId { get; }
        public Guid AddressId { get; }
        public DateTimeOffset RequestDate { get; }
        public string Description { get; }

        public TicketCreated(Guid requestId, string userId, Guid clientId, Guid addressId, DateTimeOffset requestDate, string description) 
        {
            RequestId = requestId;
            UserId = userId;
            ClientId = clientId;
            AddressId = addressId;
            RequestDate = RequestDate;
            Description = description;
        }
    }
}
