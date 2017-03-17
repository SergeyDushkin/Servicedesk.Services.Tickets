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
    public class TicketStatusModule : ModuleBase
    {
        public TicketStatusModule(IBaseService service, IMapper mapper) : base(mapper, "ticket-status")
        {
            Get("", args => FetchCollection<GetAll, TicketStatus>
                (async x => (await service.GetAsync<TicketStatus>()).PaginateWithoutLimit())
                .MapTo<TicketStatusDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, TicketStatus>
                (async x => await service.GetByIdAsync<TicketStatus>(x.Id))
                .MapTo<TicketStatusDto>()
                .HandleAsync());

            Post("", async args =>
            {
                var @input = BindRequest<CreateTicketStatus>();
                var @create = mapper.Map<TicketStatus>(@input);
                
                if (!@input.Id.GetValueOrDefault().IsEmpty())
                {
                    @create.SetId(@input.Id.GetValueOrDefault());
                }

                await service.CreateAsync(@create);

                return HttpStatusCode.Created;
            });

            Put("{id:guid}", async args =>
            {
                var @input = BindRequest<UpdateTicketStatus>();
                var @update = mapper.Map<TicketStatus>(@input);

                await service.UpdateAsync(@update);

                return HttpStatusCode.OK;
            });

            Delete("{id:guid}", async args =>
            {
                var @input = BindRequest<GetById>();
                var @delete = await service.GetByIdAsync<TicketStatus>(@input.Id);

                await service.DeleteAsync(@delete);

                return HttpStatusCode.OK;
            });
        }
    }
}