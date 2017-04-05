using System;
using servicedesk.Common.Queries;

namespace servicedesk.Services.Tickets.Queries
{
    public class GetUnitByServiceId : IQuery
    {
        public Guid ServiceId { get; set; }
    }
}