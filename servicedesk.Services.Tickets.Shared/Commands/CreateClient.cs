using servicedesk.Common.Commands;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class CreateClient : IAuthenticatedCommand
    {
        public Request Request { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
