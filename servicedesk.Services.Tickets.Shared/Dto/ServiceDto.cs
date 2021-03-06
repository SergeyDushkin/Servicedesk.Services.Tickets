using System;

namespace servicedesk.Services.Tickets.Shared.Dto
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}