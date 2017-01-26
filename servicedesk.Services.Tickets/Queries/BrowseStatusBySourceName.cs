using Coolector.Common.Types;

namespace servicedesk.Services.Tickets.Queries
{
    public class BrowseStatusBySourceName : PagedQueryBase
    {
        public string Name { get;set; }
    }
}