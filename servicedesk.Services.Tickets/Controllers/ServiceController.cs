using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Controllers
{
    [Route("services")]
    public class ServiceController : BaseCrudController<Service, ServiceDto>
    {
        public ServiceController(IBaseService service, ILoggerFactory loggerFactory) : base(service, loggerFactory)
        {
        }
    }
}
