using System;
using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class User : IdentifiableEntity, IDependently
    {
        public Guid ReferenceId { get; set; }
        public string Name { get; set; }
    }
}