using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class Address : IdentifiableEntity
    {
        public string Name { get; set; }
        public string FullAddress { get; set; }
    }
}