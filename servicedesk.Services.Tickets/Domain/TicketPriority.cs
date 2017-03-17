using servicedesk.Common.Domain;
using System;

namespace servicedesk.Services.Tickets.Domain
{
    public class TicketPriority : IdentifiableEntity
    {
        public string Name { get; set; }

        public TicketPriority() { }
        public TicketPriority(Guid id)
        {
            this.Id = id;
        }

        public void SetId(Guid id) 
        {
            this.Id = id;
        }
    }
}