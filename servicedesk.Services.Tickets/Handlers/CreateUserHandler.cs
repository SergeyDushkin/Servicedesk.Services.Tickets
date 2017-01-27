using System.Threading.Tasks;
using Coolector.Common.Services;
using RawRabbit;
using servicedesk.Common.Commands;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Services;
using servicedesk.Services.Tickets.Shared.Commands;

namespace servicedesk.Services.Tickets.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IHandler handler;
        private readonly IBusClient bus;
        private readonly IBaseService<User> service;

        public CreateUserHandler(IHandler handler, IBusClient bus, IBaseService<User> service)
        {
            this.handler = handler;
            this.bus = bus;
            this.service = service;
        }

        public async Task HandleAsync(CreateUser command)
        {
            var user = new User {
                Name = command.Name
            };

            await handler
                .Run(async () => await service.CreateAsync(user))
                //.OnSuccess((logger) => logger.Error(ex, "New address created successfully"))
                .OnError((ex, logger) => logger.Error(ex, "Error when trying to create new user: " + ex.GetBaseException().Message))
                .ExecuteAsync();
        }
    }
}