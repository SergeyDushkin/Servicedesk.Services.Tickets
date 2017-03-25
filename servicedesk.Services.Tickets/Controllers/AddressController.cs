using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Shared.Commands;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Controllers
{
    [Route("addresses")]
    public class AddressController : BaseCrudController<Address, AddressDto, CreateAddress, UpdateAddress>
    {
        public AddressController(IBaseService service, ILoggerFactory loggerFactory, IMapper mapper) : base(service, loggerFactory, mapper)
        {
        }
    }
}
