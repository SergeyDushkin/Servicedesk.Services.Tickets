using System;
using System.Collections.Generic;

namespace servicedesk.Services.Tickets.Shared.Dto
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string TicketNumber { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public DateTimeOffset? Deadline { get; set; }

        public CustomerDto Client { get; set; }
        public AddressDto Address { get; set; }
        public ContractDto Contract { get; set; }
        public TicketPriorityDto Priority { get; set; }
        public TicketStatusDto Status { get; set; }
        public ServiceDto Service { get; set; }
        public UserDto Operator { get; set; }
        public UserDto Applicant { get; set; }
        public BusinessUnitDto BusinessUnit { get; set; }
        public List<WorkDto> Works { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}