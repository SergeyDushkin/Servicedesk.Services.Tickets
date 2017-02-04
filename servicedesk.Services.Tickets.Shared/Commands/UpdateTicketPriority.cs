using System;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class UpdateTicketPriority
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
