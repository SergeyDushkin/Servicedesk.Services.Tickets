using AutoMapper;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Shared.Commands;
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
                cfg.CreateMap<CreateAddress, Address>().ForMember(dst => dst.FullAddress, opt => opt.MapFrom(src => src.Address));
                cfg.CreateMap<UpdateAddress, Address>().ForMember(dst => dst.FullAddress, opt => opt.MapFrom(src => src.Address));

                cfg.CreateMap<BusinessUnit, BusinessUnitDto>();
                cfg.CreateMap<CreateBusinessUnit, BusinessUnit>();
                cfg.CreateMap<UpdateBusinessUnit, BusinessUnit>();

                cfg.CreateMap<Contract, ContractDto>();
                cfg.CreateMap<CreateContract, Contract>();
                cfg.CreateMap<UpdateContract, Contract>();

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
                cfg.CreateMap<CreateWork, Work>();
                cfg.CreateMap<UpdateWork, Work>();

                cfg.CreateMap<WorkStatus, WorkStatusDto>();
                cfg.CreateMap<CreateWorkStatus, WorkStatus>();
                cfg.CreateMap<UpdateWorkStatus, WorkStatus>();

                //cfg.CreateMap<AppendServiceToContract, ContractService>();

                cfg.CreateMap<ContractService, ContractServiceDto>();
                cfg.CreateMap<CreateContractService, ContractService>();
                cfg.CreateMap<UpdateContractService, ContractService>();

                cfg.CreateMap<UnitUser, UnitUserDto>();
                cfg.CreateMap<CreateUnitUser, UnitUser>();
                cfg.CreateMap<UpdateUnitUser, UnitUser>();

                cfg.CreateMap<UnitService, UnitServiceDto>();
                cfg.CreateMap<CreateUnitService, UnitService>();
                cfg.CreateMap<UpdateUnitService, UnitService>();

                cfg.CreateMap<Channel, ChannelDto>();
                cfg.CreateMap<CreateChannel, Channel>();
                cfg.CreateMap<UpdateChannel, Channel>();
            });

            return config.CreateMapper();
        }
    }
}