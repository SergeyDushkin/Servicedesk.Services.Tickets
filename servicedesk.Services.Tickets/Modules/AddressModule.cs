using AutoMapper;
using Coolector.Common.Extensions;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Modules
{
    public class AddressModule : ModuleBase
    {
        public AddressModule(IBaseService<Address> service, IMapper mapper) 
            : base(mapper, "addresses")
        {
            Get("", args => FetchCollection<BrowseAll, Address>
                (async x => (await service.GetAsync()).PaginateWithoutLimit())
                .MapTo<AddressDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, Address>
                (async x => await service.GetByIdAsync(x.Id))
                .MapTo<AddressDto>()
                .HandleAsync());
        }
    }
}