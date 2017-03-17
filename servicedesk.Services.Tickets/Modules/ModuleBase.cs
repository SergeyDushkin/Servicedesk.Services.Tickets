using AutoMapper;
using Coolector.Common.Nancy;
using Nancy;
using servicedesk.Common.Domain;
using servicedesk.Common.Services;

namespace servicedesk.Services.Tickets.Modules
{
    public abstract class ModuleBase : ApiModuleBase
    {
        protected ModuleBase() { }

        protected ModuleBase(string modulePath) 
            : base(modulePath) { }

        protected ModuleBase(IMapper mapper, string modulePath = "")
            : base(mapper, modulePath) { }

        public async System.Threading.Tasks.Task<HttpStatusCode> Update<TCommand, T>(IBaseService service, IMapper mapper) 
            where TCommand : new()
            where T : class, IIdentifiable, new()
        {
            var @input = BindRequest<TCommand>();
            var @update = mapper.Map<T>(@input);
            
            await service.UpdateAsync(@update);
            return HttpStatusCode.OK;
        }
    }
}