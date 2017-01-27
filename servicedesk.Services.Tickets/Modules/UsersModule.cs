using AutoMapper;
using Coolector.Common.Extensions;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Modules
{
    public class UsersModule : ModuleBase
    {
        public UsersModule(IBaseService<User> service, IMapper mapper) 
            : base(mapper, "users")
        {
            Get("", args => FetchCollection<BrowseAll, User>
                (async x => (await service.GetAsync()).PaginateWithoutLimit())
                .MapTo<UserDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, User>
                (async x => await service.GetByIdAsync(x.Id))
                .MapTo<UserDto>()
                .HandleAsync());
        }
    }
}