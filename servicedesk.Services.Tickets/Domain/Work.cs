using System;
using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class Work : IdentifiableEntity, ITimestampable, IDependently
    {
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        
        public Guid SupplierId { get; set; }
        public Guid WorkerId { get; set; }
        public Guid StatusId { get; set; }

        public Supplier Supplier { get; set; }
        public User Worker { get; set; }
        public WorkStatus Status { get; set; }

        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}