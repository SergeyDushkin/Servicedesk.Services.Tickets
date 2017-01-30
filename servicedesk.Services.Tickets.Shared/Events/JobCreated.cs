using System;
using servicedesk.Common.Events;

namespace servicedesk.Services.Tickets.Shared.Events
{
    public class JobCreated : IAuthenticatedEvent
    {
        public Guid RequestId { get; set; }
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string Description { get; set; }

        public JobCreated() 
        {
        }
    }
}
