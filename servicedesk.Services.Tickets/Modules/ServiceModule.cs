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
    public class ServiceModule : ModuleBase
    {
        public ServiceModule(IBaseDependentlyService service, IMapper mapper) : base(mapper, "services")
        {
            Get("", args => FetchCollection<GetByReferenceId, Service>
                (async x => (await service.GetByReferenceIdAsync<Service>(x.ReferenceId)).PaginateWithoutLimit())
                .MapTo<ServiceDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, Service>
                (async x => await service.GetByIdAsync<Service>(x.Id))
                .MapTo<ServiceDto>()
                .HandleAsync());

            Post("", async args =>
            {
                var @input = BindRequest<CreateService>();
                var @create = mapper.Map<Service>(@input);

                await service.CreateAsync(@create);

                return HttpStatusCode.Created;
            });

            Put("{id:guid}", async args =>
            {
                var @input = BindRequest<UpdateService>();
                var @update = mapper.Map<Service>(@input);

                await service.UpdateAsync(@update);

                return HttpStatusCode.OK;
            });

            Delete("{id:guid}", async args =>
            {
                var @input = BindRequest<GetById>();
                var @delete = await service.GetByIdAsync<Service>(@input.Id);

                await service.DeleteAsync(@delete);

                return HttpStatusCode.OK;
            });
        }
    }
}