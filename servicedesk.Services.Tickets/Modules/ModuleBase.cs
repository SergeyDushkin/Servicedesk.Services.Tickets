using AutoMapper;
using Coolector.Common.Nancy;

namespace servicedesk.Services.Tickets.Modules
{
    public abstract class ModuleBase : ApiModuleBase
    {
        protected ModuleBase() { }

        protected ModuleBase(string modulePath) 
            : base(modulePath) { }

        protected ModuleBase(IMapper mapper, string modulePath = "")
            : base(mapper, modulePath) { }
    }
}