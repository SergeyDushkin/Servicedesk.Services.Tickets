using AutoMapper;
using servicedesk.Services.Tickets.Domain;
using servicedesk.Services.Tickets.Dto;

namespace servicedesk.Services.Tickets.Framework
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