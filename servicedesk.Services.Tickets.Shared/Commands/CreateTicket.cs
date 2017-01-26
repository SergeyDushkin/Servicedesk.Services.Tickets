using System;
using servicedesk.Common.Commands;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class CreateTicket : IAuthenticatedCommand
    {
        public Request Request { get; set; }
        public string UserId { get; set; }
        public Guid ClientId { get; set; }
        public Guid AddressId { get; set; }
        public DateTimeOffset RequestDate { get; set; }
        public string Description { get; set; }
    }
}
