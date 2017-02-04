using System;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class UpdateAddress
    {
        public Guid Id { get; set; }
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
