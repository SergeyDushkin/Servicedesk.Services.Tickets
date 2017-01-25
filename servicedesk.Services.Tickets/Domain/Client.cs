using servicedesk.Common.Domain;

namespace serviceDesk.Services.Tickets.Domain
{
    public class Client : IdentifiableEntity
    {
        public string Name { get; set; }

        protected Client()
        {
        }
    }
}