using System;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class CreateAddress
    {
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
