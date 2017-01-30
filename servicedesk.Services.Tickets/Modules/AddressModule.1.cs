using AutoMapper;
using Coolector.Common.Extensions;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Modules
{
    public class JobsModule : ModuleBase
    {
        public JobsModule(IBaseDependentlyService<Job> service, IMapper mapper) 
            : base(mapper, "jobs")
        {
            Get("", args => FetchCollection<GetByReferenceId, Job>
                (async x => (await service.GetByReferenceIdAsync(x.ReferenceId)).PaginateWithoutLimit())
                .MapTo<JobDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, Job>
                (async x => await service.GetByIdAsync(x.Id))
                .MapTo<JobDto>()
                .HandleAsync());
        }
    }
}