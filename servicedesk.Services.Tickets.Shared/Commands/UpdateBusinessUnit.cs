using System;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class UpdateBusinessUnit
    {
        public Guid Id { get; set; }
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Name { get; set; }
    }
}
