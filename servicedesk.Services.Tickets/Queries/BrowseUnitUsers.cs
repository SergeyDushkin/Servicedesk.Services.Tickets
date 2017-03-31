using System;
using servicedesk.Common.Types;

namespace servicedesk.Services.Tickets.Queries
{
    public class BrowseUnitUsers : PagedQueryBase
    {
        public Guid UnitId { get; set; }
    }
}