using System;
using servicedesk.Common.Events;

namespace servicedesk.Services.Tickets.Shared.Events
{
    public class Rejected  : IRejectedEvent
    {
        public Guid RequestId { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public string Reason { get; set; }

        public Rejected() 
        {
        }
        
        public Rejected(Guid requestId, string userId, string code, string reason) 
        {
            RequestId = requestId;
            UserId = userId;
            Code = code;
            Reason = reason;
        }
    }
}
