using System;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class UpdateWorkStatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
