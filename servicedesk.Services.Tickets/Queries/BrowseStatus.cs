using System;
using Coolector.Common.Types;

namespace servicedesk.Services.Tickets.Queries
{
    public class BrowseStatus : PagedQueryBase
    {
        public Guid SourceId { get;set; }
    }
}