using System;
using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class Address : IdentifiableEntity, IDependently
    {
        public Guid ReferenceId { get; set; }
        public string Name { get; set; }
        public string FullAddress { get; set; }
    }
}