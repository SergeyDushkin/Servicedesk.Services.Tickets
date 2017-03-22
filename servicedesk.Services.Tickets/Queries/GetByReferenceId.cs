using System;
using Collectively.Common.Types;

namespace servicedesk.Services.Tickets.Queries
{
    public class GetByReferenceId : PagedQueryBase
    {
        public Guid ReferenceId { get; set; }
    }
}