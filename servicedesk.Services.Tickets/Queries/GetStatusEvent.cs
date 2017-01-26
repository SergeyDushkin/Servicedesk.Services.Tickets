using System;
using Coolector.Common.Queries;

namespace servicedesk.Services.Tickets.Queries
{
    public class GetStatusEvent : IQuery
    {
        public Guid SourceId { get; set; }
        public Guid ReferenceId { get; set; }
    }
}