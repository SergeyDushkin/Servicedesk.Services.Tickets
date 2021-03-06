using System;
using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class Service : IdentifiableEntity, IDependently
    {
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public Service() { }
        public Service(Guid id)
        {
            this.Id = id;
        }

        public void SetId(Guid id) 
        {
            this.Id = id;
        }
    }
}