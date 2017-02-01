using AutoMapper;
using Coolector.Common.Extensions;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Modules
{
    public class CustomerModule : ModuleBase
    {
        public CustomerModule(IBaseService<Customer> service, IMapper mapper) 
            : base(mapper, "customers")
        {
            Get("", args => FetchCollection<GetAll, Customer>
                (async x => (await service.GetAsync()).PaginateWithoutLimit())
                .MapTo<ClientDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, Customer>
                (async x => await service.GetByIdAsync(x.Id))
                .MapTo<ClientDto>()
                .HandleAsync());
        }
    }
}