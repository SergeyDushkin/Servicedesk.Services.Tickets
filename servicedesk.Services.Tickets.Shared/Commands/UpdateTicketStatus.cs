using System;
using servicedesk.Common.Commands;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class UpdateTicketStatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
