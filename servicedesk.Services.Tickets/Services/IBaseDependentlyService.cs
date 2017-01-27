using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace servicedesk.Services.Tickets.Services
{
    public interface IBaseDependentlyService<T> : IBaseService<T>
    {
        Task<IEnumerable<T>> GetByReferenceIdAsync(Guid id);
    }
}