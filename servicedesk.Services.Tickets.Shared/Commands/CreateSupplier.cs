using System;
using servicedesk.Common.Commands;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class CreateSupplier
    {
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Name { get; set; }
    }
}
