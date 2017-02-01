using AutoMapper;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Shared.Commands;
using servicedesk.Services.Tickets.Shared.Dto;
using servicedesk.Services.Tickets.Shared.Events;

namespace servicedesk.Services.Tickets.Framework
{
    public class AutoMapperConfig
    {
        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressDto>().ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.FullAddress));
                cfg.CreateMap<Ticket, TicketDto>();
                cfg.CreateMap<User, UserDto>();
                //cfg.CreateMap<Job, JobDto>();
                //cfg.CreateMap<Job, JobCreated>();
                //cfg.CreateMap<CreateJob, Job>();
            });

            return config.CreateMapper();
        }
    }
}