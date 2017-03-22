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
    public class SupplierModule : ModuleBase
    {
        public SupplierModule(IBaseDependentlyService service, IMapper mapper) : base(mapper, "suppliers")
        {
            Get("", args => FetchCollection<GetByReferenceId, Supplier>
                (async x => (await service.GetByReferenceIdAsync<Supplier>(x.ReferenceId)).Paginate(x))
                .MapTo<SupplierDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, Supplier>
                (async x => await service.GetByIdAsync<Supplier>(x.Id))
                .MapTo<SupplierDto>()
                .HandleAsync());

            Post("", async args =>
            {
                var @input = BindRequest<CreateSupplier>();
                var @create = mapper.Map<Supplier>(@input);
                
                if (!@input.Id.GetValueOrDefault().IsEmpty())
                {
                    @create.SetId(@input.Id.GetValueOrDefault());
                }

                await service.CreateAsync(@create);

                return HttpStatusCode.Created;
            });

            Put("{id:guid}", async args =>
            {
                var @input = BindRequest<UpdateSupplier>();
                var @update = mapper.Map<Supplier>(@input);

                await service.UpdateAsync(@update);

                return HttpStatusCode.OK;
            });

            Delete("{id:guid}", async args =>
            {
                var @input = BindRequest<GetById>();
                var @delete = await service.GetByIdAsync<Supplier>(@input.Id);

                await service.DeleteAsync(@delete);

                return HttpStatusCode.OK;
            });
        }
    }
}