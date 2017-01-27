using servicedesk.Services.Tickets.Dal;
using servicedesk.Services.Tickets.Domain;

namespace servicedesk.Services.Tickets.Repositories
{
    public class TicketRepository : BaseRepository<Ticket, TicketDbContext>, ITicketRepository
    {
        public TicketRepository(TicketDbContext context) : base(context)
        {
        }
    }
}