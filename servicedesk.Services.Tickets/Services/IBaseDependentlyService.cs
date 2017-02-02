using servicedesk.Common.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}