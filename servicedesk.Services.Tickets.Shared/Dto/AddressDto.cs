using System;

namespace serviceDesk.Services.Tickets.Shared.Dto
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ContactDto Contact { get; set; }
    }
}