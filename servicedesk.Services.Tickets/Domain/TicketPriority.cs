using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class TicketPriority : IdentifiableEntity
    {
        public string Name { get; set; }
    }
}