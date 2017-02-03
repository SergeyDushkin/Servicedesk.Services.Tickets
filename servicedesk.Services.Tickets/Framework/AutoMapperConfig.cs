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

                cfg.CreateMap<BusinessUnit, BusinessUnitDto>();
                cfg.CreateMap<CreateBusinessUnit, BusinessUnit>();
                cfg.CreateMap<UpdateBusinessUnit, BusinessUnit>();

                cfg.CreateMap<Contract, ContractDto>();
                cfg.CreateMap<CreateContract, Contract>();
                    //.ForMember(dst => dst.CreatedAt, opt => opt.MapFrom(src => System.DateTime.Now))
                    //.ForMember(dst => dst.UpdatedAt, opt => opt.MapFrom(src => System.DateTime.Now));

                cfg.CreateMap<UpdateContract, Contract>();
                    //.ForMember(dst => dst.UpdatedAt, opt => opt.MapFrom(src => System.DateTime.Now));

                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<CreateCustomer, Customer>();
                cfg.CreateMap<UpdateCustomer, Customer>();

                cfg.CreateMap<Service, ServiceDto>();
                cfg.CreateMap<CreateService, Service>();
                cfg.CreateMap<UpdateService, Service>();

                cfg.CreateMap<Supplier, SupplierDto>();
                cfg.CreateMap<CreateSupplier, Supplier>();
                cfg.CreateMap<UpdateSupplier, Supplier>();

                cfg.CreateMap<Ticket, TicketDto>();

                cfg.CreateMap<TicketPriority, TicketPriorityDto>();
                cfg.CreateMap<CreateTicketPriority, TicketPriority>();
                cfg.CreateMap<UpdateTicketPriority, TicketPriority>();

                cfg.CreateMap<TicketStatus, TicketStatusDto>();
                cfg.CreateMap<CreateTicketStatus, TicketStatus>();
                cfg.CreateMap<UpdateTicketStatus, TicketStatus>();

                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<CreateUser, User>();
                cfg.CreateMap<UpdateUser, User>();

                cfg.CreateMap<Work, WorkDto>();
                cfg.CreateMap<CreateWork, Work>()
                    .ForMember(dst => dst.Supplier, opt => opt.MapFrom(src => new Supplier(src.SupplierId)))
                    .ForMember(dst => dst.Worker, opt => opt.MapFrom(src => new User(src.WorkerId)))
                    .ForMember(dst => dst.Status, opt => opt.MapFrom(src => new WorkStatus(src.StatusId)));

                cfg.CreateMap<UpdateWork, Work>()
                    .ForMember(dst => dst.Supplier, opt => opt.MapFrom(src => new Supplier(src.SupplierId)))
                    .ForMember(dst => dst.Worker, opt => opt.MapFrom(src => new User(src.WorkerId)))
                    .ForMember(dst => dst.Status, opt => opt.MapFrom(src => new WorkStatus(src.StatusId)));

                cfg.CreateMap<WorkStatus, WorkStatusDto>();
                cfg.CreateMap<CreateWorkStatus, WorkStatus>();
                cfg.CreateMap<UpdateWorkStatus, WorkStatus>();
            });

            return config.CreateMapper();
        }
    }
}