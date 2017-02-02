using servicedesk.Common.Domain;
using System;

namespace servicedesk.Services.Tickets.Domain
{
    public class Supplier : IdentifiableEntity, IDependently
    {
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Name { get; set; }

        public Supplier() { }
        public Supplier(Guid id)
        {
            this.Id = id;
        }
    }
}