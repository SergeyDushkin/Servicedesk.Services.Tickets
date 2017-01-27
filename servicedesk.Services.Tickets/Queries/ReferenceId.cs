using System;
using Coolector.Common.Types;

namespace servicedesk.Services.Tickets.Queries
{
    public class SearchByReferenceId : PagedQueryBase
    {
        public Guid ReferenceId { get; set; }
    }
}