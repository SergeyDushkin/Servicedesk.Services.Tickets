using Coolector.Common.Queries;

namespace serviceDesk.Services.Tickets.Queries
{
    public class GetStatusSourceByName : IQuery
    {
        public string Name { get; set; }
    }
}