using AutoMapper;
using Coolector.Common.Extensions;
using Nancy;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Commands;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Modules
{
    public class ContractModule : ModuleBase
    {
        public ContractModule(IBaseDependentlyService service, IMapper mapper) : base(mapper, "contracts")
        {
            Get("", args => FetchCollection<GetByReferenceId, Contract>
                (async x => (await service.GetByReferenceIdAsync<Contract>(x.ReferenceId, c => c.Client)).PaginateWithoutLimit())
                .MapTo<ContractDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, Contract>
                (async x => await service.GetByIdAsync<Contract>(x.Id, c => c.Client))
                .MapTo<ContractDto>()
                .HandleAsync());

            Post("", async args =>
            {
                var @input = BindRequest<CreateContract>();
                var @create = mapper.Map<Contract>(@input);

                await service.CreateAsync(@create);

                return HttpStatusCode.Created;
            });

            Put("{id:guid}", async args =>
            {
                var @input = BindRequest<UpdateContract>();
                var @update = mapper.Map<Contract>(@input);

                await service.UpdateAsync(@update);

                return HttpStatusCode.OK;
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