using System;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class CreateContract
    {
        public System.Guid? Id { get; set; }
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid ClientId { get; set; }
    }
}
