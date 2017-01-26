using servicedesk.Services.Tickets.Dal;
using servicedesk.Services.Tickets.Domain;

namespace servicedesk.Services.Tickets.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(TicketDbContext context) : base(context)
        {
        }
    }
}