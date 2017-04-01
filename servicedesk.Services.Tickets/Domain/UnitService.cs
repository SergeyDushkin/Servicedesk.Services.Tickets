using System;
using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class UnitService : IdentifiableEntity, ITimestampable
    {
        public Guid UnitId { get; set; }
        public Guid ServiceId { get; set; }
        
        public BusinessUnit Unit { get; set; }
        public Service Service { get; set; }

        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public UnitService() { }
        public UnitService(Guid id)
        {
            this.Id = id;
        }

        public void SetId(Guid id) 
        {
            this.Id = id;
        }
    }

    public class UnitServiceDto
    {
        public Guid Id { get; set; }
        public Guid UnitId { get; set; }
        public Guid ServiceId { get; set; }

        public BusinessUnit Unit { get; set; }
        public Service Service { get; set; }
    }

    public class CreateUnitService
    {
        public Guid UnitId { get; set; }
        public Guid ServiceId { get; set; }
    }

    public class UpdateUnitService
    {
        public Guid Id { get; set; }
        public Guid UnitId { get; set; }
        public Guid ServiceId { get; set; }
    }
}