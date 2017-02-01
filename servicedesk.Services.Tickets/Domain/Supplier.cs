using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class Supplier : IdentifiableEntity
    {
        public string Name { get; set; }
    }
}