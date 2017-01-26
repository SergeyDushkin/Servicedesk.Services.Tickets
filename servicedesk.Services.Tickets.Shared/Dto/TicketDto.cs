using System;

namespace servicedesk.Services.Tickets.Shared.Dto
{
    public class TicketDto
    {
        public string TicketNumber { get; set; }
        public Guid ClientId { get; set; }
        public Guid AddressId { get; set; }
        public Guid UserId { get; set; }
        
        public string Description { get; set; }
        public DateTimeOffset RequestDate { get; set; }
        public DateTimeOffset? CompleteDate { get; set; }

        public ClientDto Client { get; set; }
        public AddressDto Address { get; set; }
        public UserDto User { get; set; }

        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
    }
}