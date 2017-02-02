using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using servicedesk.Common.Domain;
using servicedesk.Services.Tickets.Repositories;

namespace servicedesk.Services.Tickets.Services
{
    /*
    public class BaseDependentlyService<T> : IBaseDependentlyService<T> where T : class, IIdentifiable, IDependently, new()
    {
        private readonly IBaseRepository<T> repository;

        public BaseDependentlyService(IBaseRepository<T> repository)
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

        public Task<IEnumerable<T>> GetByReferenceIdAsync(Guid id) => repository.FindByAsync(r => r.ReferenceId == id);
    }*/

    public class BaseDependentlyService : BaseService, IBaseDependentlyService
    {
        public BaseDependentlyService(IBaseRepository repository) : base(repository)
        {
        }

        public Task<IEnumerable<T>> GetByReferenceIdAsync<T>(Guid id) where T : class, IIdentifiable, IDependently, new() => repository.FindByAsync<T>(r => r.ReferenceId == id);
    }
}