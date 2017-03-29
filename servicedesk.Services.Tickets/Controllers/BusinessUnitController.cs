using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Shared.Commands;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Controllers
{
    [Route("businessunits")]
    public class BusinessUnitController : BaseCrudController<BusinessUnit, BusinessUnitDto, CreateBusinessUnit, UpdateBusinessUnit>
    {
        public BusinessUnitController(IBaseService service, ILoggerFactory loggerFactory, IMapper mapper) : base(service, loggerFactory, mapper)
        {
        }
    }
}
