using Coolector.Common.Queries;

namespace servicedesk.Services.Tickets.Queries
{
    public class GetStatusSourceByName : IQuery
    {
        public string Name { get; set; }
    }
}