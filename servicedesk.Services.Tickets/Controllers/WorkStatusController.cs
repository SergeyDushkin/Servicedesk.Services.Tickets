using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Shared.Commands;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Controllers
{
    [Route("work-status")]
    public class WorkStatusController : BaseController<WorkStatus, WorkStatusDto, CreateWorkStatus, UpdateWorkStatus>
    {
        public WorkStatusController(IBaseService service, ILoggerFactory loggerFactory, AutoMapper.IMapper mapper) : base(service, loggerFactory, mapper)
        {
        }
    }
}
