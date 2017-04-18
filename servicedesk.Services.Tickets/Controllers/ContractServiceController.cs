using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Queries;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Controllers
{
    [Route("contract-service")]
    public class ContractServiceController : BaseController<ContractService, ContractServiceDto, CreateContractService, UpdateContractService>
    {
        public ContractServiceController(IBaseService service, ILoggerFactory loggerFactory, AutoMapper.IMapper mapper) : base(service, loggerFactory, mapper)
        {
        }
        
        [HttpGet]
        [Route("contract")]
        public IActionResult GetContractByServiceId(GetContractByServiceId query) => service
            .Query<ContractService>(r => r.ServiceId == query.ServiceId, r => r.Contract)
            .SingleOrDefault()
            .PipeTo(OkOrNotFound);
    }
}
