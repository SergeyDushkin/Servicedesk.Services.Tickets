using System;

namespace servicedesk.Services.Tickets.Shared.Dto
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public Guid ReferenceId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}