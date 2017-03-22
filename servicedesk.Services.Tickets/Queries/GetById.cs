using System;
using Collectively.Common.Queries;

namespace servicedesk.Services.Tickets.Queries
{
    public class GetById : IQuery
    {
        public Guid Id { get; set; }
    }
}