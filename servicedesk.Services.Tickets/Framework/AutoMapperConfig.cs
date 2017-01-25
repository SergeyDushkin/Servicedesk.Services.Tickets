using AutoMapper;
using serviceDesk.Services.Tickets.Domain;
using serviceDesk.Services.Tickets.Dto;

namespace serviceDesk.Services.Tickets.Framework
{
    public class AutoMapperConfig
    {
        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StatusEvent, StatusEventDto>();
                cfg.CreateMap<StatusSource, StatusSourceDto>();
                cfg.CreateMap<Status, StatusDto>();
            });

            return config.CreateMapper();
        }
    }
}