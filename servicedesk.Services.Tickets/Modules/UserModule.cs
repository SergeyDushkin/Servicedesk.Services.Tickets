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
    public class UserModule : ModuleBase
    {
        public UserModule(IBaseDependentlyService service, IMapper mapper) : base(mapper, "users")
        {
            Get("", args => FetchCollection<GetByReferenceId, User>
                (async x => (await service.GetByReferenceIdAsync<User>(x.ReferenceId)).Paginate(x))
                .MapTo<UserDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, User>
                (async x => await service.GetByIdAsync<User>(x.Id))
                .MapTo<UserDto>()
                .HandleAsync());

            Post("", async args =>
            {
                var @input = BindRequest<CreateUser>();
                var @create = mapper.Map<User>(@input);
                
                if (!@input.Id.GetValueOrDefault().IsEmpty())
                {
                    @create.SetId(@input.Id.GetValueOrDefault());
                }

                await service.CreateAsync(@create);

                return HttpStatusCode.Created;
            });

            Put("{id:guid}", async args =>
            {
                var @input = BindRequest<UpdateUser>();
                var @update = mapper.Map<User>(@input);

                await service.UpdateAsync(@update);

                return HttpStatusCode.OK;
            });

            Delete("{id:guid}", async args =>
            {
                var @input = BindRequest<GetById>();
                var @delete = await service.GetByIdAsync<User>(@input.Id);

                await service.DeleteAsync(@delete);

                return HttpStatusCode.OK;
            });
        }
    }
}