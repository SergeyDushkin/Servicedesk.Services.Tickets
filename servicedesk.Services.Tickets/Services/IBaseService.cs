using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace servicedesk.Services.Tickets.Services
{
    public interface IBaseService<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(Guid id);
        Task CreateAsync(T @create);
    }
}