using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;

namespace servicedesk.Services.Tickets.Controllers
{
    [Route("unit-service")]
    public class UnitServiceController : BaseController<UnitService, UnitServiceDto, CreateUnitService, UpdateUnitService>
    {
        public UnitServiceController(IBaseService service, ILoggerFactory loggerFactory, AutoMapper.IMapper mapper) : base(service, loggerFactory, mapper)
        {
        }
        
        [HttpGet]
        [Route("service")]
        public IActionResult GetServiceByUnitId(GetServiceByUnitId query) => service
            .Query<UnitService>(r => r.UnitId == query.UnitId, r => r.Service)
            .SingleOrDefault()
            .PipeTo(OkOrNotFound);
    }
}
