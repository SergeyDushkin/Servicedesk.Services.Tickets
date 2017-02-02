using System.Threading.Tasks;
using RawRabbit;
using servicedesk.Common.Commands;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Commands;
using AutoMapper;
using servicedesk.Services.Tickets.Shared.Events;

namespace servicedesk.Services.Tickets.Handlers
{
    public class CreateJobHandler : ICommandHandler<CreateJob>
    {
        private readonly IHandler handler;
        private readonly IBusClient bus;
        private readonly IBaseDependentlyService service;
        private readonly IMapper mapper;

        public CreateJobHandler(IHandler handler, IBusClient bus, IBaseDependentlyService service, IMapper mapper)
        {
            this.handler = handler;
            this.bus = bus;
            this.service = service;
            this.mapper = mapper;
        }

        public async Task HandleAsync(CreateJob command)
        {
            /*
            var job = new Job {
                ReferenceId = command.ReferenceId,
                Resource = command.Resource,
                UserId = command.UserId,
                ServiceId = command.ServiceId,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                Description = command.Description
            };*/

            var job = mapper.Map<Work>(command);
            var @event = mapper.Map<JobCreated>(job);

            await handler
                .Run(async () => await service.CreateAsync(job))
                .OnSuccess(async (logger) => {

                    logger.Info("New job created successfully");
                    logger.Debug($"Publish event ticket_job_created: {Newtonsoft.Json.JsonConvert.SerializeObject(@event)}");

                    await bus.PublishAsync(@event, command.Request.Id,
                        cfg => cfg.WithExchange(e => e.WithName("servicedesk.Services.Tickets")).WithRoutingKey("ticket.job.created"));
                })
                .OnError(async (ex, logger) => {

                    logger.Error(ex, "Error when trying to create new job: " + ex.GetBaseException().Message);

                    await bus.PublishAsync(new Rejected(command.Request.Id, command.UserId, "error", "Error when trying to create new job: " + ex.GetBaseException().Message), command.Request.Id,
                        cfg => cfg.WithExchange(e => e.WithName("servicedesk.Services.Tickets")).WithRoutingKey("ticket.job.rejected"));
                })
                .ExecuteAsync();
        }

        
    }
}