using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Shared.Commands;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Controllers
{
    [Route("contracts")]
    public class ContractController : BaseCrudController<Contract, ContractDto, CreateContract, UpdateContract>
    {
        public ContractController(IBaseService service, ILoggerFactory loggerFactory, AutoMapper.IMapper mapper) : base(service, loggerFactory, mapper)
        {
        }
    }
}
