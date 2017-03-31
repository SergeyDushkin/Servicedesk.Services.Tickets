using System;
using System.Threading.Tasks;
using System.Linq;
using servicedesk.Services.Tickets.Dal;
using servicedesk.Services.Tickets.Domain;

namespace servicedesk.Services.Tickets.Framework
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseSeeder
    {
        private readonly TicketDbContext context;

        public DatabaseInitializer(TicketDbContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            var supplier = new Supplier(Guid.Empty)
            {
                Resource = "own",
                Name = "Моя компания"
            };

            if (!context.Suppliers.Any(r => r.Resource == supplier.Resource))
            {
                await context.Suppliers.AddAsync(supplier);
                await context.SaveChangesAsync();
            }

            if (!context.Channels.Any(r => r.Code == "email"))
            {
                await context.Channels.AddAsync(new Channel { Code = "email", Name = "Электронная почта" });
                await context.SaveChangesAsync();
            }

            if (!context.Channels.Any(r => r.Code == "phone"))
            {
                await context.Channels.AddAsync(new Channel { Code = "phone", Name = "Горячая линия" });
                await context.SaveChangesAsync();
            }

            if (!context.Channels.Any(r => r.Code == "web"))
            {
                await context.Channels.AddAsync(new Channel { Code = "web", Name = "Web интерфейс" });
                await context.SaveChangesAsync();
            }
        }
    }
}