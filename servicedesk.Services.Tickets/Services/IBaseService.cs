using servicedesk.Common.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace servicedesk.Services.Tickets.Services
{
    public interface IBaseService
    {
        Task<IEnumerable<T>> GetAsync<T>() where T: class, IIdentifiable, new();
        Task<T> GetByIdAsync<T>(Guid id) where T : class, IIdentifiable, new();
        Task CreateAsync<T>(T @create) where T : class, IIdentifiable, new();
    }

        /*
    public interface IBaseService<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(Guid id);
        Task CreateAsync(T @create);
    }*/
}