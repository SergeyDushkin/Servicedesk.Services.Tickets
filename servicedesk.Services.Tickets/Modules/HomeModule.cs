using Nancy;

namespace serviceDesk.Services.Tickets.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule()
        {
            Get("", args => Response.AsJson(new { name = "serviceDesk.Services.Tickets" }));
        }
    }
}