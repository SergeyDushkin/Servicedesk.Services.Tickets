using System.Threading.Tasks;
using RawRabbit;
using servicedesk.Common.Commands;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Commands;

namespace servicedesk.Services.Tickets.Handlers
{
    public class CreateJobHandler : ICommandHandler<CreateJob>
    {
        private readonly IHandler handler;
        private readonly IBusClient bus;
        private readonly IBaseDependentlyService<Job> service;

        public CreateJobHandler(IHandler handler, IBusClient bus, IBaseDependentlyService<Job> service)
        {
            this.handler = handler;
            this.bus = bus;
            this.service = service;
        }

        public async Task HandleAsync(CreateJob command)
        {
            var job = new Job {
                ReferenceId = command.ReferenceId,
                Resource = command.Resource,
                UserId = command.UserId,
                ServiceId = command.ServiceId,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                Description = command.Description
            };

            await handler
                .Run(async () => await service.CreateAsync(job))
                .OnSuccess((logger) => logger.Info("New job created successfully"))
                .OnError((ex, logger) => logger.Error(ex, "Error when trying to create new job: " + ex.GetBaseException().Message))
                .ExecuteAsync();
        }
    }
}