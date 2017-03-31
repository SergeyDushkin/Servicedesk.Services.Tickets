using System;
using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class Channel : IdentifiableEntity, IDependently
    {
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Channel() { }
        public Channel(Guid id)
        {
            this.Id = id;
        }

        public void SetId(Guid id) 
        {
            this.Id = id;
        }
    }

    
    public class ChannelDto
    {
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
    }

    public class CreateChannel
    {
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid? Id { get; set; }
    }

    public class UpdateChannel
    {
        public string Resource { get; set; }
        public Guid ReferenceId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}