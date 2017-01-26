using System;
using Coolector.Common.Types;

namespace servicedesk.Services.Tickets.Queries
{
    public class BrowseEventStatus : PagedQueryBase
    {
        public string Name { get; set; }
        public Guid SourceId { get; set; }
        public Guid ReferenceId { get; set; }
    }
}