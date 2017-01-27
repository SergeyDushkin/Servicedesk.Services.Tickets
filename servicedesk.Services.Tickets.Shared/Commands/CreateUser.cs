using System;
using servicedesk.Common.Commands;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class CreateUser : IAuthenticatedCommand
    {
        public Request Request { get; set; }
        public Guid ReferenceId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
