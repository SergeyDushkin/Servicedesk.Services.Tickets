using System;
using Collectively.Common.Queries;

namespace servicedesk.Services.Tickets.Queries
{
    public class GetTicket : IQuery
    {
        public Guid Id { get; set; }
    }
}