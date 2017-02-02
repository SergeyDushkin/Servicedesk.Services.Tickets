using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using servicedesk.Common.Domain;
using servicedesk.Services.Tickets.Repositories;

namespace servicedesk.Services.Tickets.Services
{
    /*
    public class BaseService<T> : IBaseService<T> where T : class, IIdentifiable, new()
    {
        private readonly IBaseRepository<T> repository;

        public BaseService(IBaseRepository<T> repository)
        {
            this.repository = repository;
        }

        public Task CreateAsync(T @create)
        {
            repository.Add(@create);
            return repository.CommitAsync();
        }

        public Task<IEnumerable<T>> GetAsync() => repository.GetAllAsync();

        public Task<T> GetByIdAsync(Guid id) => repository.GetSingleAsync(id);
    }*/

    public class BaseService : IBaseService
    {
        private readonly IBaseRepository repository; 

        public BaseService(IBaseRepository repository)
        {
            this.repository = repository;
        }

        public Task CreateAsync<T>(T @create) where T : class, IIdentifiable, new()
        {
            repository.Add(@create);
            return repository.CommitAsync();
        }

        public Task<IEnumerable<T>> GetAsync<T>() where T : class, IIdentifiable, new() => repository.GetAllAsync<T>();

        public Task<T> GetByIdAsync<T>(Guid id) where T : class, IIdentifiable, new() => repository.GetSingleAsync<T>(id);
    }
}