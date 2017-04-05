using System;
using servicedesk.Common.Queries;

namespace servicedesk.Services.Tickets.Queries
{
    public class GetServiceByUnitId : IQuery
    {
        public Guid UnitId { get; set; }
    }
}