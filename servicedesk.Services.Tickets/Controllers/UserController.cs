using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicedesk.Common.Services;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Shared.Commands;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Controllers
{
    [Route("users")]
    public class UserController : BaseCrudController<User, UserDto, CreateUser, UpdateUser>
    {
        public UserController(IBaseService service, ILoggerFactory loggerFactory, IMapper mapper) : base(service, loggerFactory, mapper)
        {
        }
    }
}
