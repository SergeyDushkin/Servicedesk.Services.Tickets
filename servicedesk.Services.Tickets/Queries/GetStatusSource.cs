using System;
using Coolector.Common.Queries;

namespace serviceDesk.Services.Tickets.Queries
{
    public class GetStatusSource : IQuery
    {
        public Guid Id { get; set; }
    }
}