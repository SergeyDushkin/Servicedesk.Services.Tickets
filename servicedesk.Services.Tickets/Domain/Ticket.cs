using System;
using System.Collections.Generic;
using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class Ticket : IdentifiableEntity, ITimestampable
    {
        public string TicketNumber { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public DateTimeOffset? Deadline { get; set; }

        public Customer Client { get; set; }
        public Address Address { get; set; }
        public Contract Contract { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; }
        public Service Service { get; set; }
        public User Operator { get; set; }
        public BusinessUnit BusinessUnit { get; set; }
        public List<Work> Works { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}


        //public Guid ClientId { get; set; }
        //public Guid AddressId { get; set; }