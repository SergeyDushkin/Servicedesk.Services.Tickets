using System;
using Coolector.Common.Types;

namespace serviceDesk.Services.Tickets.Queries
{
    public class BrowseStatus : PagedQueryBase
    {
        public Guid SourceId { get;set; }
    }
}