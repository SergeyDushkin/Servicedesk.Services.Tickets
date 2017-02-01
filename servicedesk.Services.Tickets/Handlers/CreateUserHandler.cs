using System.Threading.Tasks;
using RawRabbit;
using servicedesk.Common.Commands;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Commands;

namespace servicedesk.Services.Tickets.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IHandler handler;
        private readonly IBusClient bus;
        private readonly IBaseDependentlyService<User> service;

        public CreateUserHandler(IHandler handler, IBusClient bus, IBaseDependentlyService<User> service)
        {
            this.handler = handler;
            this.bus = bus;
            this.service = service;
        }

        public async Task HandleAsync(CreateUser command)
        {
            var user = new User {
                ReferenceId = command.ReferenceId,
                FirstName = command.Name
            };

            await handler
                .Run(async () => await service.CreateAsync(user))
                .OnSuccess((logger) => logger.Info("New user created successfully"))
                .OnError((ex, logger) => logger.Error(ex, "Error when trying to create new user: " + ex.GetBaseException().Message))
                .ExecuteAsync();
        }
    }
}