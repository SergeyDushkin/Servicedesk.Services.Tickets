using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class TicketStatus : IdentifiableEntity
    {
        public string Name { get; set; }
    }
}