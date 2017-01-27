using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using servicedesk.Common.Domain;

namespace servicedesk.Services.Tickets.Repositories
{
    public interface IBaseRepository<T> where T : class, IIdentifiable, new()
    {
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAllAsync();
        int Count();
        Task<T> GetSingleAsync(Guid id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        Task CommitAsync();
    }
}