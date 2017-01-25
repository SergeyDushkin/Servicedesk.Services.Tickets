using serviceDesk.Services.Tickets.Dal;
using serviceDesk.Services.Tickets.Domain;

namespace serviceDesk.Services.Tickets.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(TicketDbContext context) : base(context)
        {
        }
    }
}