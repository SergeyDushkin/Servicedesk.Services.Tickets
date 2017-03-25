using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Controllers
{
    [Route("ticket-status")]
    public class TicketStatusController : BaseController<TicketStatus, TicketStatusDto>
    {
        public TicketStatusController(IBaseService service, ILoggerFactory loggerFactory) : base(service, loggerFactory)
        {
        }
    }
}
