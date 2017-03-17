using System;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class CreateWork
    {
        public Guid? Id { get; set; }
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid WorkerId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string Description { get; set; }
        public Guid StatusId { get; set; }
    }
}
