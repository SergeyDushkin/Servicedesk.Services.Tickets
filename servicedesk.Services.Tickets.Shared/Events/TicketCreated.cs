using System;
using servicedesk.Common.Events;

namespace servicedesk.Services.Tickets.Shared.Events
{
    public class TicketCreated : IAuthenticatedEvent
    {
        public Guid RequestId { get; set; }
        public string UserId { get; set; }
        public Guid Id { get; set; }

        public TicketCreated() 
        {
        }

        public TicketCreated(Guid requestId, string userId, Guid id) 
        {
            RequestId = requestId;
            UserId = userId;
            Id = id;
        }
    }
}
