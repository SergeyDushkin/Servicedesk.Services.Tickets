using servicedesk.Common.Queries;
using servicedesk.Common.Types;

namespace servicedesk.Services.Tickets.Queries
{
    public class GetAll : PagedQueryBase, IPagedQuery, IQuery
    {
        public string Include { get; set; }
    }
}