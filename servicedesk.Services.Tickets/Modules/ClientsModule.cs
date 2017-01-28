using AutoMapper;
using Coolector.Common.Extensions;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Modules
{
    public class ClientsModule : ModuleBase
    {
        public ClientsModule(IBaseService<Client> service, IMapper mapper) 
            : base(mapper, "clients")
        {
            Get("", args => FetchCollection<GetAll, Client>
                (async x => (await service.GetAsync()).PaginateWithoutLimit())
                .MapTo<ClientDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, Client>
                (async x => await service.GetByIdAsync(x.Id))
                .MapTo<ClientDto>()
                .HandleAsync());
        }
    }
}