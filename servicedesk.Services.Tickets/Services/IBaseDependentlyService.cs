using servicedesk.Common.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace servicedesk.Services.Tickets.Services
{
    /*
    public interface IBaseDependentlyService<T> : IBaseService<T>
    {
        Task<IEnumerable<T>> GetByReferenceIdAsync(Guid id);
    }
    */

    public interface IBaseDependentlyService : IBaseService
    {
        Task<IEnumerable<T>> GetByReferenceIdAsync<T>(Guid id) where T : class, IIdentifiable, IDependently, new();
        Task<IEnumerable<T>> GetByReferenceIdAsync<T>(Guid id, params Expression<Func<T, object>>[] includeProperties) where T : class, IIdentifiable, IDependently, new();
    }
}