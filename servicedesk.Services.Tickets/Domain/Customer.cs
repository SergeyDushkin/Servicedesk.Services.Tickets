using System;
using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class Customer : IdentifiableEntity
    {
        public string Name { get; set; }

        public Customer() {}
        public Customer(Guid id) 
        {
            this.Id = id;
        }
    }
}