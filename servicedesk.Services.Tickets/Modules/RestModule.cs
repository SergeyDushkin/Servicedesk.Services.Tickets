using AutoMapper;
using Coolector.Common.Extensions;
using Coolector.Common.Nancy;
using servicedesk.Common.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Services;

namespace servicedesk.Services.Tickets.Modules
{
    /*
    public class RestModule<T, TDto> : ApiModuleBase where T : class, IIdentifiable, IDependently, new()
    {
        public RestModule(IBaseDependentlyService service, IMapper mapper, string modulePath = "") : base(mapper, modulePath: modulePath)
        {
            Get("", args => FetchCollection<GetByReferenceId, T>
                (async x => (await service.GetByReferenceIdAsync<T>(x.ReferenceId)).PaginateWithoutLimit())
                .MapTo<TDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, T>
                (async x => await service.GetByIdAsync<T>(x.Id))
                .MapTo<TDto>()
                .HandleAsync());
        }
    }*/
}