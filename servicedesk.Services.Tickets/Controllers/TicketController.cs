using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Controllers
{
    [Route("tickets")]
    public class TicketController : BaseCrudController<Ticket, TicketDto>
    {
        public TicketController(IBaseService service, ILoggerFactory loggerFactory) : base(service, loggerFactory)
        {
        }
    }
}
