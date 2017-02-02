using AutoMapper;
using Coolector.Common.Commands;
using Coolector.Common.Nancy;
using System.Linq;

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