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
                cfg.CreateMap<CreateAddress, Address>().ForMember(dst => dst.FullAddress, opt => opt.MapFrom(src => src.Address));
                cfg.CreateMap<UpdateAddress, Address>().ForMember(dst => dst.FullAddress, opt => opt.MapFrom(src => src.Address));
                cfg.CreateMap<Address, AddressDto>();
                cfg.CreateMap<BusinessUnit, BusinessUnitDto>();
                cfg.CreateMap<Contract, ContractDto>();
                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<Service, ServiceDto>();
                cfg.CreateMap<Supplier, SupplierDto>();
                cfg.CreateMap<Ticket, TicketDto>();
                cfg.CreateMap<TicketPriority, TicketPriorityDto>();
                cfg.CreateMap<TicketStatus, TicketStatusDto>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Work, WorkDto>();
                cfg.CreateMap<WorkStatus, WorkStatusDto>();

                //cfg.CreateMap<Job, JobCreated>();
                //cfg.CreateMap<CreateJob, Job>();
            });

            return config.CreateMapper();
        }
    }
}