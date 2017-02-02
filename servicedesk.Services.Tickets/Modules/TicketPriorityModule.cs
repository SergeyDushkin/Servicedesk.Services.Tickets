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
    public class TicketPriorityModule : ModuleBase
    {
        public TicketPriorityModule(IBaseService service, IMapper mapper) : base(mapper, "ticket-priority")
        {
            Get("", args => FetchCollection<GetAll, TicketPriority>
                (async x => (await service.GetAsync<TicketPriority>()).PaginateWithoutLimit())
                .MapTo<TicketPriorityDto>()
                .HandleAsync());

            Get("{id:guid}", args => Fetch<GetById, TicketPriority>
                (async x => await service.GetByIdAsync<TicketPriority>(x.Id))
                .MapTo<TicketPriorityDto>()
                .HandleAsync());

            Post("", async args =>
            {
                var @input = BindRequest<CreateTicketPriority>();
                var @create = mapper.Map<TicketPriority>(@input);

                await service.CreateAsync(@create);

                return HttpStatusCode.Created;
            });

            Put("{id:guid}", async args =>
            {
                var @input = BindRequest<UpdateTicketPriority>();
                var @update = mapper.Map<TicketPriority>(@input);

                await service.UpdateAsync(@update);

                return HttpStatusCode.OK;
            });

            Delete("{id:guid}", async args =>
            {
                var @input = BindRequest<GetById>();
                var @delete = await service.GetByIdAsync<TicketPriority>(@input.Id);

                await service.DeleteAsync(@delete);

                return HttpStatusCode.OK;
            });
        }
    }
}