using System;
using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class UnitUser : IdentifiableEntity, ITimestampable
    {
        public Guid UnitId { get; set; }
        public Guid UserId { get; set; }

        public BusinessUnit Unit { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public UnitUser() { }
        public UnitUser(Guid id)
        {
            this.Id = id;
        }

        public UnitUser SetId(Guid id)
        {
            this.Id = id;

            return this;
        }
    }

    public class UnitUserDto
    {
        public Guid Id { get; set; }
        public Guid UnitId { get; set; }
        public Guid UserId { get; set; }

        public BusinessUnit Unit { get; set; }
        public User User { get; set; }
    }

    public class CreateUnitUser
    {
        public Guid UnitId { get; set; }
        public Guid UserId { get; set; }
    }

    public class UpdateUnitUser
    {
        public Guid Id { get; set; }
        public Guid UnitId { get; set; }
        public Guid UserId { get; set; }
    }
}