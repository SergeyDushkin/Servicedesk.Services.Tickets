using AutoMapper;
using Coolector.Common.Extensions;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Modules
{
    public class WorksModule : ModuleBase
    {
        public WorksModule(IBaseDependentlyService<Work> service, IMapper mapper) 
            : base(mapper, "works")
        {
            Get("", args => FetchCollection<GetByReferenceId, Work>
                (async x => (await service.GetByReferenceIdAsync(x.ReferenceId)).PaginateWithoutLimit())
                .MapTo<JobDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, Work>
                (async x => await service.GetByIdAsync(x.Id))
                .MapTo<JobDto>()
                .HandleAsync());
        }
    }
}