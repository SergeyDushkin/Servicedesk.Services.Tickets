using AutoMapper;
using Collectively.Common.Extensions;
using Nancy;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Shared.Commands;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Modules
{
    public class WorkStatusModule : ModuleBase
    {
        public WorkStatusModule(IBaseService service, IMapper mapper) : base(mapper, "work-status")
        {
            Get("", args => FetchCollection<GetAll, WorkStatus>
                (async x => (await service.GetAsync<WorkStatus>()).PaginateWithoutLimit())
                .MapTo<WorkStatusDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, WorkStatus>
                (async x => await service.GetByIdAsync<WorkStatus>(x.Id))
                .MapTo<WorkStatusDto>()
                .HandleAsync());

            Post("", async args =>
            {
                var @input = BindRequest<CreateWorkStatus>();
                var @create = mapper.Map<WorkStatus>(@input);
                
                if (!@input.Id.GetValueOrDefault().IsEmpty())
                {
                    @create.SetId(@input.Id.GetValueOrDefault());
                }

                await service.CreateAsync(@create);

                return HttpStatusCode.Created;
            });

            Put("{id:guid}", async args =>
            {
                var @input = BindRequest<UpdateWorkStatus>();
                var @update = mapper.Map<WorkStatus>(@input);

                await service.UpdateAsync(@update);

                return HttpStatusCode.OK;
            });

            Delete("{id:guid}", async args =>
            {
                var @input = BindRequest<GetById>();
                var @delete = await service.GetByIdAsync<WorkStatus>(@input.Id);

                await service.DeleteAsync(@delete);

                return HttpStatusCode.OK;
            });
        }
    }
}