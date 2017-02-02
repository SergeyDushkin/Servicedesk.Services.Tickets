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
    public class AddressModule : ModuleBase
    {
        public AddressModule(IBaseDependentlyService service, IMapper mapper) : base(mapper, "addresses")
        {
            Get("", args => FetchCollection<GetByReferenceId, Address>
                (async x => (await service.GetByReferenceIdAsync<Address>(x.ReferenceId)).PaginateWithoutLimit())
                .MapTo<AddressDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, Address>
                (async x => await service.GetByIdAsync<Address>(x.Id))
                .MapTo<AddressDto>()
                .HandleAsync());

            Post("", async args =>
            {
                var @input = BindRequest<CreateAddress>();
                var @create = mapper.Map<Address>(@input);

                await service.CreateAsync(@create);

                return HttpStatusCode.Created;
            });

            Put("{id:guid}", async args =>
            {
                var @input = BindRequest<UpdateAddress>();
                var @update = mapper.Map<Address>(@input);

                await service.UpdateAsync(@update);

                return HttpStatusCode.OK;
            });

            Delete("{id:guid}", async args =>
            {
                var @input = BindRequest<GetById>();
                var @delete = await service.GetByIdAsync<Address>(@input.Id);

                await service.DeleteAsync(@delete);

                return HttpStatusCode.OK;
            });
        }
    }
}