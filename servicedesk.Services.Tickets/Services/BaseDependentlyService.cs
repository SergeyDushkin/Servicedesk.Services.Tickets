using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using servicedesk.Common.Domain;
using servicedesk.Services.Tickets.Repositories;

namespace servicedesk.Services.Tickets.Services
{
    public class BaseDependentlyService<T> : BaseService<T>, IBaseDependentlyService<T> where T : class, IIdentifiable, IDependently, new()
    {
        public BaseDependentlyService<T>(IBaseRepository<T> repository) : base(repository)
        
        public Task<IEnumerable<T>> GetByReferenceIdAsync(Guid id) => repository.FindBy(r => r.ReferenceId);

    }
}