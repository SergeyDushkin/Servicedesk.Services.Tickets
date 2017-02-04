using System;

namespace servicedesk.Services.Tickets.Shared.Dto
{
    public class CustomerDto
    {
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}