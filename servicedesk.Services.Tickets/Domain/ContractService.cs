using System;
using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Domain
{
    public class ContractService : IdentifiableEntity, ITimestampable
    {
        public Guid ContractId { get; set; }
        public Guid ServiceId { get; set; }
        
        public Contract Contract { get; set; }
        public Service Service { get; set; }

        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ContractService() { }
        public ContractService(Guid id)
        {
            this.Id = id;
        }

        public void SetId(Guid id) 
        {
            this.Id = id;
        }
    }

    public class ContractServiceDto
    {
        public Guid Id { get; set; }
        public Guid ContractId { get; set; }
        public Guid ServiceId { get; set; }

        public Contract Contract { get; set; }
        public Service Service { get; set; }
    }

    public class CreateContractService
    {
        public Guid ContractId { get; set; }
        public Guid ServiceId { get; set; }
    }

    public class UpdateContractService
    {
        public Guid Id { get; set; }
        public Guid ContractId { get; set; }
        public Guid ServiceId { get; set; }
    }
}