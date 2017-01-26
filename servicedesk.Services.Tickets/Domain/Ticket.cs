using System;
using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class Ticket : IdentifiableEntity, ITimestampable
    {
        public string TicketNumber { get; set; }
        public Guid ClientId { get; set; }
        public Guid AddressId { get; set; }
        public Guid UserId { get; set; }
        
        public string Description { get; set; }
        public DateTimeOffset RequestDate { get; set; }
        public DateTimeOffset? CompleteDate { get; set; }

        public Client Client { get; set; }
        public Address Address { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}