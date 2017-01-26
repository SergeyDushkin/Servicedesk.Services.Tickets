using System;
using servicedesk.Common.Events;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class CreateTicketRejected  : IRejectedEvent
    {
        public Guid RequestId { get; }
        public string UserId { get; }
        public string Code { get; }
        public string Reason { get; }

        public CreateTicketRejected(Guid requestId, string userId, string code, string reason) 
        {
            RequestId = requestId;
            UserId = userId;
            Code = code;
            Reason = reason;
        }
    }
}
