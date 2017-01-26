using Nancy;

namespace servicedesk.Services.Tickets.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule()
        {
            Get("", args => Response.AsJson(new { name = "servicedesk.Services.Tickets" }));
        }
    }
}