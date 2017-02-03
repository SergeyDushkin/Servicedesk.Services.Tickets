using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using servicedesk.Common.Domain;
using servicedesk.Services.Tickets.Repositories;

namespace servicedesk.Services.Tickets.Services
{
    public class BaseService : IBaseService
    {
        protected readonly IBaseRepository repository; 

        public BaseService(IBaseRepository repository)
        {
            this.repository = repository;
        }

        public Task CreateAsync<T>(T @create) where T : class, IIdentifiable, new()
        {
            repository.Add(@create);
            return repository.CommitAsync();
        }

        public Task UpdateAsync<T>(T @update) where T : class, IIdentifiable, new()
        {
            repository.Update(@update);
            return repository.CommitAsync();
        }

        public Task DeleteAsync<T>(T @delete) where T : class, IIdentifiable, new()
        {
            repository.Delete(@delete);
            return repository.CommitAsync();
        }

        public Task<IEnumerable<T>> GetAsync<T>() where T : class, IIdentifiable, new() => repository.GetAllAsync<T>();
        public Task<IEnumerable<T>> GetAsync<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, IIdentifiable, new() => repository.AllIncludingAsync<T>(includeProperties);

        public Task<T> GetByIdAsync<T>(Guid id) where T : class, IIdentifiable, new() => repository.GetSingleAsync<T>(id);
        public Task<T> GetByIdAsync<T>(Guid id, params Expression<Func<T, object>>[] includeProperties) where T : class, IIdentifiable, new() => repository.GetSingleAsync<T>(id, includeProperties);
    }
}