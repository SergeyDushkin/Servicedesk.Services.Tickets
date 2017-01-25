using System;
using Coolector.Common.Types;

namespace serviceDesk.Services.Tickets.Queries
{
    public class BrowseEventStatus : PagedQueryBase
    {
        public string Name { get; set; }
        public Guid SourceId { get; set; }
        public Guid ReferenceId { get; set; }
    }
}