using AutoMapper;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Shared.Dto;

namespace servicedesk.Services.Tickets.Framework
{
    public class AutoMapperConfig
    {
        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressDto>().ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.FullAddress));
                cfg.CreateMap<Client, ClientDto>();
                cfg.CreateMap<Ticket, TicketDto>();
                cfg.CreateMap<User, UserDto>();
            });

            return config.CreateMapper();
        }
    }
}