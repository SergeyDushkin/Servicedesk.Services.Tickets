using System;
using Coolector.Common.Queries;

namespace servicedesk.Services.Tickets.Queries
{
    public class GetStatus : IQuery
    {
        public Guid Id { get; set; }
    }
}