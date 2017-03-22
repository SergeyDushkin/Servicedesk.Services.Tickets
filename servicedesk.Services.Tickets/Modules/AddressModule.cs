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
    public class AddressModule : ModuleBase
    {
        public AddressModule(IBaseDependentlyService service, IMapper mapper) : base(mapper, "addresses")
        {
            //HttpStatusCode Create<TCommand, T, TResult>(System.Action<TCommand, T, HttpStatusCode> fetch)
            //    where TQuery : IQuery, new()
            //    where TResult : class
            //{
            //    return HttpStatusCode.OK;
            //}

            Get("", args => FetchCollection<GetByReferenceId, Address>
                (async x => (await service.GetByReferenceIdAsync<Address>(x.ReferenceId)).Paginate(x))
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
                
                if (!@input.Id.GetValueOrDefault().IsEmpty())
                {
                    @create.SetId(@input.Id.GetValueOrDefault());
                }

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