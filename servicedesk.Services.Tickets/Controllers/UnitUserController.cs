using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Controllers
{
    [Route("unit-user")]
    public class UnitUserController : BaseController<UnitUser, UnitUserDto, CreateUnitUser, UpdateUnitUser>
    {
        public UnitUserController(IBaseService service, ILoggerFactory loggerFactory, AutoMapper.IMapper mapper) : base(service, loggerFactory, mapper)
        {
        }
        
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsersByUnitId(BrowseUnitUsers query)
        {
            var data = service.Query<UnitUser>(r => r.UnitId == query.UnitId, r => r.User).Select(r => r.User);
            return await PagedResult(data, query);
        }
    }
}
