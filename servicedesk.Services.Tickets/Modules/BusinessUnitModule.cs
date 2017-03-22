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
    public class BusinessUnitModule : ModuleBase
    {
        public BusinessUnitModule(IBaseDependentlyService service, IMapper mapper) : base(mapper, "businessunits")
        {
            Get("", args => FetchCollection<GetByReferenceId, BusinessUnit>
                (async x => (await service.GetByReferenceIdAsync<BusinessUnit>(x.ReferenceId)).PaginateWithoutLimit())
                .MapTo<BusinessUnitDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, BusinessUnit>
                (async x => await service.GetByIdAsync<BusinessUnit>(x.Id))
                .MapTo<BusinessUnitDto>()
                .HandleAsync());

            Post("", async args =>
            {
                var @input = BindRequest<CreateBusinessUnit>();
                var @create = mapper.Map<BusinessUnit>(@input);
                
                if (!@input.Id.GetValueOrDefault().IsEmpty())
                {
                    @create.SetId(@input.Id.GetValueOrDefault());
                }

                await service.CreateAsync(@create);

                return HttpStatusCode.Created;
            });

            Put("{id:guid}", async args =>
            {
                var @input = BindRequest<UpdateBusinessUnit>();
                var @update = mapper.Map<BusinessUnit>(@input);

                await service.UpdateAsync(@update);

                return HttpStatusCode.OK;
            });

            Delete("{id:guid}", async args =>
            {
                var @input = BindRequest<GetById>();
                var @delete = await service.GetByIdAsync<BusinessUnit>(@input.Id);

                await service.DeleteAsync(@delete);

                return HttpStatusCode.OK;
            });
        }
    }
}