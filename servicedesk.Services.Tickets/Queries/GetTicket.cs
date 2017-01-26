using System;
using Coolector.Common.Queries;

namespace servicedesk.Services.Tickets.Queries
{
    public class GetTicket : IQuery
    {
        public Guid Id { get; set; }
    }
}