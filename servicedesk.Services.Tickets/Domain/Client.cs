using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class Client : IdentifiableEntity
    {
        public string Name { get; set; }
    }
}