using Coolector.Common.Types;

namespace serviceDesk.Services.Tickets.Queries
{
    public class BrowseStatusBySourceName : PagedQueryBase
    {
        public string Name { get;set; }
    }
}