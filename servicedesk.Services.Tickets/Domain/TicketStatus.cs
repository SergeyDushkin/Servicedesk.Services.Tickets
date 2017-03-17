using servicedesk.Common.Domain;
using System;

namespace servicedesk.Services.Tickets.Domain
{
    public class TicketStatus : IdentifiableEntity
    {
        public string Name { get; set; }

        public TicketStatus() { }
        public TicketStatus(Guid id)
        {
            this.Id = id;
        }

        public void SetId(Guid id) 
        {
            this.Id = id;
        }
    }
}