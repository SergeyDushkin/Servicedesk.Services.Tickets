using System;
using servicedesk.Common.Commands;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class CreateJob : IAuthenticatedCommand
    {
        public Request Request { get; set; }
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string UserId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string Description { get; set; }
    }
}
