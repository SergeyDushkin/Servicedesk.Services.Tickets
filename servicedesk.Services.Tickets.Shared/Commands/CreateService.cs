using System;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class CreateService
    {
        public System.Guid? Id { get; set; }
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
