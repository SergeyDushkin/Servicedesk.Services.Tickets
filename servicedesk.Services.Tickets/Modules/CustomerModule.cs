using AutoMapper;
using Coolector.Common.Extensions;
using Nancy;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Shared.Commands;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Modules
{
    public class CustomerModule : ModuleBase
    {
        public CustomerModule(IBaseDependentlyService service, IMapper mapper) : base(mapper, "customers")
        {
            Get("", args => FetchCollection<GetByReferenceId, Customer>
                (async x => (await service.GetByReferenceIdAsync<Customer>(x.ReferenceId)).PaginateWithoutLimit())
                .MapTo<CustomerDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, Customer>
                (async x => await service.GetByIdAsync<Customer>(x.Id))
                .MapTo<CustomerDto>()
                .HandleAsync());

            Post("", async args =>
            {
                var @input = BindRequest<CreateCustomer>();
                var @create = mapper.Map<Customer>(@input);
                
                if (!@input.Id.GetValueOrDefault().IsEmpty())
                {
                    @create.SetId(@input.Id.GetValueOrDefault());
                }

                await service.CreateAsync(@create);

                return HttpStatusCode.Created;
            });

            Put("{id:guid}", async args =>
            {
                var @input = BindRequest<UpdateCustomer>();
                var @update = mapper.Map<Customer>(@input);

                await service.UpdateAsync(@update);

                return HttpStatusCode.OK;
            });

            Delete("{id:guid}", async args =>
            {
                var @input = BindRequest<GetById>();
                var @delete = await service.GetByIdAsync<Customer>(@input.Id);

                await service.DeleteAsync(@delete);

                return HttpStatusCode.OK;
            });
        }
    }
}