using System;
using Coolector.Common.Queries;

namespace servicedesk.Services.Tickets.Queries
{
    public class GetById : IQuery
    {
        public Guid Id { get; set; }
    }
}