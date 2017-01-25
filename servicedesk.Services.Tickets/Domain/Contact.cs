using servicedesk.Common.Domain;

namespace serviceDesk.Services.Tickets.Domain
{
    public class Contact : IdentifiableEntity
    {
        public string Address { get; set; }

        protected Contact()
        {
        }
    }
}