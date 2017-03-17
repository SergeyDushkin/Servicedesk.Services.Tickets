using System;
using servicedesk.Common.Commands;

namespace servicedesk.Services.Tickets.Shared.Commands
{
    public class CreateTicket : IAuthenticatedCommand
    {
        public System.Guid? Id { get; set; }
        public Request Request { get; set; }
        public string UserId { get; set; }
        public Guid ClientId { get; set; }
        public Guid AddressId { get; set; }
        public Guid? ApplicantId { get; set; }
        public Guid? ContractId { get; set; }
        public Guid? PriorityId { get; set; }
        public Guid? StatusId { get; set; }
        public Guid? ServiceId { get; set; }
        public Guid? OperatorId { get; set; }
        public Guid? BusinessUnitId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public string Description { get; set; }
    }
}
