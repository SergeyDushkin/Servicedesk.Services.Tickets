using System;

namespace servicedesk.Services.Tickets.Shared.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string GenderCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }

        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}