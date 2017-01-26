using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class User : IdentifiableEntity
    {
        public string Name { get; set; }

        protected User()
        {
        }
    }
}