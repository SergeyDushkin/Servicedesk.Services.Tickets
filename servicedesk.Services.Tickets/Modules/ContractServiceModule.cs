using AutoMapper;
using Collectively.Common.Extensions;
using Nancy;
using System.Linq;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Shared.Commands;
using servicedesk.Services.Tickets.Shared.Dto;
using System;

namespace servicedesk.Services.Tickets.Modules
{
    public class AppendServiceToContract
    {
        public Guid ContractId { get; set; }
        public Guid ServiceId { get; set; }
    }

    public class ContractServiceModule : ModuleBase
    {
        public ContractServiceModule(IBaseService service, IMapper mapper) : base(mapper, "contracts")
        {
            Get("{referenceId:guid}/service", args => FetchCollection<GetByReferenceId, Service>
                (async query => (await service.GetAsync<ContractService>(r => r.ContractId == query.ReferenceId, c => c.Service)).Select(r => r.Service).PaginateWithoutLimit())
                .MapTo<ServiceDto>()
                .HandleAsync());

            Get("{referenceId:guid}/service/{id:guid}", args => Fetch<GetById, Service>
                (async x => (await service.GetByIdAsync<ContractService>(x.Id, c => c.Service)).Service)
                .MapTo<ServiceDto>()
                .HandleAsync());

            Post("{referenceId:guid}/service", async args =>
            {
                var @input = BindRequest<AppendServiceToContract>();
                var @create = mapper.Map<ContractService>(@input);
                
                await service.CreateAsync(@create);

                return HttpStatusCode.Created;
            });

            Delete("{id:guid}", async args =>
            {
                var @input = BindRequest<GetById>();
                var @delete = await service.GetByIdAsync<Contract>(@input.Id);

                await service.DeleteAsync(@delete);

                return HttpStatusCode.OK;
            });
        }
    }
}