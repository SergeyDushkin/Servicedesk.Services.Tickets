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
    public class WorksModule : ModuleBase
    {
        public WorksModule(IBaseDependentlyService service, IMapper mapper) : base(mapper, "works")
        {
            Get("", args => FetchCollection<GetByReferenceId, Work>
                (async x => (await service.GetByReferenceIdAsync<Work>(x.ReferenceId, f => f.Status, f => f.Supplier, f => f.Worker)).PaginateWithoutLimit())
                .MapTo<WorkDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, Work>
                (async x => await service.GetByIdAsync<Work>(x.Id, f => f.Status, f => f.Supplier, f => f.Worker))
                .MapTo<WorkDto>()
                .HandleAsync());

            Post("", async args =>
            {
                var @input = BindRequest<CreateWork>();
                var @create = mapper.Map<Work>(@input);

                await service.CreateAsync(@create);

                return HttpStatusCode.Created;
            });

            Put("{id:guid}", async args =>
            {
                var @input = BindRequest<UpdateWork>();
                var @update = mapper.Map<Work>(@input);

                await service.UpdateAsync(@update);

                return HttpStatusCode.OK;
            });

            Delete("{id:guid}", async args =>
            {
                var @input = BindRequest<GetById>();
                var @delete = await service.GetByIdAsync<Work>(@input.Id);

                await service.DeleteAsync(@delete);

                return HttpStatusCode.OK;
            });
        }
    }
}