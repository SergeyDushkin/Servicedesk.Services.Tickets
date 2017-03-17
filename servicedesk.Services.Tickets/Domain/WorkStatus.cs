using servicedesk.Common.Domain;
using System;

namespace servicedesk.Services.Tickets.Domain
{
    public class WorkStatus : IdentifiableEntity
    {
        public string Name { get; set; }

        public WorkStatus() { }
        public WorkStatus(Guid id)
        {
            this.Id = id;
        }

        public void SetId(Guid id) 
        {
            this.Id = id;
        }
    }
}